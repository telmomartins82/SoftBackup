using SoftBackupCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoftBackup
{
    public partial class Automatizacao : Form
    {
        private Janela janela;

        public Automatizacao(Janela janela)
        {
            InitializeComponent();
            this.janela = janela;
            cbActivo.Checked = janela.Configuracao.Automatizacao.Activa;
            selectDiaMes.SelectedItem = "" + janela.Configuracao.Automatizacao.DiaMensal;
            selectHora.SelectedItem = "" + (janela.Configuracao.Automatizacao.Horas > 9 ? janela.Configuracao.Automatizacao.Horas.ToString() : ("0" + janela.Configuracao.Automatizacao.Horas));
            selectMinute.SelectedItem = "" + (janela.Configuracao.Automatizacao.Minutos > 9 ? janela.Configuracao.Automatizacao.Minutos.ToString() : ("0" + janela.Configuracao.Automatizacao.Minutos));
            Domingo.Checked = janela.Configuracao.Automatizacao.DiasSemanaActivo[0];
            Segunda.Checked = janela.Configuracao.Automatizacao.DiasSemanaActivo[1];
            Terca.Checked = janela.Configuracao.Automatizacao.DiasSemanaActivo[2];
            Quarta.Checked = janela.Configuracao.Automatizacao.DiasSemanaActivo[3];
            Quinta.Checked = janela.Configuracao.Automatizacao.DiasSemanaActivo[4];
            Sexta.Checked = janela.Configuracao.Automatizacao.DiasSemanaActivo[5];
            Sabado.Checked = janela.Configuracao.Automatizacao.DiasSemanaActivo[6];

            if (janela.Configuracao.Automatizacao.Frequencia == ExecucaoAutomatica.FrequenciaOpts.MENSAL)
            {
                rdFrequenciaM.Checked = true;
                rdFrequenciaD.Checked = false;
                rdFrequenciaS.Checked = false;
            }
            else if (janela.Configuracao.Automatizacao.Frequencia == ExecucaoAutomatica.FrequenciaOpts.DIARIA)
            {
                rdFrequenciaM.Checked = false;
                rdFrequenciaD.Checked = true;
                rdFrequenciaS.Checked = false;
            }
            else if (janela.Configuracao.Automatizacao.Frequencia == ExecucaoAutomatica.FrequenciaOpts.DIAS_SEMANA)
            {
                rdFrequenciaM.Checked = false;
                rdFrequenciaD.Checked = false;
                rdFrequenciaS.Checked = true;
            }
            else
            {
                rdFrequenciaM.Checked = false;
                rdFrequenciaD.Checked = false;
                rdFrequenciaS.Checked = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            //validar primeiro
            if(cbActivo.Checked && rdFrequenciaS.Checked && !Domingo.Checked && !Segunda.Checked && !Terca.Checked && !Quarta.Checked && 
                !Quinta.Checked && !Sexta.Checked && !Sabado.Checked)
            {
                MessageBox.Show("Tem de selecionar pelo menos um dia da semana.");
                return;
            }

            janela.Configuracao.Automatizacao.Activa = cbActivo.Checked;
            janela.Configuracao.Automatizacao.Frequencia = rdFrequenciaM.Checked ? ExecucaoAutomatica.FrequenciaOpts.MENSAL :
                (rdFrequenciaD.Checked ? ExecucaoAutomatica.FrequenciaOpts.DIARIA : 
                    (rdFrequenciaS.Checked ? ExecucaoAutomatica.FrequenciaOpts.DIAS_SEMANA : ExecucaoAutomatica.FrequenciaOpts.MENSAL));

            janela.Configuracao.Automatizacao.DiaMensal = int.Parse((string)selectDiaMes.SelectedItem);
            janela.Configuracao.Automatizacao.Horas = int.Parse((string)selectHora.SelectedItem);
            janela.Configuracao.Automatizacao.Minutos = int.Parse((string)selectMinute.SelectedItem);
            janela.Configuracao.Automatizacao.DiasSemanaActivo = new bool[] 
                {Domingo.Checked, Segunda.Checked, Terca.Checked, Quarta.Checked, Quinta.Checked, Sexta.Checked, Sabado.Checked };

            janela.GuardarConfiguracao();
            this.Close();
        }

        private void rdFrequencia_CheckedChanged(object sender, EventArgs e)
        {
            selectDiaMes.Enabled = rdFrequenciaM.Checked;
        }
    }
}
