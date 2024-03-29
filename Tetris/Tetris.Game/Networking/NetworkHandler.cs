using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tetris.Game.Networking;

public class NetworkHandler(IPEndPoint serverIp)
{
    UdpClient client;
    private IPEndPoint serverIp = serverIp;

    public void Send(Packet packet)
    {
        byte[] data = Encoding.ASCII.GetBytes(packet.Serialize());
        client.Send(data, data.Length, serverIp);
    }
}
