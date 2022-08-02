using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class UsuarioDB
    {
        public static string erro;
        public static int retorno;

        public static int Insert(Usuario usu)
        {

            TipoUsuario t = new TipoUsuario();
            PerguntasSecretas p = new PerguntasSecretas();

            try
            {

                IDbConnection objConexao;
                IDbCommand objCommand;

                string sql = "INSERT INTO usu_usuario(";
                sql += "usu_nome,";
                sql += "usu_sexo,";
                sql += "usu_email,";
                sql += "usu_senha,";
                sql += "usu_username,";
                sql += "tip_tipo_tip_cod,";
                sql += "per_perguntas_per_codigo,";
                sql += "usu_resposta,";
                sql += "usu_cpf,";
                sql += "usu_data_nascimento,";
                sql += "usu_status";
                sql += ")";
                sql += " VALUES (";
                sql += "?usu_nome,";
                sql += "?usu_sexo,";
                sql += "?usu_email,";
                sql += "?usu_senha,";
                sql += "?usu_username,";
                sql += "?tip_tipo_tip_cod,";
                sql += "?per_perguntas_per_codigo,";
                sql += "?usu_resposta,";
                sql += "?usu_cpf,";
                sql += "?usu_data_nascimento,";
                sql += "?usu_status";
                sql += ")";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?usu_nome", usu.Usu_nome));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_sexo", usu.Usu_sexo));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_email", usu.Usu_email));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_senha", usu.Usu_senha));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_username", usu.Usu_login));
                objCommand.Parameters.Add(Mapped.Parameter("?tip_tipo_tip_cod", usu.Tip_cod.Tip_cod));// Chave Extrangeira
                objCommand.Parameters.Add(Mapped.Parameter("?per_perguntas_per_codigo", usu.Per_codigo.Per_codigo)); // Chave Extrangeira
                objCommand.Parameters.Add(Mapped.Parameter("?usu_resposta", usu.Usu_resposta));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_cpf", usu.Usu_cpf));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_data_nascimento", usu.Usu_dataNascimento));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_status", "Ativo"));

                objCommand.ExecuteNonQuery();

                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose(); //fecha conexão
            }

            catch (Exception e)
            {
                retorno = -2;
                erro = e.GetBaseException().ToString();
            }

            return retorno;
        }

        public Usuario Autentica(string username, string senha)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM usu_usuario WHERE usu_username = ?usu_username and usu_senha = ?usu_senha", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", username));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_senha", senha));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
                obj.Usu_nome = Convert.ToString(objDataReader["usu_nome"]);
                obj.Usu_login = Convert.ToString(objDataReader["usu_username"]);
                obj.Usu_status = Convert.ToString(objDataReader["usu_status"]);
                obj.Tip_cod = new TipoUsuario();
                obj.Tip_cod.Tip_cod = Convert.ToInt32(objDataReader["tip_tipo_tip_cod"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Usuario Select(int codigo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM usu_usuario WHERE usu_codigo = ?usu_codigo",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
                obj.Usu_nome = Convert.ToString(objDataReader["usu_nome"]);
                obj.Usu_login = Convert.ToString(objDataReader["usu_username"]);
                obj.Usu_email = Convert.ToString(objDataReader["usu_email"]);
                obj.Usu_sexo = Convert.ToString(objDataReader["usu_sexo"]);
                obj.Usu_cpf = Convert.ToString(objDataReader["usu_cpf"]);
                obj.Usu_dataNascimento = Convert.ToString(objDataReader["usu_data_nascimento"]);
                obj.Usu_status = Convert.ToString(objDataReader["usu_status"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Usuario SelectUsername(int codigo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_username FROM usu_usuario WHERE usu_codigo = ?usu_codigo",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_login = Convert.ToString(objDataReader["usu_username"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        //Validar caso email já existe (CADASTRO)
        public Usuario ValidarEmail(string email)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_email FROM usu_usuario WHERE usu_email = ?usu_email;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_email", email));
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

        //Validar caso username exista
        public Usuario ValidarUsername(string username)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_codigo, usu_username, tip_tipo_tip_cod FROM usu_usuario WHERE usu_username = ?usu_username;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", username));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
                obj.Usu_login = Convert.ToString(objDataReader["usu_username"]);
                obj.Tip_cod = new TipoUsuario();
                obj.Tip_cod.Tip_cod = Convert.ToInt32(objDataReader["tip_tipo_tip_cod"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        //Validar caso username já existe (CADASTRO)
        public Usuario ValidarCPF(string cpf)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_cpf FROM usu_usuario WHERE usu_cpf = ?usu_cpf;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_cpf", cpf));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_cpf = Convert.ToString(objDataReader["usu_cpf"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public bool Update(Usuario usuario)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = "UPDATE usu_usuario ";
                sql += "SET usu_nome = ?usu_nome, ";
                sql += "usu_sexo = ?usu_sexo, ";
                sql += "usu_email = ?usu_email, ";
                sql += "usu_username = ?usu_username, ";
                sql += "usu_cpf = ?usu_cpf, ";
                sql += "usu_data_nascimento = ?usu_data_nascimento ";
                sql += "WHERE ";
                sql += "usu_codigo = ?usu_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?usu_nome", usuario.Usu_nome));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_sexo", usuario.Usu_sexo));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_email", usuario.Usu_email));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_username", usuario.Usu_login));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_cpf", usuario.Usu_cpf));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_data_nascimento", usuario.Usu_dataNascimento));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", usuario.Usu_codigo));

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

        public Usuario SelectSenha(string login)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_senha FROM usu_usuario WHERE usu_username = ?usu_username;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", login));
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

        public Usuario SelecEmailUsuario(int codUsu)
        {
            Usuario obj = null;

            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();
            string sql = @"SELECT usu_codigo, usu_email FROM usu_usuario WHERE usu_codigo = ?usu_codigo";
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codUsu));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
                obj.Usu_email = Convert.ToString(objDataReader["usu_email"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public bool UpdateStatusUsario(string status, int codUsu)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command("UPDATE usu_usuario SET usu_status = ?usu_status WHERE usu_codigo = ?usu_codigo;",
                objConexao);

                objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codUsu));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_status", status));

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