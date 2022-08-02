using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    /// <summary>
    /// Summary description for TipoUsuario
    /// </summary>
    public class TipoUsuario
    {
        private int tip_cod;
        private string tip_descricao;

        public int Tip_cod
        {
            get { return tip_cod; }
            set { tip_cod = value; }
        }

        public string Tip_descricao
        {
            get { return tip_descricao; }
            set { tip_descricao = value; }
        }
    }
}