using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using osu.Framework.Logging;
using Tetris.Game.Game.Playfield;

namespace Tetris.Game.Networking;

public class NetworkHandler(IPEndPoint serverIp)
{
    public UdpClient Client = new UdpClient();
    private IPEndPoint serverIp = serverIp;
    public volatile bool Running = true;
    DateTime lastRecieve = DateTime.Now;
    public static object _MultiplayerLock = new object();
    private int newLines;
    private PlayField playFieldLeft;
    private PlayField playFieldRight;

    public void Loop(PlayField playFieldLeft, PlayField playFieldRight)
    {
        this.playFieldLeft = playFieldLeft;
        this.playFieldRight = playFieldRight;
        Stopwatch sw = new();
        string response;

        while (Running)
        {
            try
            {
                sw.Start();
                Packet packet = new();
                if (playFieldLeft.Occupied.Count > 0)
                {
                    lock (_MultiplayerLock)
                    {
                        playFieldLeft.ClearedLinesChanged += handleSendLines;
                        packet = new(playFieldLeft.Occupied, playFieldLeft.Piece.GridPos,
                            playFieldLeft.Piece.PieceType, new PacketCommandSendLines(newLines));
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

                sw.Stop();
                Thread.Sleep(1000);

                //Logger.Log(playFieldRight.Occupied.Where(x => x.Occupied).Count().ToString());
                foreach (var pos in playFieldRight.Piece.GridPos)
                {
                    Logger.Log(pos.Item1 + " " + pos.Item2);
                }

                //Logger.Log(deserialized.Occupied.Where(x => x.Occupied).Count().ToString());
            }
            catch (Exception e)
            {
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

    private void handleSendLines(object sender, SendLinesEventArgs eventArgs)
    {
        newLines = eventArgs.Lines;
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
        int diff = int.Parse(deserialized.Command.CommandData) - playFieldRight.ClearedLines;
        Logger.Log($"diff {diff.ToString()}");
        if (diff > 0)
        {
            playFieldLeft.ScheduleAddGarbage(diff);
        }
    }
}
