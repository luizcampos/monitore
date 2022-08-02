using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Tarefa
    {
        private int tar_codigo;
        private string tar_nome;
        private string tar_prazo;
        private string tar_data_criacao;
        private Temas tem_codigo;
        private Grupo gru_codigo;
        private string tar_tipo;
        private int tar_qtde_membros;
        private string tar_descricao;
        private string tar_realizada;
        private string tar_status;

        public int Tar_codigo
        {
            get { return tar_codigo; }
            set { tar_codigo = value; }
        }

        public string Tar_nome
        {
            get { return tar_nome; }
            set { tar_nome = value; }
        }

        public string Tar_prazo
        {
            get { return tar_prazo; }
            set { tar_prazo = value; }
        }

        public string Tar_data_criacao
        {
            get { return tar_data_criacao; }
            set { tar_data_criacao = value; }
        }

        public Temas Tem_codigo
        {
            get { return tem_codigo; }
            set { tem_codigo = value; }
        }

        public Grupo Gru_codigo
        {
            get { return gru_codigo; }
            set { gru_codigo = value; }
        }

        public string Tar_tipo
        {
            get { return tar_tipo; }
            set { tar_tipo = value; }
        }

        public int Tar_qtde_membros
        {
            get { return tar_qtde_membros; }
            set { tar_qtde_membros = value; }
        }

        public string Tar_descricao
        {
            get { return tar_descricao; }
            set { tar_descricao = value; }
        }

        public string Tar_realizada
        {
            get { return tar_realizada; }
            set { tar_realizada = value; }
        }

        public string Tar_status
        {
            get { return tar_status; }
            set { tar_status = value; }
        }
    }
}