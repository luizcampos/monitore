using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Monitore.Classes;
using System.Web.UI.WebControls;
using System.IO;

namespace Monitore.Persistencias
{
    public class ArquivoDB
    {
        public static int Inserir(Arquivo arq)
        {
            int errNumber = 0;
            try
            {
                IDbConnection objConexao;
                IDbCommand objCommand;
                string sql = "INSERT INTO arq_arquivos(";
                sql += "arq_codigo,";
                sql += "arq_caminho,";
                sql += "arq_miniatura,";
                sql += "arq_dataEnvio,";
                sql += "arq_horario,";
                sql += "tar_tarefas_tar_codigo";
                sql += ")";
                sql += " VALUES (";
                sql += "?arq_codigo,";
                sql += "?arq_caminho,";
                sql += "?arq_miniatura,";
                sql += "?arq_dataEnvio,";
                sql += "?arq_horario,";
                sql += "?tar_tarefas_tar_codigo";
                sql += ")";
                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?arq_codigo", arq.Arq_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?arq_caminho", arq.Arq_caminho));
                objCommand.Parameters.Add(Mapped.Parameter("?arq_miniatura", arq.Arq_miniatura));
                objCommand.Parameters.Add(Mapped.Parameter("?arq_dataEnvio", arq.Arq_dataEnvio));
                objCommand.Parameters.Add(Mapped.Parameter("?arq_horario", arq.Arq_horario));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_tarefas_tar_codigo", arq.Tar_codigo.Tar_codigo));
                objCommand.ExecuteNonQuery();
                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose();
            }
            catch (Exception ex)
            {
                errNumber = -2;
            }
            return errNumber;
        }

        public static int Excluir(int codArquivo)
        {
            int errNumber = 0;
            try
            {
                IDbConnection objConexao;
                IDbCommand objCommand;
                string sql = "DELETE FROM arq_arquivos WHERE arq_codigo = ?arq_codigo";
                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?arq_codigo", codArquivo));
                objCommand.ExecuteNonQuery();
                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose();
            }
            catch (Exception ex)
            {
                errNumber = -2;
            }
            return errNumber;
        }

        public static DataSet SelectArquivosTarefa(int codTar)
        {
            DataSet ds = new DataSet();
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(@"SELECT arq_codigo, arq_caminho, substring(arq_caminho,20,255) AS titulo,
                                          arq_miniatura, arq_horario,
                                          DATE_FORMAT (arq_dataEnvio, '%d/%m/%Y') AS arq_dataEnvio 
                                          FROM arq_arquivos WHERE tar_tarefas_tar_codigo = ?tar_tarefas_tar_codigo
                                          ORDER BY arq_dataEnvio DESC, arq_horario DESC;", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tar_tarefas_tar_codigo", codTar));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }

        public static Arquivo Selecionar(int codArquivo)
        {
            Arquivo arq = null;
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM arq_arquivos WHERE arq_codigo = ?arq_codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?arq_codigo", codArquivo));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                arq = new Arquivo();
                arq.Arq_codigo = Convert.ToInt32(objDataReader["arq_codigo"]);
                arq.Arq_caminho = Convert.ToString(objDataReader["arq_caminho"]);
                arq.Arq_miniatura = Convert.ToString(objDataReader["arq_miniatura"]);
                arq.Arq_dataEnvio = Convert.ToString(objDataReader["arq_dataEnvio"]);
                arq.Arq_horario = Convert.ToString(objDataReader["arq_horario"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return arq;
        }

        public static bool erro()
        {
            return false;
        }
    }
}