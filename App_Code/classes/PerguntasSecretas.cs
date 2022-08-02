using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitore.Classes
{
    public class PerguntasSecretas
    {
        private int per_codigo;
        private string per_descricao;

        public int Per_codigo
        {
            get { return per_codigo; }
            set { per_codigo = value; }
        }

        public string Per_descricao
        {
            get { return per_descricao; }
            set { per_descricao = value; }
        }
    }
}