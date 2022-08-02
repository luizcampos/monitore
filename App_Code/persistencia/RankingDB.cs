using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class RankingDB
    {
        public static DataSet SelectRankingMembrosTarefasFeitas(int codGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT m.mem_codigo, u.usu_nome, (select count(*) from tar_tarefas t 
                            inner join tar_tarefas_has_mem_membros th
                            on t.tar_codigo = th.tar_tarefas_tar_codigo where th.mem_membros_mem_codigo = m.mem_codigo
                            and t.tar_status = 'Feito') AS qtde
                            FROM usu_usuario u INNER JOIN mem_membros m
                            ON u.usu_codigo = m.usu_usuario_usu_codigo
                            WHERE m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo ORDER BY qtde DESC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }
    }
}