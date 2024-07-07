using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ChatClient
{
    private TcpClient _client;
    private NetworkStream _stream;
    public event Action<string> MessageReceived;

    public void Connect(string ipAddress, int port)
    {
        _client = new TcpClient();
        _client.Connect(ipAddress, port);
        _stream = _client.GetStream();

        Thread readThread = new Thread(ReadMessages);
        readThread.Start();
    }

    public void SendMessage(string message, string userName)
    {
        if (_stream != null)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(userName + ": " + message);
            _stream.Write(buffer, 0, buffer.Length);
            _stream.Flush();
        }
    }

    private void ReadMessages()
    {
        byte[] buffer = new byte[4096];
        int bytesRead;

        try
        {
            while (true)
            {
                bytesRead = _stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    MessageReceived?.Invoke(message);
                }
            }
        }
        catch (Exception ex)
        {
          
        }
        finally
        {
            _client.Close();
        }
    }
}
