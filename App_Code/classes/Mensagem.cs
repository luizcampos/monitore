using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class Mensagem
    {
        private int men_codigo;
        private string men_data;
        private string men_horario;
        private string men_conteudo;
        private Membros men_destinatario_mem_codigo;
        private Membros men_remetente_mem_codigo;

        public int Men_codigo
        {
            get { return men_codigo; }
            set { men_codigo = value; }
        }

        public string Men_data
        {
            get { return men_data; }
            set { men_data = value; }
        }

        public string Men_horario
        {
            get { return men_horario; }
            set { men_horario = value; }
        }

        public string Men_conteudo
        {
            get { return men_conteudo; }
            set { men_conteudo = value; }
        }

        public Membros Men_destinatario_mem_codigo
        {
            get { return men_destinatario_mem_codigo; }
            set { men_destinatario_mem_codigo = value; }
        }

        public Membros Men_remetente_mem_codigo
        {
            get { return men_remetente_mem_codigo; }
            set { men_remetente_mem_codigo = value; }
        }
    }
}