using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class RecuperacaoSenhaDB
    {
        public Usuario SelectCodigoPergunta(string login)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_senha ,usu_email, usu_username, per_perguntas_per_codigo FROM usu_usuario WHERE usu_username = ?usu_username",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", login));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_senha = Convert.ToString(objDataReader["usu_senha"]);
                obj.Usu_email = Convert.ToString(objDataReader["usu_email"]);
                obj.Usu_login = Convert.ToString(objDataReader["usu_username"]);
                obj.Per_codigo = new PerguntasSecretas();
                obj.Per_codigo.Per_codigo = Convert.ToInt32(objDataReader["per_perguntas_per_codigo"]);

            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Usuario SelectPergunta(int codigo, string login)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT per_descricao FROM usu_usuario, per_perguntas WHERE usu_username = ?usu_username and per_codigo = ?per_codigo",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", login));
            objCommand.Parameters.Add(Mapped.Parameter("?per_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Per_codigo = new PerguntasSecretas();
                obj.Per_codigo.Per_descricao = Convert.ToString(objDataReader["per_descricao"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Usuario SelectResposta(int codigo, string login, string resposta)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT per_perguntas_per_codigo, usu_username, usu_resposta FROM usu_usuario WHERE per_perguntas_per_codigo = ?per_perguntas_per_codigo and usu_username = ?usu_username and usu_resposta = ?usu_resposta;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?per_perguntas_per_codigo", codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", login));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_resposta", resposta));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Per_codigo = new PerguntasSecretas();
                obj.Per_codigo.Per_codigo = Convert.ToInt32(objDataReader["per_perguntas_per_codigo"]);
                obj.Usu_login = Convert.ToString(objDataReader["usu_username"]);
                obj.Usu_resposta = Convert.ToString(objDataReader["usu_resposta"]);

            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Usuario SelectEmail(string login)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_email FROM usu_usuario WHERE usu_username = ?usu_username;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", login));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_email = Convert.ToString(objDataReader["usu_email"]);

            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public bool UpdateSenhaNova(string senhaGerada, Usuario usuario)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = "UPDATE usu_usuario ";
                sql += "SET usu_senha = ?usu_senha ";
                sql += "WHERE ";
                sql += "usu_email = ?usu_email;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?usu_senha", senhaGerada));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_email", usuario.Usu_email));

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

    }
}