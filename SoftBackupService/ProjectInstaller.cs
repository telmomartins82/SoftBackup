using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SoftBackupService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            var ctl = new ServiceController(serviceInstaller1.ServiceName);
            if (ctl.Status == ServiceControllerStatus.Running)
            {
                ctl.Stop();
            }
            if (ctl.Status == ServiceControllerStatus.Stopped)
            {
                ctl.Start();
            }
            
        }
    }
}
