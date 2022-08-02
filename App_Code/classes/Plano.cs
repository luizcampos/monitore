using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Plano
    {
        private string pla_codigo;
        private string pla_tipo;
        private double pla_preco;
        private string pla_data_contratacao;
        private string pla_validade;
        private Usuario usu_codigo;

        public string Pla_codigo
        {
            get { return pla_codigo; }
            set { pla_codigo = value; }
        }

        public string Pla_tipo
        {
            get { return pla_tipo; }
            set { pla_tipo = value; }
        }

        public double Pla_preco
        {
            get { return pla_preco; }
            set { pla_preco = value; }
        }

        public string Pla_data_contratacao
        {
            get { return pla_data_contratacao; }
            set { pla_data_contratacao = value; }
        }

        public string Pla_validade
        {
            get { return pla_validade; }
            set { pla_validade = value; }
        }

        public Usuario Usu_codigo
        {
            get { return usu_codigo; }
            set { usu_codigo = value; }
        }
    }
}