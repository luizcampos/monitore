using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Temas
    {
        private int tem_codigo;
        private string tem_nome;
        private Usuario usu_codigo; //criador do tema
        private int qtde_tarefas_tema;


        public int Tem_codigo
        {
            get { return tem_codigo; }
            set { tem_codigo = value; }
        }

        public string Tem_nome
        {
            get { return tem_nome; }
            set { tem_nome = value; }
        }

        public Usuario Usu_codigo
        {
            get { return usu_codigo; }
            set { usu_codigo = value; }
        }

        public int Qtde_tarefas_tema
        {
            get { return qtde_tarefas_tema; }
            set { qtde_tarefas_tema = value; }
        }

    }
}