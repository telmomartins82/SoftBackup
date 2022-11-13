using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SoftBackupServer : SoftBackupCommon
    {
        public static SoftBackupServer Instance { get; private set; }
        private Thread TCopiaBackup { get; set; }
        public ConnServer ConnService { get; set; }
        private Configuracao Configuracao { get; set; }
        public bool IsExecutingBackup { get; set; }
        private JobBackups ServerMainService { get; set; }
        public FileStream ServiceLogWriteStream { get; set; }

        private SoftBackupServer(JobBackups serverMainService)
        {
            ServerMainService = serverMainService;
            ConnService = new ConnServer(ProcessRequestMessageFromClient);
            ConnService.Start();
            SendToLogFile(true, "Windows service started with success.\r\n", true);
        }

        public void IniciaBackup(bool manual, List<BackupEntry> entries = null)
        {
            Configuracao = LoadConfiguracao();
            if (TCopiaBackup != null && TCopiaBackup.IsAlive)
            {
                SendToLogFile(false, "Já está um backup em curso." + (manual ? "" : " Backup automático não foi executado!!") + "\r\n", true);
                throw new Exception("Já está um backup em curso." + (manual ? "" : " Backup automático não foi executado!!")); ;
            }

            bool fullBackup = false;

            if (entries == null || entries.Count == 0)
            {
                entries = GetBackupEntries(true, Configuracao);
                fullBackup = true;
            }
            if (entries.Count == 0)
            {
                SendToLogFile(false, "Não é possivel efectuar o Backup: não existe nenhuma entrada de origem para fazer backup!\r\n", true);
                throw new Exception("Não é possivel efectuar o Backup: não existe nenhuma entrada de origem para fazer backup!");
            }

            IsExecutingBackup = true;
            TCopiaBackup = new Thread(new ThreadStart(new CopiaBackup(entries, Configuracao.BackupApenasModificados, fullBackup).Executa));
            TCopiaBackup.Start();
        }

        public override void Terminate()
        {
            AbortaBackupEmCurso(true);
            ConnService.Terminate();
            SendToLogFile(true, "Windows service stopped.\r\n", true);
            if (ServiceLogWriteStream != null)
            {
                ServiceLogWriteStream.Flush();
                ServiceLogWriteStream.Dispose();
            }
        }
        private bool AbortaBackupEmCurso(bool serviceStopped = false)
        {
            try
            {
                if (IsExecutingBackup)
                {
                    if (TCopiaBackup != null)
                    {
                        TCopiaBackup.Abort();
                    }
                    if (serviceStopped)
                    {
                        SendToLogFile(false, "Backup um curso cancelado devido ao Windows Service ter sido parado!!\r\n", true);
                    }
                    else
                    {
                        SendToLogFile(false, "Backup um curso cancelado pelo utilizador\r\n", true);
                    }
                }
                IsExecutingBackup = false;
                return true;
            }
            catch(Exception e)
            {
                SendToLogFile(false, $"ERRO a Abortar backups: {e.StackTrace}\r\n", true);
                return false;
            }
        }

        public void FinalizarBackup(bool success, DateTime startOn, bool fullBackup)
        {
            if (success)
            {
                if (fullBackup)
                {
                    Configuracao.LastRun = startOn;
                    LastRun = startOn;
                    GuardarConfiguracao(Configuracao);
                }
            }

            IsExecutingBackup = false;

            ServerMainService.ReCalculateNextRun();
            
            if (!success)
            {
                SendMessageToClient(Message.NotificationType.NOTIFY_ERROR_BACKUP);
                DirectoryInfo di = new DirectoryInfo("D:\\___SOFTBACKUP_ERRO_BACKUP_" + DateTime.Now.ToString("yyyy-MM-dd HHmmss"));
                if (!di.Exists)
                {
                    try
                    {
                        di.Create();
                    }
                    catch { }
                }
            }
        }

        public List<BackupEntry> GetBackupEntries(bool onlyActive, Configuracao configuracao = null)
        {
            if (configuracao == null)
            {
                configuracao = LoadConfiguracao();
            }
            return configuracao.BackupEntries.Where(e => !onlyActive || (onlyActive && e.Activo)).ToList();
        }

        public static void LoadInstance(JobBackups serverMainService)
        {
            if (Instance == null)
            {
                Instance = new SoftBackupServer(serverMainService);
            }
        }

        private void SendMessageToClient(Message.NotificationType type)
        {
            Message msg = new Message();
            msg.Type = type;
            msg.ServiceStatus = GetServiceStatus();
            ConnService.SendMessageToClient(msg);
        }

        public void SendStatusToClient()
        {
            SendMessageToClient(Message.NotificationType.NOTIFY_STATUS);
        }

        public void ProcessRequestMessageFromClient(Message message)
        {
            Message response = new Message();
            
            switch (message.Type)
            {
                case Message.NotificationType.CLIENT_START_BACKUP:
                    try
                    {
                        response.Type = Message.NotificationType.START_BACKUP_RESPONSE;
                        response.IsRequestSuccess = true;
                        IniciaBackup(true, message.BackupEntries);
                    }
                    catch (Exception e)
                    {
                        response.IsRequestSuccess = false;
                        response.Error = e.Message;
                    }
                    break;

                case Message.NotificationType.CLIENT_STOP_BACKUP:
                    try
                    {
                        response.Type = Message.NotificationType.STOP_BACKUP_RESPONSE;
                        var res = AbortaBackupEmCurso();
                        response.IsRequestSuccess = res;
                    }
                    catch (Exception e)
                    {
                        response.IsRequestSuccess = false;
                        response.Error = e.Message;
                    }
                    break;

                case Message.NotificationType.CLIENT_SAVE_CONFIGURATION:
                    try
                    {
                        response.Type = Message.NotificationType.SAVE_CONFIGURATION_RESPONSE;
                        if (IsExecutingBackup)
                        {
                            response.IsRequestSuccess = false;
                            response.Error = "Não é possivel salvar configuração, há um backup em curso!";
                        }
                        else
                        {
                            if(message.Configuracao == null)
                            {
                                throw new Exception("Nova config nao pode ser vazia.");
                            }
                            Configuracao conf = LoadConfiguracao();
                            message.Configuracao.LastRun = conf.LastRun;
                            GuardarConfiguracao(message.Configuracao);
                            NextRun = CalculateNextRun(message.Configuracao);
                            response.IsRequestSuccess = true;
                        }
                        
                    }
                    catch (Exception e)
                    {
                        response.IsRequestSuccess = false;
                        response.Error = e.Message;
                    }
                    break;

                case Message.NotificationType.CLIENT_SAVE_CLIENTCONFIGURATION:
                    try
                    {
                        response.Type = Message.NotificationType.SAVE_CONFIGURATION_RESPONSE;

                        if (message.Configuracao == null)
                        {
                            throw new Exception("Nova config nao pode ser vazia.");
                        }
                        Configuracao conf = LoadConfiguracao();
                        conf.LogApenasModificados = message.Configuracao.LogApenasModificados;
                        conf.LogTotal = message.Configuracao.LogTotal;
                        GuardarConfiguracao(conf);
                        if(Configuracao != null)
                        {
                            Configuracao.LogApenasModificados = message.Configuracao.LogApenasModificados;
                            Configuracao.LogTotal = message.Configuracao.LogTotal;
                        }
                        response.IsRequestSuccess = true;
                    }
                    catch (Exception e)
                    {
                        response.IsRequestSuccess = false;
                        response.Error = e.Message;
                    }
                    break;

                case Message.NotificationType.CLIENT_GET_STATUS:
                    response.Type = Message.NotificationType.NOTIFY_STATUS;
                    break;
                /*case Message.NotificationType.CLIENT_GET_LOG:
                    try
                    {
                        response.Type = Message.NotificationType.NOTIFY_LOG;
                        response.IsRequestSuccess = true;
                        FileInfo fi = new FileInfo(GetPathFileLog());
                        if (fi.Exists)
                        {
                            var streamR = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            byte[] data = new byte[fi.Length];
                            streamR.Read(data, 0, data.Length);
                            response.LogData = Encoding.UTF8.GetString(data);
                        }
                        else
                        {
                            response.LogData = "";
                        }
                    }
                    catch (Exception e)
                    {
                        response.IsRequestSuccess = false;
                        response.Error = e.Message;
                    }
                    break;*/
            }

            response.ServiceStatus = GetServiceStatus();
            ConnService.SendMessageToClient(response);
        }

        private ServiceStatus GetServiceStatus()
        {
            var res = new ServiceStatus();
            res.IsServiceRunning = true;
            res.IsExecutingBackup = IsExecutingBackup;
            res.NextRun = NextRun;
            res.LastRun = LastRun;
            return res;
        }

        public Configuracao LoadConfiguracao()
        {
            bool fileExists;
            Configuracao configuracao;
            if (File.Exists(FileConf))
            {
                configuracao = JsonConvert.DeserializeObject<Configuracao>(File.ReadAllText(FileConf));
                fileExists = true;
            }
            else
            {
                SendToLogFile(false, "ATENÇÃO: ficheiro de configuração não encontrado: " + FileConf + "\r\n", true);
                configuracao = new Configuracao();
                fileExists = false;
            }

            if (!fileExists)
            {
                GuardarConfiguracao(configuracao);
            }

            return configuracao;
        }

        public void GuardarConfiguracao(Configuracao configuracao)
        {
            try
            {
                string json = JsonConvert.SerializeObject(configuracao, Formatting.Indented);
                File.WriteAllText(FileConf, json);
            }
            catch (Exception e)
            {
                SendToLogFile(false, "ERRO FATAL ao guardar configuração!! " + e.Message + "\r\n", true);
            }

        }

        public void SendToLogFile(bool debug, string textLog, bool showTimeStamp, bool flagNotModified = false)
        {
            try
            {
                string content = (showTimeStamp ? DateTime.Now.ToString("[MMM-dd HH:mm:ss]").ToUpper() : "") + (debug && showTimeStamp ? "[DEBUG]" : "") + (flagNotModified & showTimeStamp ? "[NM]" : "") + (showTimeStamp ? " " : "") + textLog;
                if (ServiceLogWriteStream != null)
                {
                    string path = GetPathFileLog();
                    if(path != ServiceLogWriteStream.Name)
                    {
                        ServiceLogWriteStream.Dispose();
                        ServiceLogWriteStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    }
                }
                else
                {
                    ServiceLogWriteStream = new FileStream(GetPathFileLog(), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                
                var dados = Encoding.UTF8.GetBytes(content);
                ServiceLogWriteStream.Write(dados, 0, dados.Length);
                ServiceLogWriteStream.Flush();

                Message msg = new Message();
                msg.Type = Message.NotificationType.NOTIFY_LOG;
                msg.LogData = content;
                msg.ServiceStatus = GetServiceStatus();

                ConnService.SendMessageToClient(msg);
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void ClearOldLogFiles()
        {
            try
            {
                FileInfo fi = new FileInfo(SoftBackupServer.Instance.FileLog);
                List<FileInfo> fiLogs = new List<FileInfo>();
                var allFiles = fi.Directory.GetFiles();
                Array.Sort(allFiles, (f1, f2) => f2.LastWriteTimeUtc.CompareTo(f1.LastWriteTimeUtc));

                foreach (var f in allFiles)
                {
                    if (f.Name.EndsWith(".log", StringComparison.CurrentCultureIgnoreCase))
                    {
                        fiLogs.Add(f);
                    }
                }

                if (fiLogs.Count > 10)
                {
                    int i = 0;
                    foreach (var f in fiLogs)
                    {
                        i++;
                        if (i > 10)
                        {
                            f.Delete();
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                SendToLogFile(false, "ERROR: deleting old logs: " + e.StackTrace, true);
            }
        }

        public DateTime CalculateNextRun(Configuracao configuracao = null)
        {
            if (configuracao == null)
            {
                configuracao = LoadConfiguracao();
            }
            ExecucaoAutomatica conf = configuracao.Automatizacao;
            if (!conf.Activa)
            {
                return DateTime.MinValue;
            }

            DateTime now = DateTime.Now;
            switch (conf.Frequencia)
            {
                case ExecucaoAutomatica.FrequenciaOpts.MENSAL:
                    if (now.Day > conf.DiaMensal || (now.Day == conf.DiaMensal && now.Hour > conf.Horas) ||
                        (now.Day == conf.DiaMensal && now.Hour == conf.Horas && now.Minute >= conf.Minutos))
                    {
                        DateTime res = now.AddMonths(1);
                        return new DateTime(res.Year, res.Month, conf.DiaMensal, conf.Horas, conf.Minutos, 0);
                    }
                    else
                    {
                        return new DateTime(now.Year, now.Month, conf.DiaMensal, conf.Horas, conf.Minutos, 0);
                    }
                case ExecucaoAutomatica.FrequenciaOpts.DIARIA:
                    if (now.Hour > conf.Horas || (now.Hour == conf.Horas && now.Minute >= conf.Minutos))
                    {
                        DateTime res = now.AddDays(1);
                        return new DateTime(res.Year, res.Month, res.Day, conf.Horas, conf.Minutos, 0);
                    }
                    else
                    {
                        return new DateTime(now.Year, now.Month, now.Day, conf.Horas, conf.Minutos, 0);
                    }
                case ExecucaoAutomatica.FrequenciaOpts.DIAS_SEMANA:
                    if (conf.DiasSemanaActivo[(int)now.DayOfWeek])
                    {
                        if (!(now.Hour > conf.Horas || (now.Hour == conf.Horas && now.Minute >= conf.Minutos)))
                        {
                            return new DateTime(now.Year, now.Month, now.Day, conf.Horas, conf.Minutos, 0);
                        }
                    }


                    int numCiclo = 1;
                    for (int i = (int)now.DayOfWeek; numCiclo < 10; numCiclo++)
                    {
                        i++;
                        if (i > 6)
                        {
                            i = 0;
                        }

                        if (conf.DiasSemanaActivo[i])
                        {
                            DateTime res = now.AddDays(numCiclo);
                            return new DateTime(res.Year, res.Month, res.Day, conf.Horas, conf.Minutos, 0);
                        }
                    }

                    return DateTime.MinValue;
                default:
                    return DateTime.MinValue;
            }

        }
    }
}
