using SoftBackupCore;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace SoftBackupService
{
    public partial class SoftBackupService : ServiceBase
    {

        private Thread TJobBackups;
        private JobBackups JobBackups;
        public SoftBackupService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            JobBackups = new JobBackups();
            TJobBackups = new Thread(new ThreadStart(JobBackups.Executa));
            TJobBackups.Start();
        }

        protected override void OnStop()
        {
            try
            {
                JobBackups.Terminate();
                TJobBackups.Abort();
            }
            catch (Exception) { }
        }

    }
}
