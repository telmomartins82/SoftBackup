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
    public abstract class SoftBackupCommon
    {
#if DEBUG
        public const bool RUN_AS_DEV = true;
        public const bool DELAY_BACKUP_DEBUG = true;
        public const bool FORCE_APPDATA_PATH = true;
#else
        public const bool RUN_AS_DEV = false;
        public const bool DELAY_BACKUP_DEBUG = false;
        public const bool FORCE_APPDATA_PATH = true;
#endif
        public string FileConf { get; set; }
        public string FileLog { get; set; }
        public string FileLogClient { get; set; }
        public string FileLogDir { get; set; }
        public DateTime NextRun { get; set; }
        public DateTime LastRun { get; set; }

        public abstract void Terminate();
        
        protected SoftBackupCommon()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (FORCE_APPDATA_PATH || (Assembly.GetEntryAssembly().Location.ToLower().IndexOf("program files") < 4 && Assembly.GetEntryAssembly().Location.ToLower().IndexOf("program files") > 0))
            {
                var basePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "SoftBackup");

                Directory.CreateDirectory(basePath);
                FileLogDir = basePath;
                FileConf = Path.Combine(basePath, (RUN_AS_DEV ? "DEV_" : "") + "SoftBackup.conf");
                FileLog = Path.Combine(basePath, (RUN_AS_DEV ? "DEV_" : "") + "SoftBackup{Date}.log");
                FileLogClient = Path.Combine(basePath, (RUN_AS_DEV ? "DEV_" : "") + "SoftBackupClient.log.txt");
            }
            else
            {
                FileLogDir = "";
                FileConf = (RUN_AS_DEV ? "DEV_" : "") + "SoftBackup.conf";
                FileLog = (RUN_AS_DEV ? "DEV_" : "") + "SoftBackup.log";
                FileLogClient = (RUN_AS_DEV ? "DEV_" : "") + "SoftBackupClient.txt";
                File.Delete(FileLog);
                File.Delete(FileLogClient);
            }
        }
        
        public string GetPathFileLog(DateTime? now = null)
        {
            if(now == null)
            {
                now = DateTime.Now;
            }
            return FileLog.Replace("{Date}", "." + ((DateTime)now).ToString("yyyy-MM-dd").ToUpper());
        }

        public void SendToClientLog(string content, bool showTimeStamp = true)
        {
            /*string logData = (showTimeStamp ? DateTime.Now.ToString("[MMM-dd HH:mm:ss]").ToUpper() + " " : "") + content;
            byte[] data = Encoding.UTF8.GetBytes(logData);
            using (FileStream fs = new FileStream(FileLogClient, FileMode.Append))
            {
                fs.Write(data, 0, data.Length);
            }*/

        }

    }
}
