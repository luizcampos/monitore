using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Grupo
    {
        private int gru_codigo;
        private string gru_nome;
        private string gru_cor;
        private string gru_data_criacao;
        private Usuario usu_codigo;
        private string gru_status;
        private string gru_chave;

        public int Gru_codigo
        {
            get { return gru_codigo; }
            set { gru_codigo = value; }
        }

        public string Gru_nome
        {
            get { return gru_nome; }
            set { gru_nome = value; }
        }

        public string Gru_cor
        {
            get { return gru_cor; }
            set { gru_cor = value; }
        }

        public string Gru_data_criacao
        {
            get { return gru_data_criacao; }
            set { gru_data_criacao = value; }
        }

        public Usuario Usu_codigo
        {
            get { return usu_codigo; }
            set { usu_codigo = value; }
        }

        public string Gru_status
        {
            get { return gru_status; }
            set { gru_status = value; }
        }

        public string Gru_chave
        {
            get { return gru_chave; }
            set { gru_chave = value; }
        }
    }
}