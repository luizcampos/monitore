using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Comentario
    {
        private int com_codigo;
        private string com_conteudo;
        private string com_data;
        private string com_horario;
        private string com_cor;
        private string com_borda;
        private string com_cor_texto;
        private Usuario usu_codigo;
        private Tarefa tar_codigo;

        public int Com_codigo
        {
            get { return com_codigo; }
            set { com_codigo = value; }
        }

        public string Com_conteudo
        {
            get { return com_conteudo; }
            set { com_conteudo = value; }
        }

        public string Com_data
        {
            get { return com_data; }
            set { com_data = value; }
        }

        public string Com_horario
        {
            get { return com_horario; }
            set { com_horario = value; }
        }

        public string Com_cor
        {
            get { return com_cor; }
            set { com_cor = value; }
        }

        public string Com_borda
        {
            get { return com_borda; }
            set { com_borda = value; }
        }

        public string Com_cor_texto
        {
            get { return com_cor_texto; }
            set { com_cor_texto = value; }
        }

        public Usuario Usu_codigo
        {
            get { return usu_codigo; }
            set { usu_codigo = value; }
        }

        public Tarefa Tar_codigo
        {
            get { return tar_codigo; }
            set { tar_codigo = value; }
        }
    }
}