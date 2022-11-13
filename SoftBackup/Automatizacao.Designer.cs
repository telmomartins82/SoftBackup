namespace SoftBackup
{
    partial class Automatizacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Automatizacao));
            this.cbActivo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdFrequenciaM = new System.Windows.Forms.RadioButton();
            this.rdFrequenciaD = new System.Windows.Forms.RadioButton();
            this.rdFrequenciaS = new System.Windows.Forms.RadioButton();
            this.selectDiaMes = new System.Windows.Forms.ComboBox();
            this.lblDia = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.selectHora = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selectMinute = new System.Windows.Forms.ComboBox();
            this.lblDiasSemana = new System.Windows.Forms.Label();
            this.Domingo = new System.Windows.Forms.CheckBox();
            this.Segunda = new System.Windows.Forms.CheckBox();
            this.Terca = new System.Windows.Forms.CheckBox();
            this.Sexta = new System.Windows.Forms.CheckBox();
            this.Quinta = new System.Windows.Forms.CheckBox();
            this.Quarta = new System.Windows.Forms.CheckBox();
            this.Sabado = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbActivo
            // 
            this.cbActivo.AutoSize = true;
            this.cbActivo.Location = new System.Drawing.Point(70, 39);
            this.cbActivo.Name = "cbActivo";
            this.cbActivo.Size = new System.Drawing.Size(165, 17);
            this.cbActivo.TabIndex = 0;
            this.cbActivo.Text = "Backups automaticos activos";
            this.cbActivo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Frequencia";
            // 
            // rdFrequenciaM
            // 
            this.rdFrequenciaM.AutoSize = true;
            this.rdFrequenciaM.Location = new System.Drawing.Point(70, 105);
            this.rdFrequenciaM.Name = "rdFrequenciaM";
            this.rdFrequenciaM.Size = new System.Drawing.Size(59, 17);
            this.rdFrequenciaM.TabIndex = 2;
            this.rdFrequenciaM.TabStop = true;
            this.rdFrequenciaM.Text = "Mensal";
            this.rdFrequenciaM.UseVisualStyleBackColor = true;
            this.rdFrequenciaM.CheckedChanged += new System.EventHandler(this.rdFrequencia_CheckedChanged);
            // 
            // rdFrequenciaD
            // 
            this.rdFrequenciaD.AutoSize = true;
            this.rdFrequenciaD.Location = new System.Drawing.Point(135, 105);
            this.rdFrequenciaD.Name = "rdFrequenciaD";
            this.rdFrequenciaD.Size = new System.Drawing.Size(52, 17);
            this.rdFrequenciaD.TabIndex = 3;
            this.rdFrequenciaD.TabStop = true;
            this.rdFrequenciaD.Text = "Diaria";
            this.rdFrequenciaD.UseVisualStyleBackColor = true;
            this.rdFrequenciaD.CheckedChanged += new System.EventHandler(this.rdFrequencia_CheckedChanged);
            // 
            // rdFrequenciaS
            // 
            this.rdFrequenciaS.AutoSize = true;
            this.rdFrequenciaS.Location = new System.Drawing.Point(193, 105);
            this.rdFrequenciaS.Name = "rdFrequenciaS";
            this.rdFrequenciaS.Size = new System.Drawing.Size(101, 17);
            this.rdFrequenciaS.TabIndex = 4;
            this.rdFrequenciaS.TabStop = true;
            this.rdFrequenciaS.Text = "Dias da semana";
            this.rdFrequenciaS.UseVisualStyleBackColor = true;
            this.rdFrequenciaS.CheckedChanged += new System.EventHandler(this.rdFrequencia_CheckedChanged);
            // 
            // selectDiaMes
            // 
            this.selectDiaMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectDiaMes.FormattingEnabled = true;
            this.selectDiaMes.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.selectDiaMes.Location = new System.Drawing.Point(102, 138);
            this.selectDiaMes.Name = "selectDiaMes";
            this.selectDiaMes.Size = new System.Drawing.Size(110, 21);
            this.selectDiaMes.TabIndex = 5;
            // 
            // lblDia
            // 
            this.lblDia.AutoSize = true;
            this.lblDia.Location = new System.Drawing.Point(67, 138);
            this.lblDia.Name = "lblDia";
            this.lblDia.Size = new System.Drawing.Size(23, 13);
            this.lblDia.TabIndex = 6;
            this.lblDia.Text = "Dia";
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(67, 169);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(30, 13);
            this.lblHora.TabIndex = 7;
            this.lblHora.Text = "Hora";
            // 
            // selectHora
            // 
            this.selectHora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectHora.FormattingEnabled = true;
            this.selectHora.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.selectHora.Location = new System.Drawing.Point(102, 169);
            this.selectHora.Name = "selectHora";
            this.selectHora.Size = new System.Drawing.Size(47, 21);
            this.selectHora.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = ":";
            // 
            // selectMinute
            // 
            this.selectMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectMinute.FormattingEnabled = true;
            this.selectMinute.Items.AddRange(new object[] {
            "00",
            "05",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55"});
            this.selectMinute.Location = new System.Drawing.Point(164, 169);
            this.selectMinute.Name = "selectMinute";
            this.selectMinute.Size = new System.Drawing.Size(48, 21);
            this.selectMinute.TabIndex = 10;
            // 
            // lblDiasSemana
            // 
            this.lblDiasSemana.AutoSize = true;
            this.lblDiasSemana.Location = new System.Drawing.Point(67, 206);
            this.lblDiasSemana.Name = "lblDiasSemana";
            this.lblDiasSemana.Size = new System.Drawing.Size(28, 13);
            this.lblDiasSemana.TabIndex = 11;
            this.lblDiasSemana.Text = "Dias";
            // 
            // Domingo
            // 
            this.Domingo.AutoSize = true;
            this.Domingo.Location = new System.Drawing.Point(102, 205);
            this.Domingo.Name = "Domingo";
            this.Domingo.Size = new System.Drawing.Size(48, 17);
            this.Domingo.TabIndex = 12;
            this.Domingo.Text = "Dom";
            this.Domingo.UseVisualStyleBackColor = true;
            // 
            // Segunda
            // 
            this.Segunda.AutoSize = true;
            this.Segunda.Location = new System.Drawing.Point(156, 205);
            this.Segunda.Name = "Segunda";
            this.Segunda.Size = new System.Drawing.Size(45, 17);
            this.Segunda.TabIndex = 13;
            this.Segunda.Text = "Seg";
            this.Segunda.UseVisualStyleBackColor = true;
            // 
            // Terca
            // 
            this.Terca.AutoSize = true;
            this.Terca.Location = new System.Drawing.Point(207, 205);
            this.Terca.Name = "Terca";
            this.Terca.Size = new System.Drawing.Size(42, 17);
            this.Terca.TabIndex = 14;
            this.Terca.Text = "Ter";
            this.Terca.UseVisualStyleBackColor = true;
            // 
            // Sexta
            // 
            this.Sexta.AutoSize = true;
            this.Sexta.Location = new System.Drawing.Point(355, 205);
            this.Sexta.Name = "Sexta";
            this.Sexta.Size = new System.Drawing.Size(44, 17);
            this.Sexta.TabIndex = 17;
            this.Sexta.Text = "Sex";
            this.Sexta.UseVisualStyleBackColor = true;
            // 
            // Quinta
            // 
            this.Quinta.AutoSize = true;
            this.Quinta.Location = new System.Drawing.Point(307, 205);
            this.Quinta.Name = "Quinta";
            this.Quinta.Size = new System.Drawing.Size(42, 17);
            this.Quinta.TabIndex = 16;
            this.Quinta.Text = "Qui";
            this.Quinta.UseVisualStyleBackColor = true;
            // 
            // Quarta
            // 
            this.Quarta.AutoSize = true;
            this.Quarta.Location = new System.Drawing.Point(255, 205);
            this.Quarta.Name = "Quarta";
            this.Quarta.Size = new System.Drawing.Size(46, 17);
            this.Quarta.TabIndex = 15;
            this.Quarta.Text = "Qua";
            this.Quarta.UseVisualStyleBackColor = true;
            // 
            // Sabado
            // 
            this.Sabado.AutoSize = true;
            this.Sabado.Location = new System.Drawing.Point(405, 205);
            this.Sabado.Name = "Sabado";
            this.Sabado.Size = new System.Drawing.Size(45, 17);
            this.Sabado.TabIndex = 18;
            this.Sabado.Text = "Sab";
            this.Sabado.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(70, 252);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(193, 252);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 20;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // Automatizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 329);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Sabado);
            this.Controls.Add(this.Sexta);
            this.Controls.Add(this.Quinta);
            this.Controls.Add(this.Quarta);
            this.Controls.Add(this.Terca);
            this.Controls.Add(this.Segunda);
            this.Controls.Add(this.Domingo);
            this.Controls.Add(this.lblDiasSemana);
            this.Controls.Add(this.selectMinute);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectHora);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.lblDia);
            this.Controls.Add(this.selectDiaMes);
            this.Controls.Add(this.rdFrequenciaS);
            this.Controls.Add(this.rdFrequenciaD);
            this.Controls.Add(this.rdFrequenciaM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbActivo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Automatizacao";
            this.ShowInTaskbar = false;
            this.Text = "Automatizacao";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbActivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdFrequenciaM;
        private System.Windows.Forms.RadioButton rdFrequenciaD;
        private System.Windows.Forms.RadioButton rdFrequenciaS;
        private System.Windows.Forms.ComboBox selectDiaMes;
        private System.Windows.Forms.Label lblDia;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.ComboBox selectHora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selectMinute;
        private System.Windows.Forms.Label lblDiasSemana;
        private System.Windows.Forms.CheckBox Domingo;
        private System.Windows.Forms.CheckBox Segunda;
        private System.Windows.Forms.CheckBox Terca;
        private System.Windows.Forms.CheckBox Sexta;
        private System.Windows.Forms.CheckBox Quinta;
        private System.Windows.Forms.CheckBox Quarta;
        private System.Windows.Forms.CheckBox Sabado;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancelar;
    }
}