using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class PlanoDB
    {
        public static string erro;
        public static int retorno = 0;

        public static bool Insert(Plano pla)
        {
            Usuario u = new Usuario();

            try
            {

                IDbConnection objConexao;
                IDbCommand objCommand;

                string sql = "INSERT INTO pla_plano(";
                sql += "pla_tipo,";
                sql += "pla_preco,";
                sql += "pla_data_contratacao,";
                sql += "pla_validade,";
                sql += "usu_usuario_usu_codigo";
                sql += ")";
                sql += " VALUES (";
                sql += "?pla_tipo,";
                sql += "?pla_preco,";
                sql += "?pla_data_contratacao,";
                sql += "?pla_validade,";
                sql += "?usu_usuario_usu_codigo";
                sql += ")";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?pla_tipo", pla.Pla_tipo));
                objCommand.Parameters.Add(Mapped.Parameter("?pla_preco", pla.Pla_preco));
                objCommand.Parameters.Add(Mapped.Parameter("?pla_data_contratacao", pla.Pla_data_contratacao));
                objCommand.Parameters.Add(Mapped.Parameter("?pla_validade", pla.Pla_validade));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", pla.Usu_codigo.Usu_codigo));

                objCommand.ExecuteNonQuery();

                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose();
            }

            catch (Exception e)
            {
                return false;
                erro = e.GetBaseException().ToString();
            }

            return true;
        }

        public Usuario SelectCodigoCadastrado(string username)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_codigo FROM usu_usuario WHERE usu_username = ?usu_username;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", username));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Plano SelectTipoPlano(int codigo)
        {
            Plano obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT pla_codigo, pla_tipo, pla_preco, DATE_FORMAT (pla_validade, '%d/%m/%Y') AS pla_validade, DATE_FORMAT (pla_data_contratacao, '%d/%m/%Y') AS pla_data_contratacao FROM pla_plano WHERE usu_usuario_usu_codigo = ?usu_usuario_usu_codigo",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", codigo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Plano();
                obj.Pla_tipo = Convert.ToString(objDataReader["pla_tipo"]);
                obj.Pla_preco = Convert.ToDouble(objDataReader["pla_preco"]);
                obj.Pla_data_contratacao = Convert.ToString(objDataReader["pla_data_contratacao"]);
                obj.Pla_validade = Convert.ToString(objDataReader["pla_validade"]);

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