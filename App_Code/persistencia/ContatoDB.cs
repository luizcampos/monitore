using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;
using System.Data;

namespace Monitore.Persistencias
{
    public class ContatoDB
    {
        public static string erro;
        public bool InsertContato(Contato contato)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = @"INSERT INTO con_contato(con_titulo, con_conteudo, 
                            con_data, usu_usuario_usu_codigo) 
                            VALUES (?con_titulo, ?con_conteudo, 
                            ?con_data, ?usu_usuario_usu_codigo);";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?con_titulo", contato.Con_titulo));
                objCommand.Parameters.Add(Mapped.Parameter("?con_conteudo", contato.Con_conteudo));
                objCommand.Parameters.Add(Mapped.Parameter("?con_data", contato.Con_data));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", contato.Usu_codigo.Usu_codigo));
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

        public static DataSet SelectMensagensAll()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT u.usu_codigo, u.usu_nome, c.con_titulo, c.con_codigo FROM con_contato c INNER JOIN usu_usuario u
                            ON u.usu_codigo = c.usu_usuario_usu_codigo;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); 
            return ds;
        }

        public Contato SelectMensagemInfos(int codMsg)
        {
            Contato obj = null;

            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();
            string sql = @"SELECT u.usu_codigo, c.con_titulo, c.con_conteudo, c.con_codigo FROM con_contato c 
                            INNER JOIN usu_usuario u ON u.usu_codigo = c.usu_usuario_usu_codigo 
                            WHERE c.con_codigo = ?con_codigo;";
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?con_codigo", codMsg));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Contato();
                obj.Usu_codigo = new Usuario();
                obj.Usu_codigo.Usu_codigo = Convert.ToInt32(objDataReader["usu_codigo"]);
                obj.Con_titulo = Convert.ToString(objDataReader["con_titulo"]);
                obj.Con_conteudo = Convert.ToString(objDataReader["con_conteudo"]);
                obj.Con_codigo = Convert.ToInt32(objDataReader["con_codigo"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public Usuario SelecNomeUsuarioMensagem(int codUsu)
        {
            Usuario obj = null;

            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();
            string sql = @"SELECT usu_nome FROM usu_usuario WHERE usu_codigo = ?usu_codigo";
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codUsu));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Usuario();
                obj.Usu_nome = Convert.ToString(objDataReader["usu_nome"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public bool DelectMensagem(int codMsg)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = @"DELETE FROM con_contato WHERE con_codigo = ?con_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?con_codigo", codMsg));

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

        public static DataSet SelectClientesAll()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT usu_codigo, usu_nome, usu_status FROM usu_usuario WHERE tip_tipo_tip_cod <> 1;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }
    }
}