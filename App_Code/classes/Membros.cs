using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Membros
    {
        private int mem_codigo;
        private string mem_tipo;
        private Grupo gru_codigo;
        private Usuario usu_codigo;

        public int Mem_codigo
        {
            get { return mem_codigo; }
            set { mem_codigo = value; }
        }

        public string Mem_tipo
        {
            get { return mem_tipo; }
            set { mem_tipo = value; }
        }

        public Grupo Gru_codigo
        {
            get { return gru_codigo; }
            set { gru_codigo = value; }
        }

        public Usuario Usu_codigo
        {
            get { return usu_codigo; }
            set { usu_codigo = value; }
        }
    }
}