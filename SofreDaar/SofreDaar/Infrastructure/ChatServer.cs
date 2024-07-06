using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ChatServer
{
    private TcpListener _listener;
    private List<TcpClient> _clients = new List<TcpClient>();

    public ChatServer(string ipAddress, int port)
    {
        _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
    }

    public void Start()
    {
        _listener.Start();
        Thread listenThread = new Thread(ListenForClients);
        listenThread.Start();
    }

    private void ListenForClients()
    {
        while (true)
        {
            TcpClient client = _listener.AcceptTcpClient();
            _clients.Add(client);
            Thread clientThread = new Thread(HandleClientComm);
            clientThread.Start(client);
        }
    }

    private void HandleClientComm(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;
        NetworkStream clientStream = client.GetStream();
        byte[] message = new byte[4096];
        int bytesRead;

        try
        {
            while (true)
            {
                bytesRead = 0;
                bytesRead = clientStream.Read(message, 0, 4096);

                if (bytesRead == 0)
                {
                    break;
                }

                string receivedMessage = Encoding.UTF8.GetString(message, 0, bytesRead);

                foreach (var otherClient in _clients)
                {
                    if (otherClient != client)
                    {
                        NetworkStream stream = otherClient.GetStream();
                        stream.Write(message, 0, bytesRead);
                        stream.Flush();
                    }
                }
            }
        }
        catch (Exception ex)
        {
           
        }
        finally
        {
            client.Close();
            _clients.Remove(client);
        }
    }
}
