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

namespace Tetris.Game.Networking;

public class NetworkHandler()
{
    public UdpClient Client = new UdpClient();
    public event EventHandler<EventArgs> GameIsReady;
    public volatile bool Running = true;
    DateTime lastRecieve = DateTime.Now;
    public static object _MultiplayerLock = new object();

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

    public NetworkHandler(IPEndPoint serverIp, int tickRate = 5) : this()
    {
        this.serverIp = serverIp;
        TickRate = tickRate;
    }

    public void Start(PlayField playFieldLeft, PlayField playFieldRight)
    {
        this.playFieldLeft = playFieldLeft;
        this.playFieldRight = playFieldRight;
        Handshake();
        Loop();
    }

    public void Handshake()
    {
        while (true) // confirm connection to server
        {
            Packet packet = new(new PacketCommandStart());
            Send(packet);
            string recieved = Recieve();
            if (recieved == "OK") break;
            Thread.Sleep(64);
        }

        Logger.Log("Connected to server");
        while (true) // wait for second player to connect
        {
            string recieved = Recieve();
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
                        packet = new(playFieldLeft.Occupied, playFieldLeft.Piece.GridPos,
                            playFieldLeft.Piece.PieceType, new PacketCommandSendLines(newLines, lastPieceGridPos));
                    }
                }

                Send(packet);
                string recieved = Recieve();
                //Console.WriteLine(recieved);
                if (recieved == null || recieved == "" || recieved == "OK")
                {
                    continue;
                }

                Packet deserialized = Packet.Deserialize(recieved);

                addSentLines(deserialized);

                setNewPlayfieldValues(deserialized);


                Thread.Sleep((int)(tickMs - Math.Min(tickMs, sw.ElapsedMilliseconds)));
                Logger.Log(sw.ElapsedMilliseconds.ToString() + "ms -----");
                sw.Restart();
                //Logger.Log(playFieldRight.Occupied.Where(x => x.Occupied).Count().ToString());
                foreach (var pos in playFieldRight.Piece.GridPos)
                {
                    Logger.Log(pos.Item1 + " " + pos.Item2);
                }
                //Logger.Log(deserialized.Occupied.Where(x => x.Occupied).Count().ToString());
            }
            catch (Exception e)
            {
                sw.Restart();
                Console.WriteLine(e);
            }
        }
    }

    public void Send(Packet packet)
    {
        byte[] data = Encoding.ASCII.GetBytes(packet.Serialize());
        Client.Send(data, data.Length, serverIp);
    }

    public string Recieve()
    {
        try
        {
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = Client.Receive(ref serverIp);
            this.serverIp = serverIp;
            return Encoding.ASCII.GetString(data);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
        }

        return null;
    }

    private void OnGameIsReady()
    {
        GameIsReady?.Invoke(this, new EventArgs());
    }

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

    private void addSentLines(Packet deserialized)
    {
        string[] split = deserialized.Command.CommandData.Split("-");
        int diff = int.Parse(split[0]) - playFieldRight.ClearedLines;
        Logger.Log($"diff {diff.ToString()}");
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
}
