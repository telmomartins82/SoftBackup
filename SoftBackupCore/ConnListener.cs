using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftBackupCore
{
    public class ConnListener
    {
#if DEBUG
        public const int TCP_PORT_SERVICE = 54832;
        public const bool LOG_CONSOLE_MSGS = false;
#else
        public const int TCP_PORT_SERVICE = 54831;
        public const bool LOG_CONSOLE_MSGS = false;
#endif
        public const int RESPONSE_TIMEOUT = 2000;//milliseg
        public const int RESPONSE_TIMEOUT_STATUS = 1000;//milliseg

        private Thread TListener { get; set; }
        private Action<Message> CallbackMessageReceived;
        private TcpClient Client;
        private bool ServiceSide { get; set; }
        private bool Closed { get; set; } = false;
        public bool SocketConnected { get { return Client != null && Client.Connected; } }

        public ConnListener(bool serviceSide, TcpClient tcpSocket, Action<Message> callbackMessageReceived)
        {
            ServiceSide = serviceSide;
            Client = tcpSocket;
            CallbackMessageReceived = callbackMessageReceived;
        }

        public void Start()
        {
            TListener = new Thread(new ThreadStart(StartReceiving));
            TListener.Start();
        }

        private void StartReceiving()
        {
            var data = new byte[1024];
            string tst;
            // Get a stream object for reading and writing
            NetworkStream stream = Client.GetStream();
            while (true)
            {
                try
                {
                    var sizeStr = "";
                    while (true)
                    {
                        var b = (byte)stream.ReadByte();
                        if(b == 255)
                        {
                            Client.Close();
                            return;
                        }
                        var c = Encoding.UTF8.GetString(new byte[] { b });
                        if (c != "-")
                        {
                            sizeStr = sizeStr + c;
                        }
                        else
                        {
                            break;
                        }
                    }

                    int lengthToRead = int.Parse(sizeStr);
                    var bytes = new byte[lengthToRead];
                    while (lengthToRead > 0)
                    {
                        var i = stream.Read(bytes, bytes.Length - lengthToRead, lengthToRead);
                        lengthToRead -= i;
                    }

                    string json = Encoding.UTF8.GetString(bytes);tst = json;
                    Message msg = JsonConvert.DeserializeObject<Message>(json);
                    if(msg.ErrorBin != null && msg.ErrorBin.Length > 0)
                    {
                        msg.Error = Encoding.UTF8.GetString(msg.ErrorBin);
                        msg.ErrorBin = null;
                    }
                    if (msg.LogDataBin != null && msg.LogDataBin.Length > 0)
                    {
                        msg.LogData = Encoding.UTF8.GetString(msg.LogDataBin);
                        msg.LogDataBin = null;
                    }

                    if (LOG_CONSOLE_MSGS && SoftBackupServer.RUN_AS_DEV && ServiceSide)
                    {
#pragma warning disable CS0162 // Unreachable code detected
                        Console.WriteLine("RECEIVE from client:\n\r" + MessageToString(msg) + "\n\r\n\r");
#pragma warning restore CS0162 // Unreachable code detected
                    }
                    else if(!ServiceSide)
                    {
                        SoftBackupClient.Instance.SendToClientLog($"Receive message from server: { msg.Type}\r\n");
                    }
                    CallbackMessageReceived(msg);
                }
                catch (Exception)
                {
                    if (Closed)
                    {
                        return;
                    }
                    if (ServiceSide)
                    {
                        //SoftBackupServer.Instance.SendToLogFile(false, "Error on Tcp object: " + e.StackTrace, true);
                    }

                    Client.Close();
                    return;
                }
            }

        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendMessageToConnection(Message message)
        {
            if (!Client.Connected)
            {
                if (ServiceSide) return; else throw new Exception();
            }
            
            try
            {
                if (!string.IsNullOrEmpty(message.Error))
                {
                    message.ErrorBin = Encoding.UTF8.GetBytes(message.Error);
                    message.Error = "";
                }
                if (!string.IsNullOrEmpty(message.LogData))
                {
                    message.LogDataBin = Encoding.UTF8.GetBytes(message.LogData);
                    message.LogData = "";
                }

                string json = JsonConvert.SerializeObject(message);
                byte[] dataJson = Encoding.UTF8.GetBytes(json);
                byte[] data = Encoding.UTF8.GetBytes(("" + dataJson.Length) + "-");
                Client.GetStream().Write(data, 0, data.Length);
                Client.GetStream().Write(dataJson, 0, dataJson.Length);

                if (LOG_CONSOLE_MSGS && SoftBackupServer.RUN_AS_DEV && ServiceSide)
                {
#pragma warning disable CS0162 // Unreachable code detected
                    Console.WriteLine("SENDING to client:\n\r" + MessageToString(message) + "\n\r\n\r");
#pragma warning restore CS0162 // Unreachable code detected
                }
                else if(!ServiceSide)
                {
                    SoftBackupClient.Instance.SendToClientLog($"Send message to server: { message.Type}\r\n");
                }
            }
            catch (Exception e)
            {
                if (ServiceSide)
                {
                    SoftBackupServer.Instance.SendToLogFile(false, "Error sending message to Client: " + e.StackTrace, true);
                }
                //nao pode enviar para o log file do client
                //SoftBackup.Instance.SendToLogFile(false, "Error sending UDP message to Server: " + e.StackTrace + "\r\n\r\n", true);
                /*Message resp = new Message();
                resp.Type = Message.NotificationType.SYNC_RESPONSE_STATUS;
                resp.IsRequestSuccess = false;
                resp.Error = e.StackTrace;
                return resp;*/
            }
        }

        public string MessageToString(Message msg)
        {
            return $"[{DateTime.Now.ToString("MMM-dd HH:mm:ss")}] {msg.Type} - {msg.IsRequestSuccess} - LastRun {msg.ServiceStatus.LastRun} NextRun {msg.ServiceStatus.NextRun}" + 
                $" In backup: {msg.ServiceStatus.IsExecutingBackup}\r\n - {msg.Error}";
        }

        public void Terminate()
        {
            Closed = true;
            if(Client != null)
            {
                Client.Close();
            }
            try
            {
                TListener.Abort();
            }
            catch (Exception) { }
        }

    }
}
