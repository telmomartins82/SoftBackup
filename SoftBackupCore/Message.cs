using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftBackupCore
{
    public class Message
    {
        
        public enum NotificationType { CLIENT_START_BACKUP, START_BACKUP_RESPONSE, CLIENT_STOP_BACKUP, STOP_BACKUP_RESPONSE, CLIENT_SAVE_CONFIGURATION, CLIENT_SAVE_CLIENTCONFIGURATION, SAVE_CONFIGURATION_RESPONSE, CLIENT_GET_STATUS, NOTIFY_ERROR_BACKUP, NOTIFY_CONFIGURATION_UPDATED, NOTIFY_STATUS, NOTIFY_LOG}
        public ServiceStatus ServiceStatus { get; set; } = new ServiceStatus();
        public NotificationType Type { get; set; }
        public bool IsRequestSuccess { get; set; }
        public string Error { get; set; }
        public byte[] ErrorBin { get; set; }
        public List<BackupEntry> BackupEntries { get; set; }
        public Configuracao Configuracao { get; set; }
        public string LogData { get; set; }
        public byte[] LogDataBin { get; set; }

    }

    public class ServiceStatus
    {
        public bool IsServiceRunning { get; set; }
        public bool IsExecutingBackup { get; set; }
        public DateTime NextRun { get; set; }
        public DateTime LastRun { get; set; }
    }
}
