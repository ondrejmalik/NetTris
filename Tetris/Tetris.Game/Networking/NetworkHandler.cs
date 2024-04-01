using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using osu.Framework.Logging;
using osu.Framework.Threading;

namespace Tetris.Game.Networking;

public class NetworkHandler(IPEndPoint serverIp)
{
    UdpClient client = new UdpClient();
    private IPEndPoint serverIp = serverIp;
    DateTime lastRecieve = DateTime.Now;
    public static object _MultiplayerLock = new object();

    public void Loop(PlayField playField1, PlayField playField2)
    {
        Stopwatch sw = new();
        string response;

        while (true)
        {
            try
            {
                sw.Start();
                Packet packet = new();
                if (playField1.Occupied.Count > 0)
                {
                    lock (_MultiplayerLock)
                    {
                        packet = new(playField1.Occupied, playField1.Piece.GridPos,
                            playField1.Piece.PieceType);
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
                playField2.Occupied = deserialized.Occupied;
                playField2.Piece.PieceType = deserialized.PieceType;
                playField2.Piece.GridPos = deserialized.DeserializePiecePos();
                playField2.ScheduleRedraw();
                sw.Stop();
                Thread.Sleep(1000);
                Logger.Log(playField2.Occupied.Where(x => x.Occupied).Count().ToString());
                foreach (var pos in playField2.Piece.GridPos)
                {
                    Logger.Log(pos.Item1 + " " + pos.Item2);
                }

                Logger.Log(deserialized.Occupied.Where(x => x.Occupied).Count().ToString());
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
        client.Send(data, data.Length, serverIp);
    }

    public string Recieve()
    {
        try
        {
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = client.Receive(ref serverIp);
            this.serverIp = serverIp;
            return Encoding.ASCII.GetString(data);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
        }

        return null;
    }
}
