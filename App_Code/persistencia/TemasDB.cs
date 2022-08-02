using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class TemasDB
    {
        public static DataSet SelectAll()  //DataSet é para um grande volume de dados
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            objConnection = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM tem_temas_associados ORDER BY tem_nome ASC;", objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            // O objeto DataAdapter vai preencher o DataSet com os dados do BD, O método Fill 
            //é o responsável por preencher o DataSet 

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public Temas SelectCodigoTema(string tema)
        {
            Temas obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT tem_codigo, tem_nome FROM tem_temas_associados WHERE tem_nome = ?tem_nome",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tem_nome", tema));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Temas();
                obj.Tem_codigo = Convert.ToInt32(objDataReader["tem_codigo"]);
                obj.Tem_nome = Convert.ToString(objDataReader["tem_nome"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Temas SelectTemaTarefa(int codTema)
        {
            Temas obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM tem_temas_associados WHERE tem_codigo = ?tem_codigo",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tem_codigo", codTema));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Temas();
                obj.Tem_codigo = Convert.ToInt32(objDataReader["tem_codigo"]);
                obj.Tem_nome = Convert.ToString(objDataReader["tem_nome"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public static DataSet SelectTemasGrupoAll(int codigoGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT DISTINCT t.tem_temas_tem_codigo, tm.tem_nome, t.gru_grupo_gru_codigo 
                            FROM tar_tarefas t INNER JOIN tem_temas_associados tm
                            ON t.tem_temas_tem_codigo = tm.tem_codigo
                            WHERE t.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codigoGrupo));

            objDataAdapter.Fill(ds);
            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();

            return ds;
        }

        public static DataSet SelectProgressoTemaIntegrante(int codTema, int codigoGrupo, int codMembro)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT *, (select count(*) from tar_tarefas where tem_temas_tem_codigo = ?tem_temas_tem_codigo
                            and gru_grupo_gru_codigo = ?gru_grupo_gru_codigo) AS TotalTema,
                            (select count(*) from tar_tarefas t inner join tar_tarefas_has_mem_membros th 
                            on t.tar_codigo = th.tar_tarefas_tar_codigo
                            where t.tem_temas_tem_codigo =  ?tem_temas_tem_codigo and t.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo 
                            and th.mem_membros_mem_codigo = ?mem_membros_mem_codigo and t.tar_status = 'Feito') AS TotalTemaFeito
                            FROM tar_tarefas t INNER JOIN tar_tarefas_has_mem_membros th
                            ON t.tar_codigo = th.tar_tarefas_tar_codigo WHERE t.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                            AND th.mem_membros_mem_codigo = ?mem_membros_mem_codigo AND t.tar_status = 'Feito'
                            AND t.tem_temas_tem_codigo = ?tem_temas_tem_codigo LIMIT 1;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?tem_temas_tem_codigo", codTema));
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codigoGrupo));
            objCommand.Parameters.Add(Mapped.Parameter("?mem_membros_mem_codigo", codMembro));

            objDataAdapter.Fill(ds);
            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();

            return ds;
        }

        public Temas SelectQtdeTarefaDoTema(int codTema, int codGrupo)
        {
            Temas obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            string sql = @"SELECT count(*) AS qtde FROM tar_tarefas 
                        WHERE tem_temas_tem_codigo = ?tem_temas_tem_codigo
                        AND gru_grupo_gru_codigo = ?gru_grupo_gru_codigo;";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tem_temas_tem_codigo", codTema));
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Temas();
                obj.Qtde_tarefas_tema = Convert.ToInt32(objDataReader["qtde"]);
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