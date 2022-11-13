using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SoftBackupCore;
using Message = SoftBackupCore.Message;

namespace SoftBackup
{

    public class Janela : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button BotaoCopiar;
        public System.Windows.Forms.TextBox ConsolaBox;
		public System.Windows.Forms.Button CancelarBotao;
		public System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.ComponentModel.IContainer components;

        //private System.Windows.Forms.NotifyIcon s_TrayIcon;
        //private System.Windows.Forms.ContextMenu ctxMenu;
#if DEBUG
        public const int UDP_PORT = 54877;
#else
        public const int UDP_PORT = 54878;
#endif
        public Thread TMyListener;
        private MyListener myListener;
        private System.Windows.Forms.Button AdicionarBotao;
		private System.Windows.Forms.Button RemoverBotao;
        private System.Windows.Forms.CheckBox CheckModificados;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
        private Button buttonEditarBackup;
        private ListBox ListaPath;
        private CheckBox chkBLogDetalhe;
        private CheckBox chkBLogModificados;
        private Button btnAutomatizacao;
        private Button LimparConsolaBtn;
        private Label lblEstadoBackupsAutomaticos;
        private Button btnExecutarSelecionado;
        private Label lblBackupDates;
        public Configuracao Configuracao { get; set; }
        private Button btnMostrarLabel;
        private Button btnUpEntry;
        private Button btnDownEntry;
        private Button VerLogFileBtn;
        private bool IsLoadingConfiguration { get; set; }
        public bool IsInstanceInitialized { get; set; }
        public SoftBackupClient SoftBackupInstance { get; set; }
        private string CurrentLogFile { get; set; }
        private long CurrentLogFilePosition { get; set; }
        private bool WasLastLogDisplayed { get; set; } = true;
        public Janela(bool minimizada = false)
		{
			
			InitializeComponent();
            
            Control.CheckForIllegalCrossThreadCalls = false;

            if (minimizada)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Hide();
            }
            
            SoftBackupClient.LoadInstance(this.ProcessMessageFromServer);
            SoftBackupInstance = SoftBackupClient.Instance;
            
            myListener = new MyListener(this);
            TMyListener = new Thread(new ThreadStart(myListener.Executa));
            TMyListener.Start();
            
            if (!SoftBackupInstance.ConnService.ConnectedToServer)
            {
                var thrStatus = new Thread(new ThreadStart(this.ServiceStatusDown));
                thrStatus.Start();
            }
            else
            {
                CarregaConfiguracao();
                SendGetStatusRequest();
            }
            
        }

        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
            if(myListener != null)
            {
                myListener.CloseListener();
            }
            if (TMyListener != null)
            {
                TMyListener.Abort();
            }
            if (SoftBackupInstance != null)
            {
                SoftBackupInstance.Terminate();
            }

        }


		//private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		//{
		//	this.ShowInTaskbar = false;
		//	this.Visible = false;
		//
		//	e.Cancel = true;
		//}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Janela));
            this.BotaoCopiar = new System.Windows.Forms.Button();
            this.ConsolaBox = new System.Windows.Forms.TextBox();
            this.CancelarBotao = new System.Windows.Forms.Button();
            this.AdicionarBotao = new System.Windows.Forms.Button();
            this.RemoverBotao = new System.Windows.Forms.Button();
            this.CheckModificados = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonEditarBackup = new System.Windows.Forms.Button();
            this.LimparConsolaBtn = new System.Windows.Forms.Button();
            this.ListaPath = new System.Windows.Forms.ListBox();
            this.chkBLogDetalhe = new System.Windows.Forms.CheckBox();
            this.chkBLogModificados = new System.Windows.Forms.CheckBox();
            this.btnAutomatizacao = new System.Windows.Forms.Button();
            this.lblEstadoBackupsAutomaticos = new System.Windows.Forms.Label();
            this.btnExecutarSelecionado = new System.Windows.Forms.Button();
            this.lblBackupDates = new System.Windows.Forms.Label();
            this.btnMostrarLabel = new System.Windows.Forms.Button();
            this.btnUpEntry = new System.Windows.Forms.Button();
            this.btnDownEntry = new System.Windows.Forms.Button();
            this.VerLogFileBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BotaoCopiar
            // 
            this.BotaoCopiar.Location = new System.Drawing.Point(64, 24);
            this.BotaoCopiar.Name = "BotaoCopiar";
            this.BotaoCopiar.Size = new System.Drawing.Size(176, 23);
            this.BotaoCopiar.TabIndex = 2;
            this.BotaoCopiar.Text = "Iniciar Backup";
            this.BotaoCopiar.Click += new System.EventHandler(this.BotaoIniciarBackup_Click);
            // 
            // ConsolaBox
            // 
            this.ConsolaBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConsolaBox.Location = new System.Drawing.Point(48, 288);
            this.ConsolaBox.MaxLength = 1000000;
            this.ConsolaBox.Multiline = true;
            this.ConsolaBox.Name = "ConsolaBox";
            this.ConsolaBox.ReadOnly = true;
            this.ConsolaBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsolaBox.Size = new System.Drawing.Size(924, 226);
            this.ConsolaBox.TabIndex = 0;
            // 
            // CancelarBotao
            // 
            this.CancelarBotao.Location = new System.Drawing.Point(64, 74);
            this.CancelarBotao.Name = "CancelarBotao";
            this.CancelarBotao.Size = new System.Drawing.Size(176, 23);
            this.CancelarBotao.TabIndex = 11;
            this.CancelarBotao.Text = "Cancelar Backup em curso";
            this.CancelarBotao.Click += new System.EventHandler(this.CancelarBackupEmCurso_Click);
            // 
            // AdicionarBotao
            // 
            this.AdicionarBotao.Location = new System.Drawing.Point(310, 204);
            this.AdicionarBotao.Name = "AdicionarBotao";
            this.AdicionarBotao.Size = new System.Drawing.Size(112, 23);
            this.AdicionarBotao.TabIndex = 13;
            this.AdicionarBotao.Text = "Adicionar Path";
            this.AdicionarBotao.Click += new System.EventHandler(this.AdicionarBotao_Click);
            // 
            // RemoverBotao
            // 
            this.RemoverBotao.Location = new System.Drawing.Point(428, 204);
            this.RemoverBotao.Name = "RemoverBotao";
            this.RemoverBotao.Size = new System.Drawing.Size(96, 23);
            this.RemoverBotao.TabIndex = 14;
            this.RemoverBotao.Text = "RemoverPath";
            this.RemoverBotao.Click += new System.EventHandler(this.RemoverBotao_Click);
            // 
            // CheckModificados
            // 
            this.CheckModificados.Checked = true;
            this.CheckModificados.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckModificados.Location = new System.Drawing.Point(64, 116);
            this.CheckModificados.Name = "CheckModificados";
            this.CheckModificados.Size = new System.Drawing.Size(192, 24);
            this.CheckModificados.TabIndex = 16;
            this.CheckModificados.Text = "Apenas ficheiros modificados";
            this.CheckModificados.CheckedChanged += new System.EventHandler(this.ConfChecksChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SoftBackup";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
            // 
            // buttonEditarBackup
            // 
            this.buttonEditarBackup.Location = new System.Drawing.Point(530, 204);
            this.buttonEditarBackup.Name = "buttonEditarBackup";
            this.buttonEditarBackup.Size = new System.Drawing.Size(117, 23);
            this.buttonEditarBackup.TabIndex = 17;
            this.buttonEditarBackup.Text = "Editar Selecionado";
            this.buttonEditarBackup.UseVisualStyleBackColor = true;
            this.buttonEditarBackup.Click += new System.EventHandler(this.buttonEditarBackup_Click);
            // 
            // LimparConsolaBtn
            // 
            this.LimparConsolaBtn.Location = new System.Drawing.Point(897, 259);
            this.LimparConsolaBtn.Name = "LimparConsolaBtn";
            this.LimparConsolaBtn.Size = new System.Drawing.Size(75, 23);
            this.LimparConsolaBtn.TabIndex = 18;
            this.LimparConsolaBtn.Text = "Limpar";
            this.LimparConsolaBtn.UseVisualStyleBackColor = true;
            this.LimparConsolaBtn.Click += new System.EventHandler(this.LimparConsolaBtn_Click);
            // 
            // ListaPath
            // 
            this.ListaPath.FormattingEnabled = true;
            this.ListaPath.Location = new System.Drawing.Point(310, 24);
            this.ListaPath.Name = "ListaPath";
            this.ListaPath.Size = new System.Drawing.Size(662, 173);
            this.ListaPath.TabIndex = 19;
            this.ListaPath.DoubleClick += new System.EventHandler(this.ListaPath_DoubleClick);
            // 
            // chkBLogDetalhe
            // 
            this.chkBLogDetalhe.AutoSize = true;
            this.chkBLogDetalhe.Location = new System.Drawing.Point(64, 180);
            this.chkBLogDetalhe.Name = "chkBLogDetalhe";
            this.chkBLogDetalhe.Size = new System.Drawing.Size(147, 17);
            this.chkBLogDetalhe.TabIndex = 20;
            this.chkBLogDetalhe.Text = "Log completo e descritivo";
            this.chkBLogDetalhe.UseVisualStyleBackColor = true;
            this.chkBLogDetalhe.CheckedChanged += new System.EventHandler(this.ConfChecksChanged);
            // 
            // chkBLogModificados
            // 
            this.chkBLogModificados.AutoSize = true;
            this.chkBLogModificados.Location = new System.Drawing.Point(64, 150);
            this.chkBLogModificados.Name = "chkBLogModificados";
            this.chkBLogModificados.Size = new System.Drawing.Size(141, 17);
            this.chkBLogModificados.TabIndex = 21;
            this.chkBLogModificados.Text = "Log apenas modificados";
            this.chkBLogModificados.UseVisualStyleBackColor = true;
            this.chkBLogModificados.CheckedChanged += new System.EventHandler(this.ConfChecksChanged);
            // 
            // btnAutomatizacao
            // 
            this.btnAutomatizacao.Location = new System.Drawing.Point(847, 204);
            this.btnAutomatizacao.Name = "btnAutomatizacao";
            this.btnAutomatizacao.Size = new System.Drawing.Size(124, 23);
            this.btnAutomatizacao.TabIndex = 22;
            this.btnAutomatizacao.Text = "Backups Automaticos";
            this.btnAutomatizacao.UseVisualStyleBackColor = true;
            this.btnAutomatizacao.Click += new System.EventHandler(this.btnAutomatizacao_Click);
            // 
            // lblEstadoBackupsAutomaticos
            // 
            this.lblEstadoBackupsAutomaticos.AutoSize = true;
            this.lblEstadoBackupsAutomaticos.Location = new System.Drawing.Point(787, 230);
            this.lblEstadoBackupsAutomaticos.Name = "lblEstadoBackupsAutomaticos";
            this.lblEstadoBackupsAutomaticos.Size = new System.Drawing.Size(180, 13);
            this.lblEstadoBackupsAutomaticos.TabIndex = 23;
            this.lblEstadoBackupsAutomaticos.Text = "(backups automaticos desactivados)";
            // 
            // btnExecutarSelecionado
            // 
            this.btnExecutarSelecionado.Location = new System.Drawing.Point(723, 204);
            this.btnExecutarSelecionado.Name = "btnExecutarSelecionado";
            this.btnExecutarSelecionado.Size = new System.Drawing.Size(118, 23);
            this.btnExecutarSelecionado.TabIndex = 24;
            this.btnExecutarSelecionado.Text = "Backup Selecionado";
            this.btnExecutarSelecionado.UseVisualStyleBackColor = true;
            this.btnExecutarSelecionado.Click += new System.EventHandler(this.btnExecutarSelecionado_Click);
            // 
            // lblBackupDates
            // 
            this.lblBackupDates.AutoSize = true;
            this.lblBackupDates.Location = new System.Drawing.Point(48, 269);
            this.lblBackupDates.Name = "lblBackupDates";
            this.lblBackupDates.Size = new System.Drawing.Size(67, 13);
            this.lblBackupDates.TabIndex = 25;
            this.lblBackupDates.Text = "Last Backup";
            // 
            // btnMostrarLabel
            // 
            this.btnMostrarLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrarLabel.Location = new System.Drawing.Point(975, 24);
            this.btnMostrarLabel.Name = "btnMostrarLabel";
            this.btnMostrarLabel.Size = new System.Drawing.Size(20, 73);
            this.btnMostrarLabel.TabIndex = 26;
            this.btnMostrarLabel.Text = "Path";
            this.btnMostrarLabel.UseVisualStyleBackColor = true;
            this.btnMostrarLabel.Click += new System.EventHandler(this.BtnMostrarLabel_Click);
            // 
            // btnUpEntry
            // 
            this.btnUpEntry.Image = ((System.Drawing.Image)(resources.GetObject("btnUpEntry.Image")));
            this.btnUpEntry.Location = new System.Drawing.Point(975, 144);
            this.btnUpEntry.Name = "btnUpEntry";
            this.btnUpEntry.Size = new System.Drawing.Size(23, 23);
            this.btnUpEntry.TabIndex = 27;
            this.btnUpEntry.UseVisualStyleBackColor = true;
            this.btnUpEntry.Click += new System.EventHandler(this.BtnUpEntry_Click);
            // 
            // btnDownEntry
            // 
            this.btnDownEntry.Image = ((System.Drawing.Image)(resources.GetObject("btnDownEntry.Image")));
            this.btnDownEntry.Location = new System.Drawing.Point(975, 173);
            this.btnDownEntry.Name = "btnDownEntry";
            this.btnDownEntry.Size = new System.Drawing.Size(23, 23);
            this.btnDownEntry.TabIndex = 28;
            this.btnDownEntry.UseVisualStyleBackColor = true;
            this.btnDownEntry.Click += new System.EventHandler(this.BtnDownEntry_Click);
            // 
            // VerLogFileBtn
            // 
            this.VerLogFileBtn.Location = new System.Drawing.Point(766, 259);
            this.VerLogFileBtn.Name = "VerLogFileBtn";
            this.VerLogFileBtn.Size = new System.Drawing.Size(75, 23);
            this.VerLogFileBtn.TabIndex = 29;
            this.VerLogFileBtn.Text = "Log File";
            this.VerLogFileBtn.UseVisualStyleBackColor = true;
            this.VerLogFileBtn.Click += new System.EventHandler(this.VerLogFileBtn_Click);
            // 
            // Janela
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1004, 526);
            this.Controls.Add(this.VerLogFileBtn);
            this.Controls.Add(this.btnDownEntry);
            this.Controls.Add(this.btnUpEntry);
            this.Controls.Add(this.btnMostrarLabel);
            this.Controls.Add(this.lblBackupDates);
            this.Controls.Add(this.btnExecutarSelecionado);
            this.Controls.Add(this.lblEstadoBackupsAutomaticos);
            this.Controls.Add(this.btnAutomatizacao);
            this.Controls.Add(this.chkBLogModificados);
            this.Controls.Add(this.chkBLogDetalhe);
            this.Controls.Add(this.ListaPath);
            this.Controls.Add(this.LimparConsolaBtn);
            this.Controls.Add(this.buttonEditarBackup);
            this.Controls.Add(this.CheckModificados);
            this.Controls.Add(this.RemoverBotao);
            this.Controls.Add(this.AdicionarBotao);
            this.Controls.Add(this.CancelarBotao);
            this.Controls.Add(this.ConsolaBox);
            this.Controls.Add(this.BotaoCopiar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Janela";
            this.Text = "SoftBackup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingCallback);
            this.Load += new System.EventHandler(this.Janela_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
#endregion

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool instanceCountOne = false;
#if DEBUG
            string mutexName = "DEBUGSoftBackup";
#else
            string mutexName = "SoftBackup";
#endif
            
            using (Mutex mtex = new Mutex(true, mutexName, out instanceCountOne))
            {
                if (instanceCountOne)
                {
                    Janela janela = new Janela(args != null && args.Length > 0 && args[0] == "true");
                    janela.IsInstanceInitialized = true;
                    Application.Run(janela);
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    try
                    {
                        IntPtr handle = FindWindow(null, mutexName);
                        long val = handle.ToInt64();
                        if (handle != null && handle != IntPtr.Zero)
                        {
                            SetForegroundWindow(handle);
                        }

                        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), UDP_PORT);
                        socket.SendTo(Encoding.ASCII.GetBytes("OPEN_SOFTBACKUP"), endpoint);

                    }
                    catch (Exception) { }
                }
            }
        }

        private void Janela_Load(object sender, System.EventArgs e)
		{
		}

        public void ConsolaAppendCustomText(string texto, bool showTimeStamp)
        {
            ConsolaBox.AppendText((showTimeStamp ? DateTime.Now.ToString("[MMM-dd HH:mm:ss]").ToUpper() + " " : "") + texto);
        }

        public void ConsolaAppendLog(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                ConsolaBox.AppendText(texto);
            }
        }

        public void ServiceStatusDown()
        {
            
            while (true)
            {
                var result = MessageBox.Show("Não foi possivel ligar-se ao Serviço de Backup. Tentar nova ligação?", "Erro FATAL",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.OK && result != DialogResult.Yes)
                {
                    Process.GetCurrentProcess().Kill();
                }

                bool connected = SoftBackupInstance.ConnService.Reconnect();
                
                if (! connected)
                {
                    continue;
                }
                else
                {
                    CarregaConfiguracao();
                    SendGetStatusRequest();
                    return;
                }
            }


        }

        public void ProcessMessageFromServer(Message message)
        {
            switch (message.Type)
            {
                case Message.NotificationType.NOTIFY_ERROR_BACKUP:
                    UpdateApplicationStatus(message);
                    NotificarErro();
                    break;
                case Message.NotificationType.NOTIFY_STATUS:
                    UpdateApplicationStatus(message);
                    break;
                case Message.NotificationType.NOTIFY_CONFIGURATION_UPDATED:
                    UpdateApplicationStatus(message);
                    UpdateEditConfButtonsStatus(false);
                    CarregaConfiguracao();
                    break;
                case Message.NotificationType.NOTIFY_LOG:
                    UpdateLogDataInfo(message);
                    break;
                case Message.NotificationType.START_BACKUP_RESPONSE:
                    UpdateApplicationStatus(message);
                    if (!message.IsRequestSuccess)
                    {
                        ConsolaBox.Text += $"Não foi possível iniciar o Backup. {message.Error}";
                    }
                    break;
                case Message.NotificationType.STOP_BACKUP_RESPONSE:
                    UpdateApplicationStatus(message);
                    if (!message.IsRequestSuccess)
                    {
                        ConsolaBox.Text += $"Não foi possível fazer STOP o Backup. {message.Error}";
                    }
                    break;
                case Message.NotificationType.SAVE_CONFIGURATION_RESPONSE:
                    UpdateApplicationStatus(message);
                    if (!message.IsRequestSuccess)
                    {
                        ConsolaBox.Text += $"Não foi possível Salvar a configuração. {message.Error}";
                    }
                    UpdateEditConfButtonsStatus(false);
                    break;
                    
            }
        }

        private void UpdateApplicationStatus(Message message)
        {
            UpdateBackupsButtonsStatus(message.ServiceStatus.IsExecutingBackup);
            UpdateBackupDatesDisplayed(message.ServiceStatus);
        }

        private void UpdateBackupsButtonsStatus(bool isExecutingBackup)
        {
            lock (this)
            {
                BotaoCopiar.Enabled = !isExecutingBackup;
                CancelarBotao.Enabled = isExecutingBackup;
                ListaPath.Enabled = !isExecutingBackup;
                RemoverBotao.Enabled = !isExecutingBackup;
                AdicionarBotao.Enabled = !isExecutingBackup;
                CheckModificados.Enabled = !isExecutingBackup;
                buttonEditarBackup.Enabled = !isExecutingBackup;
                btnAutomatizacao.Enabled = !isExecutingBackup;
                btnExecutarSelecionado.Enabled = !isExecutingBackup;
            }
        }

        private void UpdateEditConfButtonsStatus(bool isSavingConf)
        {
            if (isSavingConf)
            {
                ListaPath.Enabled = false;
                AdicionarBotao.Enabled = false;
                RemoverBotao.Enabled = false;
                btnExecutarSelecionado.Enabled = false;
                lblEstadoBackupsAutomaticos.Visible = !Configuracao.Automatizacao.Activa;
            }
            else
            {
                ListaPath.Enabled = true;
                AdicionarBotao.Enabled = true;
                RemoverBotao.Enabled = true;
                btnExecutarSelecionado.Enabled = true;
                lblEstadoBackupsAutomaticos.Visible = !Configuracao.Automatizacao.Activa;
            }
        }

        private void UpdateLogDataInfo(Message msg)
        {
            if (!IsInstanceInitialized || string.IsNullOrEmpty(msg.LogData)) return;
            
            string[] aux = msg.LogData.Replace("\r", "").Split('\n');
            
            foreach (var s in aux)
            {
                bool isCurrentLogDebug = s.IndexOf("[DEBUG]") >= 0;
                bool isCurrentLogFileNotModified = s.IndexOf("[NM]") >= 0;

                if (s.Trim().Length == 0)
                {
                    if (WasLastLogDisplayed)
                    {
                        ConsolaAppendLog("\r\n");
                    }
                    WasLastLogDisplayed = true;
                    continue;
                }
                
                if (isCurrentLogFileNotModified && chkBLogModificados.Checked){
                    WasLastLogDisplayed = false;
                    continue;
                }
                else if(isCurrentLogDebug && !chkBLogDetalhe.Checked)
                {
                    WasLastLogDisplayed = false;
                    continue;
                }
                else if(!WasLastLogDisplayed && s.IndexOf(" CONCLU") == 0)
                {
                    WasLastLogDisplayed = false;
                    continue;
                }
                else
                {
                    WasLastLogDisplayed = true;
                    ConsolaAppendLog(s.Replace("[DEBUG]", "").Replace("[NM]", ""));
                }
            }

        }    

		private void BotaoIniciarBackup_Click(object sender, System.EventArgs e)
		{
            SendStartBackupRequest();
		}

        private void SendStartBackupRequest(BackupEntry[] entries = null)
        {
            var req = new Message();
            req.Type = Message.NotificationType.CLIENT_START_BACKUP;
            req.BackupEntries = entries == null ? null : new List<BackupEntry>(entries);
            UpdateBackupsButtonsStatus(true);
            SendRequestMessage(req);
        }

        private void SendAbortBackupRequest()
        {
            var req = new Message();
            req.Type = Message.NotificationType.CLIENT_STOP_BACKUP;
            SendRequestMessage(req);
        }

        private void SendSaveConfiguracaoRequest(Configuracao config)
        {
            var req = new Message();
            req.Type = Message.NotificationType.CLIENT_SAVE_CONFIGURATION;
            req.Configuracao = config;
            UpdateEditConfButtonsStatus(true);
            SendRequestMessage(req);
        }

        private void SendSaveConfiguracaoClientSide()
        {
            var req = new Message();
            req.Type = Message.NotificationType.CLIENT_SAVE_CLIENTCONFIGURATION;
            req.Configuracao = new Configuracao();
            req.Configuracao.LogApenasModificados = chkBLogModificados.Checked;
            req.Configuracao.LogTotal = chkBLogDetalhe.Checked;
            SendRequestMessage(req);
        }

        private void SendGetStatusRequest()
        {
            var req = new Message();
            req.Type = Message.NotificationType.CLIENT_GET_STATUS;
            SendRequestMessage(req);
        }

        private void SendRequestMessage(Message msg)
        {
            bool tryReconnect = false;

            Start:
            try
            {
                SoftBackupInstance.SendMessageToServer(msg);
            } 
            catch(Exception e)
            {
                if (!tryReconnect)
                {
                    SoftBackupInstance.ConnService.Reconnect();
                    tryReconnect = true;
                    goto Start;
                }
                if (msg.Type == Message.NotificationType.CLIENT_START_BACKUP)
                {
                    UpdateBackupsButtonsStatus(false);
                }
                MessageBox.Show("ERRO FATAL!! Serviço não está a responder!! " + e.Message);
            }
        }

        private void CancelarBackupEmCurso_Click(object sender, System.EventArgs e)
		{
            SendAbortBackupRequest();
		}

        public void NotificarErro()
        {
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon1.BalloonTipText = "Aconteceu um erro a efectuar o Backup. Ver os Logs.";
            notifyIcon1.BalloonTipTitle = "Erro no Backup";
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000000);
            OpenFromHide();
            Activate();
            ConsolaBox.AppendText("\r\n\r\n ******* ERRO NO BACKUP!!! ******* VER OS LOGS ******* \r\n\r\n");
        }

        public void EntryAdded(BackupEntry be)
        {
            if(be.Id == null || be.Id == "")
            {
                be.Id = Guid.NewGuid().ToString().Replace("-", "");
                ListaPath.Items.Add(be);
            }
            else
            {
                int index = ListaPath.Items.IndexOf(be);
                ListaPath.Items.RemoveAt(index);
                ListaPath.Items.Insert(index, be);
            }

            GuardarConfiguracao();
        }

		private void AdicionarBotao_Click(object sender, System.EventArgs e)
		{

            EditBackupEntry form = new EditBackupEntry(this, null);
            form.ShowDialog();
            
        }

		private void buttonEditarBackup_Click(object sender, EventArgs e)
        {
            if(ListaPath.SelectedItem != null){
                EditBackupEntry form = new EditBackupEntry(this, (BackupEntry)ListaPath.SelectedItem);
                form.ShowDialog();
            }
        }

        private void RemoverBotao_Click(object sender, System.EventArgs e)
		{
            if (MessageBox.Show("Confirmar apagar a entrada selecionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ListaPath.Items.Remove(ListaPath.SelectedItem);
                GuardarConfiguracao();
            }
		}

        public void UpdateBackupDatesDisplayed(ServiceStatus status)
        {
            SoftBackupInstance.NextRun = status.NextRun;
            SoftBackupInstance.LastRun = status.LastRun;
            DateTime nextRun = SoftBackupInstance.NextRun;
            string txt = nextRun == DateTime.MinValue ? "" : ("Prox. Backup: " + nextRun.ToString("yyyy-MM-dd HH:mm") + "   ");
            txt += "Ultimo backup: " + (status.LastRun.Year == 1 ? "(nunca)" : status.LastRun.ToString("yyyy-MM-dd HH:mm"));
            lblBackupDates.Text = txt;
        }

		private void CarregaConfiguracao()
		{

            IsLoadingConfiguration = true;

            Configuracao = SoftBackupInstance.LoadConfiguracao();
            BackupEntry[] entries = Configuracao.BackupEntries.ToArray();
            CheckModificados.Checked = Configuracao.BackupApenasModificados;
            chkBLogModificados.Checked = Configuracao.LogApenasModificados;
            chkBLogDetalhe.Checked = Configuracao.LogTotal;
            ListaPath.Items.Clear();
            foreach (BackupEntry be in entries)
            {
                ListaPath.Items.Add(be);
            }

            lblEstadoBackupsAutomaticos.Visible = !Configuracao.Automatizacao.Activa;
            
            IsLoadingConfiguration = false;
            
        }

        public void GuardarConfiguracao()
        {
            if (IsLoadingConfiguration)
            {
                return;
            }

            BackupEntry[] entries = new BackupEntry[ListaPath.Items.Count];

            int i = 0;
            foreach (BackupEntry be in ListaPath.Items)
            {
                entries[i++] = be;
            }

            Configuracao.BackupEntries = new List<BackupEntry>(entries);
            Configuracao.BackupApenasModificados = CheckModificados.Checked;
            Configuracao.LogApenasModificados = chkBLogModificados.Checked;
            Configuracao.LogTotal = chkBLogDetalhe.Checked;

            SendSaveConfiguracaoRequest(Configuracao);

        }

        private delegate void ConsolaAppendTexto(string texto);

        private void ConfChecksChanged(object sender, System.EventArgs e)
		{
            if(sender.GetType() == typeof(CheckBox))
            {
                CheckBox cb = (CheckBox)sender;
                if(cb.Name == "chkBLogModificados")
                {
                    if (chkBLogModificados.Checked)
                    {
                        chkBLogDetalhe.Checked = false;
                    }
                }
                else if (cb.Name == "chkBLogDetalhe")
                {
                    if (chkBLogDetalhe.Checked)
                    {
                        chkBLogModificados.Checked = false;
                    }
                }
                else if(cb.Name == "CheckModificados")
                {
                    if (!IsLoadingConfiguration)
                    {
                        GuardarConfiguracao();
                    }
                    return;
                }
            }

            if (!IsLoadingConfiguration)
            {
                SendSaveConfiguracaoClientSide();
            }
		}

        private void FormClosingCallback(object sender, FormClosingEventArgs e)
		{

            /*if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                var result = MessageBox.Show("Confirmar fechar SoftBackup? Os backups NÃO irão correr.", "Fechar SoftBackup",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

                e.Cancel = (result == DialogResult.No);
            }
            if (!e.Cancel)
            {
                GuardarConfiguracao();
            }*/


		}

        private void LimparConsolaBtn_Click(object sender, EventArgs e)
        {
            ConsolaBox.Text = "";
        }

        private void ListaPath_DoubleClick(object sender, EventArgs e)
        {
            if (ListaPath.SelectedItem != null)
            {
                EditBackupEntry form = new EditBackupEntry(this, (BackupEntry)ListaPath.SelectedItem);
                form.ShowDialog();
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        public void OpenFromHide()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                //notifyIcon1.Visible = true;
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                //notifyIcon1.Visible = false;
            }
        }

        public bool MostrarDetalhe()
        {
            return chkBLogDetalhe.Checked;
        }

        private void btnAutomatizacao_Click(object sender, EventArgs e)
        {
            Automatizacao auto = new Automatizacao(this);
            auto.ShowDialog();
        }

        private void btnExecutarSelecionado_Click(object sender, EventArgs e)
        {
            if (ListaPath.SelectedItem != null)
            {
                BackupEntry be = (BackupEntry)ListaPath.SelectedItem;
                SendStartBackupRequest(new BackupEntry[] {be});
            }
        }

        private void BtnMostrarLabel_Click(object sender, EventArgs e)
        {
            Configuracao.MOSTRAR_LABEL = !Configuracao.MOSTRAR_LABEL;
            if (Configuracao.MOSTRAR_LABEL)
            {
                btnMostrarLabel.Text = "Path";
            }
            else
            {
                btnMostrarLabel.Text = "Label";
            }

            List<BackupEntry> entriesAux = new List<BackupEntry>(ListaPath.Items.Count);
            foreach(BackupEntry be in ListaPath.Items)
            {
                entriesAux.Add(be);
            }
            ListaPath.Items.Clear();
            ListaPath.Items.AddRange(entriesAux.ToArray());
            
        }

        private void BtnUpEntry_Click(object sender, EventArgs e)
        {
            if (ListaPath.SelectedItem != null && ListaPath.SelectedIndex > 0)
            {
                var selected = ListaPath.SelectedItem;
                var above = ListaPath.Items[ListaPath.SelectedIndex - 1];
                var selectedIndex = ListaPath.SelectedIndex;
                ListaPath.Items[selectedIndex - 1] = selected;
                ListaPath.Items[selectedIndex] = above;
                ListaPath.SelectedItem = selected;
            }
        }

        private void BtnDownEntry_Click(object sender, EventArgs e)
        {
            if (ListaPath.SelectedItem != null && ListaPath.SelectedIndex < ListaPath.Items.Count - 1)
            {
                var selected = ListaPath.SelectedItem;
                var bellow = ListaPath.Items[ListaPath.SelectedIndex + 1];
                var selectedIndex = ListaPath.SelectedIndex;
                ListaPath.Items[selectedIndex + 1] = selected;
                ListaPath.Items[selectedIndex] = bellow;
                ListaPath.SelectedItem = selected;
            }
        }

        private void VerLogFileBtn_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(SoftBackupInstance.GetPathFileLog());
            if (Directory.Exists(fi.DirectoryName))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = fi.DirectoryName,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("Directoria nao encontrada: {0}", fi.DirectoryName));
            }
        }

    }

    public class MyListener
    {
        private Janela janela;
        UdpClient listener;

        public MyListener(Janela janela)
        {
            this.janela = janela;
        }

        public void Executa()
        {
            listener = new UdpClient(Janela.UDP_PORT);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, Janela.UDP_PORT);
            while (true)
            {
                byte[] byteArray = listener.Receive(ref groupEP);
                string data = Encoding.ASCII.GetString(byteArray, 0, byteArray.Length);
                if(data.ToUpper() == "OPEN_SOFTBACKUP")
                {
                    janela.OpenFromHide();
                }
            }
        }

        public void CloseListener()
        {
            if(listener != null)
            {
                listener.Close();
            }
        }
    }

}
