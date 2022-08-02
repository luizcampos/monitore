using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Arquivo
    {
        private int arq_codigo;
        private string arq_caminho;
        private string arq_miniatura;
        private string arq_dataEnvio;
        private string arq_horario;
        private Tarefa tar_codigo;

        public int Arq_codigo
        {
            get { return arq_codigo; }
            set { arq_codigo = value; }
        }

        public string Arq_caminho
        {
            get { return arq_caminho; }
            set { arq_caminho = value; }
        }

        public string Arq_miniatura
        {
            get { return arq_miniatura; }
            set { arq_miniatura = value; }
        }

        public string Arq_dataEnvio
        {
            get { return arq_dataEnvio; }
            set { arq_dataEnvio = value; }
        }

        public string Arq_horario
        {
            get { return arq_horario; }
            set { arq_horario = value; }
        }

        public Tarefa Tar_codigo
        {
            get { return tar_codigo; }
            set { tar_codigo = value; }
        }
    }
}