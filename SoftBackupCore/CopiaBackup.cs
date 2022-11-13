using SoftBackupCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SoftBackupCore
{
	
	public class CopiaBackup
	{
		private List<BackupEntry> Entries { get; set; }
        private bool ApenasFicheirosModificados { get; set; }
        private bool HasErrors { get; set; }
        private bool FullBackup { get; set; }
        public CopiaBackup(List<BackupEntry> entries, bool apenasFicheirosModificados, bool fullBackup)
		{
			Entries = entries;
            ApenasFicheirosModificados = apenasFicheirosModificados;
            FullBackup = fullBackup;
		}

		//Copia a dir e os seus files
		private void CopiaDir(string o, string d, string oOriginal, List<string> excepcoes)
		{
			string[]ficheiros = Directory.GetFiles(o);
			string[]temp = o.Split("\\".ToCharArray());
			string nomeDir = temp[temp.Length-1];
			nomeDir += "\\";

			string destino = d;
			if(!destino.EndsWith("\\"))
				destino += "\\";

			destino += nomeDir;
			destino += "\\";

            string oRelativa = o.Replace(oOriginal, "");
            if (oRelativa.StartsWith("\\"))
            {
                oRelativa = oRelativa.Substring(1);
            }
            foreach(string exp in excepcoes)
            {
                if(oRelativa == exp)
                {
                    return;
                }
            }

			if(!Directory.Exists(destino))
				Directory.CreateDirectory(destino);
			
			for(int i = 0; i < ficheiros.Length; i++)
			{
				//Obter nome
				string []t = ficheiros[i].Split("\\".ToCharArray());
				string nome = t[t.Length-1];

                var logFileEntry = "A copiar " + ficheiros[i] + " ...";
                
                try
				{
					
					if((ApenasFicheirosModificados) && File.Exists(destino+nome) && (File.GetLastWriteTime(ficheiros[i]) == File.GetLastWriteTime(destino+nome)))
					{
                        SoftBackupServer.Instance.SendToLogFile(true, $"{logFileEntry} CONCLUIDO: Ficheiro não é mais recente\r\n", true, true);
					}
					else
					{
                        SoftBackupServer.Instance.SendToLogFile(true, logFileEntry, true);
                        File.Copy(ficheiros[i], destino + nome, true);
                        SoftBackupServer.Instance.SendToLogFile(true, " CONCLUIDO\r\n", false);
					}
				}
				catch(Exception e)
				{
                    HasErrors = true;
                    if (e.GetType() == typeof(ThreadAbortException))
                    {
                        SoftBackupServer.Instance.SendToLogFile(false, "Backup Abortado: " + ficheiros[i] + " PODE NÃO TER sido copiado na totalidade\r\n", true);
                    }
                    else {
                        SoftBackupServer.Instance.SendToLogFile(false, "IMPOSSIVEL, o ficheiro " + ficheiros[i] + " pode estar a ser usado ou já não existir\r\n", true);
                    }
                    SoftBackupServer.Instance.SendToLogFile(false, "EXCEPTION: " + e.Message + " " + e.StackTrace, false);
                }
			}
			 //Vamos tratar agora das subdirectorias
			string[] directorias = Directory.GetDirectories(o);
			foreach(string s in directorias)
			{
				CopiaDir(s, destino, oOriginal, excepcoes);
			}

		}

        private void ApagaFilesJaNaoExistentes(string o, string d)
        {
            
            string[] temp = o.Split("\\".ToCharArray());
            string nomeDir = temp[temp.Length - 1];
            if(! nomeDir.EndsWith("\\"))
                nomeDir += "\\";

            string origem = o;
            if (!origem.EndsWith("\\"))
                origem += "\\";

            string destino = d;
            if(!destino.EndsWith("\\"))
                destino += "\\";

            destino += nomeDir;
            string[] ficheiros = Directory.GetFiles(destino);

            for (int i = 0; i < ficheiros.Length; i++)
            {
                //Obter nome
                string[] t = ficheiros[i].Split("\\".ToCharArray());
                string nome = t[t.Length - 1];

                try
                {

                    if (! File.Exists(origem + nome))
                    {
                        File.Delete(ficheiros[i]);
                        SoftBackupServer.Instance.SendToLogFile(true, "Entrada " + ficheiros[i].Replace("\\\\", "\\") + " APAGADA\r\n", true);
                    }
                }
                catch (Exception e)
                {
                    HasErrors = true;
                    SoftBackupServer.Instance.SendToLogFile(false, "EXCEPTION: " + e.Message + " " + e.StackTrace, false);
                }
            }
            //Vamos tratar agora das subdirectorias
            string[] directorias = Directory.GetDirectories(destino);
            foreach (string s in directorias)
            {
                string origemF = origem;
                string[] tmp = s.Split("\\".ToCharArray());
                string nomeDirTmp = tmp[tmp.Length - 1];

                if(! nomeDirTmp.EndsWith("\\"))
                    nomeDirTmp += "\\";

                if (!origemF.EndsWith("\\")){
                    origemF += "\\";
                }

                origemF += nomeDirTmp;
                ApagaFilesJaNaoExistentes(origemF.Replace("\\\\", "\\"), s.Replace("\\\\", "\\"));
            }

            //ver a directoria em si
            if(!Directory.Exists(o))
            {
                Directory.Delete(d);
                SoftBackupServer.Instance.SendToLogFile(true, "Entrada " + d.Replace("\\\\", "\\") + " APAGADA\r\n", true);
            }
        }

		public void Executa()
		{
            DateTime startedOn = DateTime.Now;
            SoftBackupServer.Instance.SendToLogFile(false, $"A iniciar Backup{(FullBackup ? "" : " (selecionado)")}\r\n", true);
            if (SoftBackupServer.DELAY_BACKUP_DEBUG)
            {
                Thread.Sleep(2500);
            }
            foreach (BackupEntry entry in Entries)
			{
				if(!Directory.Exists(entry.Origem))
				{
                    if (!File.Exists(entry.Origem))
                    {
                        HasErrors = true;
                        SoftBackupServer.Instance.SendToLogFile(false, "ERRO : directorio ou ficheiro fonte não encontrado: " + entry.Origem + "\r\n", true);
                        continue;
                    }

                    var logFileEntry = "A iniciar ficheiro de entrada " + entry.Origem + " ...";
                    string fileDestino = entry.Destino + (entry.Destino.EndsWith("\\") ? "" : "\\");
                    string[] t = entry.Origem.Split("\\".ToCharArray());
                    string nome = t[t.Length - 1];
                    fileDestino += nome;

                    if ((ApenasFicheirosModificados) && File.Exists(fileDestino) && (File.GetLastWriteTime(entry.Origem) == File.GetLastWriteTime(fileDestino)))
                    {
                        SoftBackupServer.Instance.SendToLogFile(false, $"{logFileEntry} CONCLUIDO: Ficheiro não é mais recente\r\n", true, true);
                    }
                    else
                    {
                        SoftBackupServer.Instance.SendToLogFile(false, logFileEntry, true);
                        File.Copy(entry.Origem, fileDestino, true);
                        SoftBackupServer.Instance.SendToLogFile(false, " CONCLUIDO\r\n", false);
                    }
                    continue;
                }

                DateTime nowEntry = DateTime.Now;
                SoftBackupServer.Instance.SendToLogFile(false, "A iniciar entrada " + entry.Origem + "\r\n", true);
                CopiaDir(entry.Origem, entry.Destino.EndsWith("\\") ? entry.Destino : entry.Destino + "\\", entry.Origem, entry.Excepcoes);
                if (entry.ApagarSeJaNaoExiste)
                {
                    ApagaFilesJaNaoExistentes(entry.Origem, entry.Destino.EndsWith("\\") ? entry.Destino : entry.Destino + "\\");
                }
                SoftBackupServer.Instance.SendToLogFile(false, "Entrada terminada em: " + DiffDatasStr(nowEntry, DateTime.Now) + "\r\n", true);
            }
            SoftBackupServer.Instance.SendToLogFile(false, "Backup Terminado em " + DiffDatasStr(startedOn, DateTime.Now) + "\r\n\r\n", true);

            SoftBackupServer.Instance.FinalizarBackup(!HasErrors, startedOn, FullBackup);

        }

        private string DiffDatasStr(DateTime d1, DateTime d2)
        {
            double minutesD = (d2 - d1).TotalMinutes;
            int seconds = (int)(d2 - d1).TotalSeconds;
            if(minutesD <= 1)
            {
                return "00m:" + AddZerosEsq("" + seconds) + "s";
            }
            else
            {
                return ((int)minutesD) + "m:" + AddZerosEsq("" + (seconds % 60)) +"s";
            }
        }

        private string AddZerosEsq(string s)
        {
            if(s.Length == 0)
            {
                return "00";
            }
            if(s.Length == 1)
            {
                return "0" + s;
            }
            return s;
        }

	}
}
