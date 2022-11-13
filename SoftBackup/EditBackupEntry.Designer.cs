namespace SoftBackup
{
    partial class EditBackupEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBackupEntry));
            this.label1 = new System.Windows.Forms.Label();
            this.PathOrigem = new System.Windows.Forms.TextBox();
            this.buttonProcurarOrigemFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.PathDestino = new System.Windows.Forms.TextBox();
            this.buttonProcurarDestino = new System.Windows.Forms.Button();
            this.buttonSalvar = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonProcurarOrigemFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxApagar = new System.Windows.Forms.CheckBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.Excepcoes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLabel = new System.Windows.Forms.Label();
            this.boxLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path de Origem:";
            // 
            // PathOrigem
            // 
            this.PathOrigem.Location = new System.Drawing.Point(32, 117);
            this.PathOrigem.Name = "PathOrigem";
            this.PathOrigem.Size = new System.Drawing.Size(725, 20);
            this.PathOrigem.TabIndex = 1;
            // 
            // buttonProcurarOrigemFolder
            // 
            this.buttonProcurarOrigemFolder.Location = new System.Drawing.Point(32, 143);
            this.buttonProcurarOrigemFolder.Name = "buttonProcurarOrigemFolder";
            this.buttonProcurarOrigemFolder.Size = new System.Drawing.Size(100, 23);
            this.buttonProcurarOrigemFolder.TabIndex = 2;
            this.buttonProcurarOrigemFolder.Text = "Procurar pasta...";
            this.buttonProcurarOrigemFolder.UseVisualStyleBackColor = true;
            this.buttonProcurarOrigemFolder.Click += new System.EventHandler(this.buttonProcurarOrigemFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path de Destino:";
            // 
            // PathDestino
            // 
            this.PathDestino.Location = new System.Drawing.Point(32, 206);
            this.PathDestino.Name = "PathDestino";
            this.PathDestino.Size = new System.Drawing.Size(725, 20);
            this.PathDestino.TabIndex = 4;
            // 
            // buttonProcurarDestino
            // 
            this.buttonProcurarDestino.Location = new System.Drawing.Point(32, 232);
            this.buttonProcurarDestino.Name = "buttonProcurarDestino";
            this.buttonProcurarDestino.Size = new System.Drawing.Size(100, 23);
            this.buttonProcurarDestino.TabIndex = 5;
            this.buttonProcurarDestino.Text = "Procurar pasta...";
            this.buttonProcurarDestino.UseVisualStyleBackColor = true;
            this.buttonProcurarDestino.Click += new System.EventHandler(this.buttonProcurarDestino_Click);
            // 
            // buttonSalvar
            // 
            this.buttonSalvar.Location = new System.Drawing.Point(32, 388);
            this.buttonSalvar.Name = "buttonSalvar";
            this.buttonSalvar.Size = new System.Drawing.Size(75, 23);
            this.buttonSalvar.TabIndex = 6;
            this.buttonSalvar.Text = "Salvar";
            this.buttonSalvar.UseVisualStyleBackColor = true;
            this.buttonSalvar.Click += new System.EventHandler(this.buttonSalvar_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Location = new System.Drawing.Point(162, 388);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 7;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.ButtonCancelar_Click);
            // 
            // buttonProcurarOrigemFile
            // 
            this.buttonProcurarOrigemFile.Location = new System.Drawing.Point(183, 143);
            this.buttonProcurarOrigemFile.Name = "buttonProcurarOrigemFile";
            this.buttonProcurarOrigemFile.Size = new System.Drawing.Size(115, 23);
            this.buttonProcurarOrigemFile.TabIndex = 8;
            this.buttonProcurarOrigemFile.Text = "Procurar ficheiro...";
            this.buttonProcurarOrigemFile.UseVisualStyleBackColor = true;
            this.buttonProcurarOrigemFile.Click += new System.EventHandler(this.buttonProcurarOrigemFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBoxApagar
            // 
            this.checkBoxApagar.AutoSize = true;
            this.checkBoxApagar.Location = new System.Drawing.Point(32, 357);
            this.checkBoxApagar.Name = "checkBoxApagar";
            this.checkBoxApagar.Size = new System.Drawing.Size(176, 17);
            this.checkBoxApagar.TabIndex = 9;
            this.checkBoxApagar.Text = "Apagar se nao existir no destino";
            this.checkBoxApagar.UseVisualStyleBackColor = true;
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Location = new System.Drawing.Point(32, 13);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(56, 17);
            this.chkActivo.TabIndex = 10;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // Excepcoes
            // 
            this.Excepcoes.Location = new System.Drawing.Point(32, 289);
            this.Excepcoes.Multiline = true;
            this.Excepcoes.Name = "Excepcoes";
            this.Excepcoes.Size = new System.Drawing.Size(725, 57);
            this.Excepcoes.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Excepções:";
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(29, 45);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(36, 13);
            this.lblLabel.TabIndex = 13;
            this.lblLabel.Text = "Label:";
            // 
            // boxLabel
            // 
            this.boxLabel.Location = new System.Drawing.Point(32, 61);
            this.boxLabel.Name = "boxLabel";
            this.boxLabel.Size = new System.Drawing.Size(725, 20);
            this.boxLabel.TabIndex = 14;
            // 
            // EditBackupEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 453);
            this.Controls.Add(this.boxLabel);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Excepcoes);
            this.Controls.Add(this.chkActivo);
            this.Controls.Add(this.checkBoxApagar);
            this.Controls.Add(this.buttonProcurarOrigemFile);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonSalvar);
            this.Controls.Add(this.buttonProcurarDestino);
            this.Controls.Add(this.PathDestino);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonProcurarOrigemFolder);
            this.Controls.Add(this.PathOrigem);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditBackupEntry";
            this.ShowInTaskbar = false;
            this.Text = "EditBackupEntry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PathOrigem;
        private System.Windows.Forms.Button buttonProcurarOrigemFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PathDestino;
        private System.Windows.Forms.Button buttonProcurarDestino;
        private System.Windows.Forms.Button buttonSalvar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonProcurarOrigemFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBoxApagar;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.TextBox Excepcoes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox boxLabel;
    }
}