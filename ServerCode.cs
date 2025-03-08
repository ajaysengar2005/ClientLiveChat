using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatServer
{
    private static Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();

    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine("Server started. Waiting for clients...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Thread thread = new Thread(() => HandleClient(client));
            thread.Start();
        }
    }

    static void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        // Receive username
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string username = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
        lock (clients) clients[client] = username;

        Console.WriteLine(username + " joined the chat!");
        BroadcastMessage(username + " joined the chat!", client);

        try
        {
            while (true)
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break; // Client disconnected

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                BroadcastMessage(username + ": " + message, client);
            }
        }
        catch { }

        // Handle client disconnect
        lock (clients)
        {
            Console.WriteLine(username + " left the chat.");
            clients.Remove(client);
        }
        BroadcastMessage(username + " left the chat.", null);
        client.Close();
    }

    static void BroadcastMessage(string message, TcpClient sender)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        lock (clients)
        {
            foreach (var client in clients.Keys)
            {
                if (client != sender)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
