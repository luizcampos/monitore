using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;
using System.Data;

namespace Monitore.Persistencias
{
    public class MembrosTarefasDB
    {
        public static DataSet SelectMembrosTarefa(int codTar)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT (select usu_nome from usu_usuario WHERE usu_codigo = m.usu_usuario_usu_codigo) as Nomes
                            FROM mem_membros m INNER JOIN tar_tarefas_has_mem_membros th
                            ON th.mem_membros_mem_codigo = m.mem_codigo WHERE th.tar_tarefas_tar_codigo = ?tar_tarefas_tar_codigo
                            ORDER BY nomes ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?tar_tarefas_tar_codigo", codTar));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public bool DelectMembrosTarefa(int codTar)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = "DELETE FROM tar_tarefas_has_mem_membros ";
                sql += "WHERE ";
                sql += "tar_tarefas_tar_codigo = ?tar_tarefas_tar_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?tar_tarefas_tar_codigo", codTar));

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

        public static DataSet SelectNomesIntegrantes(int codigo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT u.usu_codigo, u.usu_nome, m.mem_codigo, m.gru_grupo_gru_codigo 
                            FROM usu_usuario u INNER JOIN mem_membros m
                            ON u.usu_codigo = m.usu_usuario_usu_codigo
                            WHERE m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codigo));

            objDataAdapter.Fill(ds);
            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); 
            
            return ds;
        }

        public static DataSet SelectProgressoIntegrante(int codigoGrupo, int codMembro, int codUser)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT usu_nome, (select count(*) from tar_tarefas t INNER JOIN tar_tarefas_has_mem_membros th
                            ON t.tar_codigo = th.tar_tarefas_tar_codigo WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                            AND th.mem_membros_mem_codigo = ?mem_membros_mem_codigo) AS TotalIndividual,
                            (select count(*) from tar_tarefas t inner join tar_tarefas_has_mem_membros th
                            on t.tar_codigo = th.tar_tarefas_tar_codigo where th.mem_membros_mem_codigo = ?mem_membros_mem_codigo) 
                            AS Total, (select count(*) from tar_tarefas t inner join tar_tarefas_has_mem_membros th
                            on t.tar_codigo = th.tar_tarefas_tar_codigo where th.mem_membros_mem_codigo = ?mem_membros_mem_codigo
                            and t.tar_status = 'Feito') AS TotalFeito,
                            ifnull(mediaIntegrante(?gru_grupo_gru_codigo, ?mem_membros_mem_codigo),0) as Progresso
                            FROM usu_usuario WHERE usu_codigo = ?usu_codigo;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codigoGrupo));
            objCommand.Parameters.Add(Mapped.Parameter("?mem_membros_mem_codigo", codMembro));
            objCommand.Parameters.Add(Mapped.Parameter("?usu_codigo", codUser));

            objDataAdapter.Fill(ds);
            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();

            return ds;
        }
    }
}