using SoftBackupCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoftBackup
{
    public partial class EditBackupEntry : Form
    {
        private BackupEntry bckEntry;
        private Janela parent;

        public BackupEntry Entry { get => bckEntry; set => bckEntry = value; }

        public EditBackupEntry(Janela janela, BackupEntry entry)
        {
            InitializeComponent();
            Entry = entry != null ? entry : new BackupEntry();
            PathOrigem.Text = Entry.Origem;
            PathDestino.Text = Entry.Destino;
            checkBoxApagar.Checked = Entry.ApagarSeJaNaoExiste;
            chkActivo.Checked = Entry.Activo;
            boxLabel.Text = Entry.Label;
            foreach(string s in Entry.Excepcoes)
            {
                Excepcoes.Text += s + "\r\n";
            }
            parent = janela;
        }
        
        private void ButtonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonProcurarOrigemFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Reset();
            folderBrowserDialog1.ShowNewFolderButton = false;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                PathOrigem.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonProcurarOrigemFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Reset();
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PathOrigem.Text = openFileDialog1.FileName;
            }
        }

        private void buttonProcurarDestino_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Reset();
            folderBrowserDialog1.ShowNewFolderButton = false;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                PathDestino.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if(PathOrigem.Text == null || PathOrigem.Text == "" || PathDestino.Text == null || PathDestino.Text == "")
            {
                MessageBox.Show("Origem ou Destino por preencher.");
                return;
            }

            Entry.Origem = PathOrigem.Text;
            Entry.Destino = PathDestino.Text;
            Entry.ApagarSeJaNaoExiste = checkBoxApagar.Checked;
            Entry.Activo = chkActivo.Checked;
            Entry.Label = boxLabel.Text;

            List<string> exp = new List<string>();
            foreach(string s in Excepcoes.Text.Split("\r\n".ToCharArray()))
            {
                if(s.Length > 0)
                {
                    exp.Add(s);
                }
            }
            Entry.Excepcoes = exp;

            parent.EntryAdded(Entry);
            this.Close();
        }
    }
}
