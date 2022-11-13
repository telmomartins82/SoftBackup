using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SoftBackupCore
{
    public class ConnClient
    {
        private Action<Message> CallbackReceivedMessage;
        private ConnListener Client;
        private bool Closed { get; set; } = false;
        public bool ConnectedToServer { get { return Client != null && Client.SocketConnected; } }

        public ConnClient(Action<Message> callbackReceivedMessage)
        {
            CallbackReceivedMessage = callbackReceivedMessage;
        }

        public void Start()
        {
            TcpClient cli = new TcpClient();
            try
            {
                cli.Connect(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), ConnListener.TCP_PORT_SERVICE));
                Client = new ConnListener(false, cli, CallbackReceivedMessage);
                Client.Start();
            }
            catch(Exception)
            {

            }

        }

        public void SendMessageToServer(Message msg)
        {
            Client.SendMessageToConnection(msg);
        }

        public bool Reconnect()
        {
            if(Client != null)
            {
                Client.Terminate();
            }
            Start();
            return ConnectedToServer;
        }

        public void Terminate()
        {
            Closed = true;
            if (Client != null)
            {
                Client.Terminate();
            }
        }
    }
}
