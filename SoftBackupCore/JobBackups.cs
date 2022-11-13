using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SoftBackupCore
{
    public class JobBackups
    {
        private bool Closed;
        private NextRunJob NextRunJ { get; set; }
        private const int MILLISECONDS_HOUR = 1000 * 60 * 60;
        public void Executa()
        {
            Closed = false;
            SoftBackupServer.LoadInstance(this);

            while (true)
            {
                try
                {
                    SoftBackupServer.Instance.ClearOldLogFiles();
                    if (NextRunJ != null)
                    {
                        NextRunJ.Terminate();
                    }
                    Configuracao configuracao = SoftBackupServer.Instance.LoadConfiguracao();
                    SoftBackupServer.Instance.NextRun = SoftBackupServer.Instance.CalculateNextRun(configuracao);
                    SoftBackupServer.Instance.LastRun = configuracao.LastRun;
                    SoftBackupServer.Instance.SendStatusToClient();
                    NextRunJ = new NextRunJob(SoftBackupServer.Instance.NextRun);
                    NextRunJ.StartThread();
                    lock (this)
                    {
                        Monitor.Wait(this, MILLISECONDS_HOUR);
                    }
                }
                catch (Exception) 
                {
                    if (Closed)
                    {
                        return;
                    }
                }
            }
        }

        public void ReCalculateNextRun()
        {
            lock (this)
            {
                Monitor.PulseAll(this);
            }
        }

        public void Terminate()
        {
            Closed = true;
            if (NextRunJ != null)
            {
                NextRunJ.Terminate();
            }
            if(SoftBackupServer.Instance != null)
            {
                SoftBackupServer.Instance.Terminate();
            }
        }

        class NextRunJob
        {
            private DateTime NextRun { get; set; }
            private Thread ThreadJob { get; set; }
            public NextRunJob(DateTime nextRun)
            {
                NextRun = nextRun;
                ThreadJob = new Thread(new ThreadStart(this.Executa));
            }

            public void StartThread()
            {
                ThreadJob.Start();
            }

            public void Executa()
            {
                try
                {
                    if (NextRun == DateTime.MinValue)
                    {
                        return;
                    }

                    DateTime now = DateTime.Now;
                    if (NextRun > now)
                    {
                        Thread.Sleep(NextRun.Subtract(now));
                    }

                    SoftBackupServer.Instance.IniciaBackup(false);
                }
                catch (Exception) { }

            }

            public void Terminate()
            {
                try
                {
                    ThreadJob.Abort();
                }
                catch (Exception) { }
            }


        }
    }
}
