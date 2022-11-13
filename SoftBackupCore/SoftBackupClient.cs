using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftBackupCore
{
    public class SoftBackupClient : SoftBackupCommon
    {
        public static SoftBackupClient Instance { get; private set; }
        public ConnClient ConnService { get; set; }
        
        private SoftBackupClient(Action<Message> processMessageClientSide)
        {
            ConnService = new ConnClient(processMessageClientSide);
            ConnService.Start();
        }

        public void SendMessageToServer(Message msg)
        {
            ConnService.SendMessageToServer(msg);
        }

        public override void Terminate()
        {
            ConnService.Terminate();
        }
        public static void LoadInstance(Action<Message> processMessageClientSide)
        {
            if (Instance == null)
            {
                Instance = new SoftBackupClient(processMessageClientSide);
            }
        }

        public Configuracao LoadConfiguracao()
        {
            Configuracao configuracao;
            if (File.Exists(FileConf))
            {
                configuracao = JsonConvert.DeserializeObject<Configuracao>(File.ReadAllText(FileConf));
            }
            else
            {
                configuracao = new Configuracao();
            }

            return configuracao;
        }

    }
}
