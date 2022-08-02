using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;
using System.Data;

namespace Monitore.Persistencias
{
    public class ComentarioDB
    {
        public int Insert(Comentario com)
        {
            try
            {
                IDbConnection objConexao;
                IDbCommand objCommand;

                string sql = "INSERT INTO com_comentario(";
                sql += "com_conteudo,";
                sql += "com_data,";
                sql += "com_horario,";
                sql += "usu_usuario_usu_codigo,";
                sql += "tar_tarefas_tar_codigo, ";
                sql += "com_cor,";
                sql += "com_borda,";
                sql += "com_cor_texto";
                sql += ")";
                sql += " VALUES (";
                sql += "?com_conteudo,";
                sql += "?com_data,";
                sql += "?com_horario,";
                sql += "?usu_usuario_usu_codigo,";
                sql += "?tar_tarefas_tar_codigo,";
                sql += "?com_cor,";
                sql += "?com_borda,";
                sql += "?com_cor_texto";
                sql += ");";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?com_conteudo", com.Com_conteudo));
                objCommand.Parameters.Add(Mapped.Parameter("?com_data", com.Com_data));
                objCommand.Parameters.Add(Mapped.Parameter("?com_horario", com.Com_horario));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", com.Usu_codigo.Usu_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_tarefas_tar_codigo", com.Tar_codigo.Tar_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?com_cor", com.Com_cor));
                objCommand.Parameters.Add(Mapped.Parameter("?com_borda", com.Com_borda));
                objCommand.Parameters.Add(Mapped.Parameter("?com_cor_texto", com.Com_cor_texto));

                objCommand.ExecuteNonQuery();

                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose(); //fecha conexão
            }

            catch (Exception e)
            {
                return -2;
            }

            return 0;
        }

        public static DataSet SelectComentariosTarefa(int codTar)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT c.com_codigo, c.com_conteudo, c.com_cor, c.com_borda, c.com_cor_texto, c.com_horario, u.usu_nome, DATE_FORMAT (com_data, '%d/%m/%Y') AS dataCom
                            FROM com_comentario c INNER JOIN usu_usuario u
                            ON c.usu_usuario_usu_codigo = u.usu_codigo
                            WHERE c.tar_tarefas_tar_codigo = ?tar_tarefas_tar_codigo ORDER BY com_data DESC, com_horario DESC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?tar_tarefas_tar_codigo", codTar));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public Comentario ValidaSeLogadoComentou(int codCom, int codUsu)
        {
            Comentario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();

            string sql = @"SELECT c.com_codigo, c.com_conteudo, DATE_FORMAT (com_data, '%d/%m/%Y') AS com_data, c.com_horario
                            FROM com_comentario c INNER JOIN usu_usuario u
                            ON u.usu_codigo = c.usu_usuario_usu_codigo
                            WHERE c.com_codigo = ?com_codigo AND c.usu_usuario_usu_codigo = ?usu_usuario_usu_codigo;";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?com_codigo", codCom));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", codUsu));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Comentario();
                obj.Com_codigo = Convert.ToInt32(objDataReader["com_codigo"]);
                obj.Com_conteudo = Convert.ToString(objDataReader["com_conteudo"]);
                obj.Com_data = Convert.ToString(objDataReader["com_data"]);
                obj.Com_horario = Convert.ToString(objDataReader["com_horario"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public bool DelectComentario(int codCom)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = @"DELETE FROM com_comentario WHERE com_codigo = ?com_codigo";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?com_codigo", codCom));

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