using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftBackupCore
{
    public class Configuracao
    {
        public static bool MOSTRAR_LABEL = true;

        public List<BackupEntry> BackupEntries { get; set; } = new List<BackupEntry>();
        public bool BackupApenasModificados { get; set; }
        public bool LogApenasModificados { get; set; }
        public bool LogTotal { get; set; }
        public DateTime LastRun { get; set; }
        public ExecucaoAutomatica Automatizacao { get; set; } = new ExecucaoAutomatica();
    }

    public class BackupEntry
    {
        
        public string Id { get; set; } = "";
        public string Origem { get; set; } = "";
        public string Destino { get; set; } = "";
        public bool Activo { get; set; } = true;
        public bool ApagarSeJaNaoExiste { get; set; }
        public List<string> Excepcoes { get; set; } = new List<string>();
        public string Label { get; set; } = "";

        override
        public string ToString()
        {
            return (Activo ? "" : "(INACTIVO) ") + ToStringAux();
        }

        private string ToStringAux()
        {
            if(Label != null && Label.Trim() != "" && Configuracao.MOSTRAR_LABEL)
            {
                return Label;
            }
            else
            {
                return Origem + "    =>    " + Destino + (ApagarSeJaNaoExiste ? "  (apagar apagados)" : "");
            }
        }
    }

    public class ExecucaoAutomatica
    {
        public enum FrequenciaOpts { MENSAL, DIARIA, DIAS_SEMANA }

        public bool Activa { get; set; }
        public FrequenciaOpts Frequencia { get; set; } = FrequenciaOpts.DIAS_SEMANA;
        public int DiaMensal { get; set; } = 1;
        public int Horas { get; set; } = 17;
        public int Minutos { get; set; } = 0;
        public bool[] DiasSemanaActivo { get; set; } = { false, true, true, true, true, true, false };
    }
}
