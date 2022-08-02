using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Contato
    {
        private int con_codigo;
        private string con_titulo;
        private string con_conteudo;
        private string con_data;
        private Usuario usu_codigo;

        public int Con_codigo
        {
            get { return con_codigo; }
            set { con_codigo = value; }
        }

        public string Con_titulo
        {
            get { return con_titulo; }
            set { con_titulo = value; }
        }

        public string Con_conteudo
        {
            get { return con_conteudo; }
            set { con_conteudo = value; }
        }

        public string Con_data
        {
            get { return con_data; }
            set { con_data = value; }
        }

        public Usuario Usu_codigo
        {
            get { return usu_codigo; }
            set { usu_codigo = value; }
        }
    }
}