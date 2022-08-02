using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitore.Classes
{
    /// <summary>
    /// Summary description for Usuario
    /// </summary>
    public class Usuario
    {
        private int usu_codigo;   //Criado 04/04
        private string usu_nome;
        private string usu_sexo;
        private string usu_email;
        private string usu_login;
        private string usu_senha;
        private TipoUsuario tip_cod;
        private PerguntasSecretas per_codigo;
        private string usu_resposta;
        private string usu_cpf;
        private string usu_dataNascimento;
        private string usu_status;

        public int Usu_codigo
        {
            get { return usu_codigo; }
            set { usu_codigo = value; }
        }

        public string Usu_nome
        {
            get { return usu_nome; }
            set { usu_nome = value; }
        }

        public string Usu_sexo
        {
            get { return usu_sexo; }
            set { usu_sexo = value; }
        }

        public string Usu_email
        {
            get { return usu_email; }
            set { usu_email = value; }
        }

        public string Usu_login
        {
            get { return usu_login; }
            set { usu_login = value; }
        }

        public string Usu_senha
        {
            get { return usu_senha; }
            set { usu_senha = value; }
        }

        public TipoUsuario Tip_cod
        {
            get { return tip_cod; }
            set { tip_cod = value; }
        }

        public PerguntasSecretas Per_codigo
        {
            get { return per_codigo; }
            set { per_codigo = value; }
        }

        public string Usu_resposta
        {
            get { return usu_resposta; }
            set { usu_resposta = value; }
        }

        public string Usu_cpf
        {
            get { return usu_cpf; }
            set { usu_cpf = value; }
        }

        public string Usu_dataNascimento
        {
            get { return usu_dataNascimento; }
            set { usu_dataNascimento = value; }
        }

        public string Usu_status
        {
            get { return usu_status; }
            set { usu_status = value; }
        }
    }
}