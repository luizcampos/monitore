using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;
using System.Data;

namespace Monitore.Persistencias
{
    public class MembrosDB
    {
        public static DataSet SelectAllMembrosGrupos(int codGrupo)  //DataSet é para um grande volume de dados
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            objConnection = Mapped.Connection();
            objCommand = Mapped.Command("SELECT m.mem_codigo, u.usu_nome FROM mem_membros m INNER JOIN usu_usuario u ON m.usu_usuario_usu_codigo = u.usu_codigo and gru_grupo_gru_codigo = ?gru_grupo_gru_codigo ORDER BY u.usu_nome ASC", objConnection);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            // O objeto DataAdapter vai preencher o DataSet com os dados do BD, O método Fill 
            //é o responsável por preencher o DataSet 

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public static DataSet SelectCodigoMembro(int codGrupo, string nomeMembro) 
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            objConnection = Mapped.Connection();
            objCommand = Mapped.Command("SELECT m.mem_codigo FROM mem_membros m INNER JOIN usu_usuario u ON m.usu_usuario_usu_codigo = u.usu_codigo and m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo and u.usu_nome = ?usu_nome;", objConnection);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_nome", nomeMembro));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public  Membros SelectCodigoMembroPorCodigo(int codGrupo, int codUsu)
        {
            Membros obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();

            objCommand = Mapped.Command("SELECT m.mem_codigo FROM mem_membros m INNER JOIN usu_usuario u ON m.usu_usuario_usu_codigo = u.usu_codigo and m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo and u.usu_codigo = ?usu_codigo;", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codUsu));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Membros();
                obj.Mem_codigo = Convert.ToInt32(objDataReader["mem_codigo"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Membros SelectTipoMembro(int codMembro)
        {
            Membros obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            string sql = @"SELECT mem_tipo FROM mem_membros WHERE mem_codigo = ?mem_codigo";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?mem_codigo", codMembro));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Membros();
                obj.Mem_tipo = Convert.ToString(objDataReader["mem_tipo"]);
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Membros ValidaUsernameComMembros(string username, int codGrupo)
        {
            Membros obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            string sql = @"SELECT m.mem_codigo FROM mem_membros m INNER JOIN usu_usuario u
                            ON m.usu_usuario_usu_codigo = u.usu_codigo
                            WHERE u.usu_username = ?usu_username
                            AND m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_username", username));
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Membros();
                obj.Mem_codigo = Convert.ToInt32(objDataReader["mem_codigo"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public static DataSet SelectAllUsernames()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            objConnection = Mapped.Connection();
            objCommand = Mapped.Command("SELECT usu_codigo, usu_username FROM usu_usuario ORDER BY usu_username ASC", objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public static DataSet SelectAllOutrosMembrosGrupos(int codGrupo, int codUsu)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            objConnection = Mapped.Connection();

            string sql = @"SELECT m.mem_codigo, u.usu_nome 
                            FROM mem_membros m INNER JOIN usu_usuario u 
                            ON m.usu_usuario_usu_codigo = u.usu_codigo 
                            and gru_grupo_gru_codigo = ?gru_grupo_gru_codigo 
                            WHERE u.usu_codigo <> ?usu_codigo
                            ORDER BY u.usu_nome ASC";

            objCommand = Mapped.Command(sql, objConnection);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codUsu));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }
    }
}