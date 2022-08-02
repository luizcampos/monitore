using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class PerfilDB
    {
        public static int retorno;

        //Validar caso email já existe (CADASTRO)
        public Usuario ValidarEmailPerfil(string email, int codigo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_email, usu_codigo FROM usu_usuario WHERE usu_email = ?usu_email and usu_codigo NOT IN (?usu_codigo);",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_email", email));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_email = Convert.ToString(objDataReader["usu_email"]);
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        //Validar caso username já existe (CADASTRO)
        public Usuario ValidarCPFPerfil(string cpf, int codigo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_cpf, usu_codigo FROM usu_usuario WHERE usu_cpf = ?usu_cpf and usu_codigo NOT IN (?usu_codigo);",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_cpf", cpf));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_cpf = Convert.ToString(objDataReader["usu_cpf"]);
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public Usuario ValidarUsernamePerfil(string username, int codigo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_codigo, usu_username, tip_tipo_tip_cod FROM usu_usuario WHERE usu_username = ?usu_username AND usu_codigo <> ?usu_codigo;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", username));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_login = Convert.ToString(objDataReader["usu_username"]);
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        //Validar caso senha esteja certa (ALTERAÇÃO DE SENHA)
        public Usuario ValidarSenhaAtual(string senha, int codigo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_senha FROM usu_usuario WHERE usu_senha = ?usu_senha and usu_codigo = ?usu_codigo;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_senha", senha));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_senha = Convert.ToString(objDataReader["usu_senha"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        //ALTERAR SENHA
        public bool UpdateSenhaPerfil(Usuario usuSenha)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command("UPDATE usu_usuario SET usu_senha = ?usu_senha WHERE usu_codigo = ?usu_codigo;",
                objConexao);

                objCommand.Parameters.Add(Mapped.Parameter("?usu_senha", usuSenha.Usu_senha));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", usuSenha.Usu_codigo));

                objCommand.ExecuteNonQuery();
                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose();
            }

            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public Usuario ValidarSenhaPorEmail(string email, int codigo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_senha FROM usu_usuario WHERE usu_email = ?usu_email and usu_codigo = ?usu_codigo;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_email", email));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_senha = Convert.ToString(objDataReader["usu_senha"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }
    }
}