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

/// <summary>
/// Takes care of network communication with server.
/// </summary>
public class NetworkHandler()
{
    /// <summary>
    /// Singleton instance of the NetworkHandler.
    /// </summary>
    private static NetworkHandler instance = null;

    /// <summary>
    /// Time between sending packet and recieving packet.
    /// </summary>
    public static int LastMs = 0;

    /// <summary>
    /// Gets the instance of the NetworkHandler.
    /// </summary>
    /// <param name="serverIp">ip of server</param>
    /// <param name="port">port of server</param>
    /// <param name="tickRate">rate at which it will try to send data (ms)</param>
    /// <returns></returns>
    public static NetworkHandler GetInstance(string serverIp, int port, int tickRate = 5)
    {
        if (instance == null)
        {
            instance = new NetworkHandler(serverIp, port, tickRate);
        }

        return instance;
    }

    /// <summary>
    /// Disposes the instance of the NetworkHandler.
    /// </summary>
    public static void DisposeInstance()
    {
        instance = null;
    }

    /// <summary>
    /// UdpClient used for communication with server.
    /// </summary>
    public UdpClient Client = new UdpClient();

    /// <summary>
    /// Event that is triggered when the game is ready.
    /// </summary>
    public event EventHandler<EventArgs> GameIsReady;

    /// <summary>
    /// Event that is triggered when the game is over.
    /// </summary>
    public event EventHandler<GameOverEventArgs> GameOver;

    /// <summary>
    /// If the network handler should be running.
    /// </summary>
    public volatile bool Running = true;

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

    /// <summary>
    /// Starts the network handler.
    /// </summary>
    public void Start(PlayField playFieldLeft, PlayField playFieldRight)
    {
        playFieldLeft.GameOverChanged += (sender, args) => sendGameOver = true;
        this.playFieldLeft = playFieldLeft;
        this.playFieldRight = playFieldRight;
        Handshake();
        Loop();
    }

    /// <summary>
    /// Performs handshake with server.
    /// </summary>
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

    /// <summary>
    /// Main send recieve loop of the network handler.
    /// </summary>
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

                Send(packet);
                //------------------------Recieve part-----------------------------------
                string received = Receive();
                //Console.WriteLine(recieved);
                if (received == null || received == "" || received == "OK")
                {
                    continue;
                }

                Packet deserialized;
                try
                {
                    deserialized = Packet.Deserialize(received);
                    if (deserialized.Command.PacketCommandType == PacketCommandType.GameOver)
                    {
                        OnGameOver(false);
                        Running = false;
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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

    /// <summary>
    /// Sends a serialized packet to the server.
    /// </summary>
    /// <param name="packet"><see cref="Packet"/> that shold be sent to server</param>
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

    /// <summary>
    /// Recieves a packet from the server.
    /// </summary>
    /// <returns>pancket json string</returns>
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


    #region Event Invokes

    /// <summary>
    /// Invokes the GameIsReady event.
    /// </summary>
    private void OnGameIsReady()
    {
        GameIsReady?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// Invokes the GameOver event.
    /// </summary>
    /// <param name="lost">If the player lost</param>
    private void OnGameOver(bool lost)
    {
        Logger.Log("Game Over " + (lost ? "You Lost" : "You Won"));
        GameOver?.Invoke(this, new GameOverEventArgs(lost));
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles the event of sending garbage lines to the opponent.
    /// </summary>
    private void handleSendLines(object sender, SendLinesEventArgs eventArgs)
    {
        newLines = eventArgs.Lines;
        lastPieceGridPos = eventArgs.LastPieceGridPos;
    }

    /// <summary>
    /// Sets playfield values to the values in packet.
    /// </summary>
    /// <param name="deserialized"></param>
    private void setNewPlayfieldValues(Packet deserialized)
    {
        playFieldRight.Occupied = deserialized.Occupied;
        playFieldRight.Piece.PieceType = deserialized.PieceType;
        playFieldRight.Piece.GridPos = deserialized.DeserializePiecePos();
        playFieldRight.ScheduleRedraw();
    }

    #endregion

    #region Parsing

    /// <summary>
    /// Compares the ClearedLines in right playfield and lines in packet and Schedules addition garbage lines to the left playfield based on the difference.
    /// </summary>
    /// <param name="deserialized"></param>
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

    /// <summary>
    /// Parses a string to a list of cords based on <see cref="PacketCommandSendLines"/> format.
    /// </summary>
    /// <param name="cords">value in <see cref="PacketCommandSendLines"/></param>
    /// <returns>List of cords</returns>
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
