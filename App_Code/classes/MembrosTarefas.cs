using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Persistencias;

namespace Monitore.Classes
{
    public class MembrosTarefas
    {
        private Tarefa tar_codigo;
        private Membros mem_codigo;

        public Tarefa Tar_codigo
        {
            get { return tar_codigo; }
            set { tar_codigo = value; }
        }

        public Membros Mem_codigo
        {
            get { return mem_codigo; }
            set { mem_codigo = value; }
        }
    }
}