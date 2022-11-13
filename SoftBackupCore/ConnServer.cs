using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftBackupCore
{
    public class ConnServer
    {
        private Thread TAcceptConnections;
        public Action<Message> CallbackReceivedMessage;
        private ConnListener Client;
        private TcpListener TcpListener { get; set; }
        private bool Closed { get; set; } = false;
        public ConnServer(Action<Message> callbackReceivedMessage)
        {
            CallbackReceivedMessage = callbackReceivedMessage;
        }

        public void Start()
        {
            TAcceptConnections = new Thread(new ThreadStart(StartAcceptingConnections));
            TAcceptConnections.Start();
        }

        private void StartAcceptingConnections()
        {
            TcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), ConnListener.TCP_PORT_SERVICE);
            TcpListener.Start();
            while (true)
            {
                try
                {
                    var client = TcpListener.AcceptTcpClient();
                    if (Client != null)
                    {
                        Client.Terminate();
                    }
                    
                    Client = new ConnListener(true, client, CallbackReceivedMessage);
                    Client.Start();
                    
                }
                catch (Exception)
                {
                    
                }
            }
        }

        public void SendMessageToClient(Message msg)
        {
            if(Client != null)
            {
                Client.SendMessageToConnection(msg);
            }
        }

        public void Terminate()
        {
            Closed = true;
            TcpListener.Stop();
            try
            {
                TAcceptConnections.Abort();
            }
            catch (Exception) { }
            if(Client != null)
            {
                Client.Terminate();
            }
        }
    }
}
