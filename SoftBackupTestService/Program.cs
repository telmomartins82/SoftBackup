using SoftBackupCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SoftBackupTestService
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Thread(new ThreadStart(SendError));
            t.Start();
            new JobBackups().Executa();
        }

        private static void SendError()
        {
            while (true)
            {
                var l = Console.ReadLine();
                Message msg = new Message();
                msg.Type = Message.NotificationType.NOTIFY_ERROR_BACKUP;
                SoftBackupServer.Instance.ConnService.SendMessageToClient(msg);
                if("ss" == l)
                {
                    Process.Start("D:\\Aplicações e Projectos\\SoftBackup\\bin\\Debug\\SoftBackup.exe", "false true");
                }
            }
        }
    }
}
