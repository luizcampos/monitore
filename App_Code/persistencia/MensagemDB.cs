using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class MensagemDB
    {
        public static string erro;

        public int Insert(Mensagem men, int codRemetente, int codDestinatario)
        {
            try
            {
                IDbConnection objConexao;
                IDbCommand objCommand;

                string sql = "INSERT INTO men_mensagens(";
                sql += "men_data,";
                sql += "men_horario,";
                sql += "men_conteudo,";
                sql += "men_destinatario_mem_codigo,";
                sql += "men_remetente_mem_codigo";
                sql += ")";
                sql += " VALUES (";
                sql += "?men_data,";
                sql += "?men_horario,";
                sql += "?men_conteudo,";
                sql += "?men_destinatario_mem_codigo,";
                sql += "?men_remetente_mem_codigo";
                sql += ");";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?men_data", men.Men_data));
                objCommand.Parameters.Add(Mapped.Parameter("?men_horario", men.Men_horario));
                objCommand.Parameters.Add(Mapped.Parameter("?men_conteudo", men.Men_conteudo));
                objCommand.Parameters.Add(Mapped.Parameter("?men_destinatario_mem_codigo", codDestinatario));
                objCommand.Parameters.Add(Mapped.Parameter("?men_remetente_mem_codigo", codRemetente));

                objCommand.ExecuteNonQuery();

                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose();
            }

            catch (Exception e)
            {
                return -2;
                erro = e.GetBaseException().ToString();
            }

            return 0;
        }

        public static DataSet SelectMensagens(int codDestinatario, int codRemetente)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT men_codigo, DATE_FORMAT (men_data, '%d/%m/%Y') AS men_data, men_horario, men_conteudo, men_destinatario_mem_codigo,
                            men_remetente_mem_codigo, (select usu_nome FROM usu_usuario u INNER JOIN mem_membros m
                            ON m.usu_usuario_usu_codigo = u.usu_codigo 
                            WHERE m.mem_codigo = men_remetente_mem_codigo) AS destinatario
                            FROM men_mensagens WHERE men_destinatario_mem_codigo = ?men_destinatario_mem_codigo
                            OR men_destinatario_mem_codigo = ?men_remetente_mem_codigo
                            ORDER BY men_data DESC, men_horario DESC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?men_destinatario_mem_codigo", codDestinatario));
            objCommand.Parameters.Add(Mapped.Parameter("?men_remetente_mem_codigo", codRemetente));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }
    }
}