using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using osu.Framework.Logging;
using Tetris.Game.Game.Playfield;
using Tetris.Game.Networking.Commands;

namespace Tetris.Game.Networking;

public class NetworkHandler()
{
    private static NetworkHandler instance = null;
    public static int LastMs = 0;

    public static NetworkHandler GetInstance(string serverIp, int port, int tickRate = 5)
    {
        if (instance == null)
        {
            instance = new NetworkHandler(serverIp, port, tickRate);
        }

        return instance;
    }

    public static void DisposeInstance()
    {
        instance = null;
    }

    public UdpClient Client = new UdpClient();
    public event EventHandler<EventArgs> GameIsReady;
    public event EventHandler<GameOverEventArgs> GameOver;
    public volatile bool Running = true;
    DateTime lastRecieve = DateTime.Now;
    public static object _MultiplayerLock = new object();
    public Packet LastPacket = new();

    public int TickRate
    {
        get => 1 / tickMs * 1000;
        set => tickMs = (int)((double)1 / value * 1000);
    }

    private IPEndPoint serverIp;
    private int tickMs;
    private int newLines;
    private List<(int, int)> lastPieceGridPos;
    private PlayField playFieldLeft;
    private PlayField playFieldRight;
    private bool sendGameOver = false;


    private NetworkHandler(string serverIp, int port, int tickRate = 5) : this()
    {
        if (!IPAddress.TryParse(serverIp, out IPAddress ipAddress))
        {
            ipAddress = Dns.GetHostAddresses(serverIp)[0]; // Resolve domain to IP address
        }

        this.serverIp = new IPEndPoint(ipAddress, port);
        TickRate = tickRate;
    }

    #region Run

    public void Start(PlayField playFieldLeft, PlayField playFieldRight)
    {
        playFieldLeft.GameOverChanged += (sender, args) => sendGameOver = true;
        this.playFieldLeft = playFieldLeft;
        this.playFieldRight = playFieldRight;
        Handshake();
        Loop();
    }

    public void Handshake()
    {
        while (Running) // confirm connection to server
        {
            Packet packet = new(new PacketCommandStart());
            Send(packet);
            Logger.Log("Waiting for server to respond..");
            string recieved = Receive();
            if (recieved == "OK") break;
            Thread.Sleep(64);
        }

        Logger.Log("Connected to server");
        while (Running) // wait for second player to connect
        {
            string recieved = Receive();
            if (recieved == "GameReady")
            {
                Logger.Log("Game Ready");
                OnGameIsReady();
                break;
            }
        }
    }

    public void Loop()
    {
        //------------------------Send Part-----------------------------------
        Stopwatch sw = new();
        string response;
        playFieldLeft.ClearedLinesChanged += handleSendLines;
        sw.Start();
        while (Running)
        {
            try
            {
                Packet packet = new();
                if (playFieldLeft.Occupied.Count > 0)
                {
                    lock (_MultiplayerLock) // maybe remove lock
                    {
                        if (sendGameOver)
                        {
                            packet = new(new PacketCommandGameOver());
                            OnGameOver(true);
                        }
                        else
                        {
                            packet = new(playFieldLeft.Occupied, playFieldLeft.Piece.GridPos,
                                playFieldLeft.Piece.PieceType, new PacketCommandSendLines(newLines, lastPieceGridPos));
                        }
                    }
                }

                Send(packet);
                //------------------------Recieve part-----------------------------------
                string received = Receive();
                //Console.WriteLine(recieved);
                if (received == null || received == "" || received == "OK")
                {
                    continue;
                }

                Packet deserialized = Packet.Deserialize(received);
                if (deserialized.Command.PacketCommandType == PacketCommandType.GameOver)
                {
                    OnGameOver(false);
                    Running = false;
                    break;
                }

                addSentLines(deserialized);

                setNewPlayfieldValues(deserialized);

                Thread.Sleep((int)(tickMs - Math.Min(tickMs, sw.ElapsedMilliseconds)));
                LastMs = (int)sw.ElapsedMilliseconds;
                Logger.Log(LastMs + "ms -----");
                sw.Restart();
            }
            catch (Exception e)
            {
                sw.Restart();
                Console.WriteLine(e);
            }
        }
    }

    #endregion

    #region Send Receive

    public void Send(Packet packet)
    {
        try
        {
            byte[] data = Encoding.ASCII.GetBytes(packet.Serialize());
            Client.Send(data, data.Length, serverIp);
        }
        catch (Exception e)
        {
        }
    }

    public string Receive()
    {
        try
        {
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = Client.Receive(ref serverIp);
            this.serverIp = serverIp;
            return Encoding.ASCII.GetString(data);
        }
        catch (SocketException e)
        {
            Running = false;
        }
        catch (ObjectDisposedException e)
        {
            Running = false;
        }
        catch (Exception e)
        {
            Running = false;
            Console.WriteLine(e);
        }

        return null;
    }

    #endregion


    private List<OccupiedSet> getOccupiedDela(Packet packet)
    {
        if (LastPacket.Occupied != null)
        {
            List<OccupiedSet> delta = LastPacket.Occupied.Except(packet.Occupied)
                .Union(packet.Occupied.Except(LastPacket.Occupied)).ToList();
            return delta;
        }

        LastPacket = new Packet();
        return LastPacket.Occupied;
    }

    #region Event Invokes

    private void OnGameIsReady()
    {
        GameIsReady?.Invoke(this, new EventArgs());
    }

    private void OnGameOver(bool lost)
    {
        Logger.Log("Game Over " + (lost ? "You Lost" : "You Won"));
        GameOver?.Invoke(this, new GameOverEventArgs(lost));
    }

    #endregion

    #region Event Handlers

    private void handleSendLines(object sender, SendLinesEventArgs eventArgs)
    {
        newLines = eventArgs.Lines;
        lastPieceGridPos = eventArgs.LastPieceGridPos;
    }

    private void setNewPlayfieldValues(Packet deserialized)
    {
        playFieldRight.Occupied = deserialized.Occupied;
        playFieldRight.Piece.PieceType = deserialized.PieceType;
        playFieldRight.Piece.GridPos = deserialized.DeserializePiecePos();
        playFieldRight.ScheduleRedraw();
    }

    #endregion

    #region Parsing

    private void addSentLines(Packet deserialized)
    {
        string[] split = deserialized.Command.CommandData.Split("-");
        int diff = int.Parse(split[0]) - playFieldRight.ClearedLines;
        //Logger.Log($"diff {diff.ToString()}");
        if (diff > 0)
        {
            playFieldLeft.ScheduleAddGarbage(diff, parseStringToCords(split[1]));
        }
    }

    private List<(int, int)> parseStringToCords(string cords)
    {
        List<(int, int)> list = new();
        string[] split = cords.Split(";");
        foreach (var cord in split)
        {
            string[] split2 = cord.Split(",");
            list.Add((int.Parse(split2[0]), int.Parse(split2[1])));
        }

        return list;
    }

    #endregion
}
