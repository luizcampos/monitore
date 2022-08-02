using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;
using System.Data;

namespace Monitore.Persistencias
{
    public class TarefaDB
    {
        public static string erro;
        public static int retorno = 0;


        public static int Insert(Tarefa tar)
        {
            Grupo gru = new Grupo();
            Temas tem = new Temas();

            try
            {
                IDbConnection objConexao;
                IDbCommand objCommand;

                string sql = "INSERT INTO tar_tarefas(";
                sql += "tar_nome,";
                sql += "tar_prazo,";
                sql += "tar_data_criacao,";
                sql += "tem_temas_tem_codigo,";
                sql += "gru_grupo_gru_codigo,";
                sql += "tar_tipo,";
                sql += "tar_qtde_membros,";
                sql += "tar_descricao, ";
                sql += "tar_realizada,";
                sql += "tar_status";
                sql += ")";
                sql += " VALUES (";
                sql += "?tar_nome,";
                sql += "?tar_prazo,";
                sql += "?tar_data_criacao,";
                sql += "?tem_temas_tem_codigo,";
                sql += "?gru_grupo_gru_codigo,";
                sql += "?tar_tipo,";
                sql += "?tar_qtde_membros,";
                sql += "?tar_descricao, ";
                sql += "?tar_realizada, ";
                sql += "?tar_status";
                sql += ");";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?tar_nome", tar.Tar_nome));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_prazo", tar.Tar_prazo));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_data_criacao", tar.Tar_data_criacao));
                objCommand.Parameters.Add(Mapped.Parameter("?tem_temas_tem_codigo", tar.Tem_codigo.Tem_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", tar.Gru_codigo.Gru_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_tipo", tar.Tar_tipo));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_qtde_membros", tar.Tar_qtde_membros));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_descricao", tar.Tar_descricao));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_realizada", tar.Tar_realizada));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_status", tar.Tar_status));

                objCommand.ExecuteNonQuery();

                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose(); //fecha conexão
            }

            catch (Exception e)
            {
                return -2;
                erro = e.GetBaseException().ToString();
            }

            return 0;
        }

        public static int InsertMembrosTarefa(MembrosTarefas memTar)
        {
            Membros mem = new Membros();
            Tarefa tar = new Tarefa();

            try
            {
                IDbConnection objConexao;
                IDbCommand objCommand;

                string sql = "INSERT INTO tar_tarefas_has_mem_membros(";
                sql += "tar_tarefas_tar_codigo,";
                sql += "mem_membros_mem_codigo";
                sql += ")";
                sql += " VALUES (";
                sql += "?tar_tarefas_tar_codigo,";
                sql += "?mem_membros_mem_codigo";
                sql += ");";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?tar_tarefas_tar_codigo", memTar.Tar_codigo.Tar_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?mem_membros_mem_codigo", memTar.Mem_codigo.Mem_codigo));

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

        public Tarefa SelectCodigoUltimaTarefa(int codigoGru)
        {
            Tarefa obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            //Pega o código da última tarefa cadastrada
            objCommand = Mapped.Command("SELECT tar_codigo FROM tar_tarefas WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo ORDER BY tar_codigo DESC LIMIT 1;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codigoGru));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Tarefa();
                obj.Tar_codigo = Convert.ToInt32(objDataReader["tar_codigo"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public int UpdateTipoTarefa(Tarefa tar, int codGrupo)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = "UPDATE tar_tarefas ";
                sql += "SET tar_tipo = ?tar_tipo, ";
                sql += "tar_qtde_membros = ?tar_qtde_membros ";
                sql += "WHERE ";
                sql += "tar_codigo = ?tar_codigo and ";
                sql += "gru_grupo_gru_codigo = ?gru_grupo_gru_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?tar_tipo", tar.Tar_tipo));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_qtde_membros", tar.Tar_qtde_membros));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_codigo", tar.Tar_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

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

        public static DataSet SelectTarefasAFazer(int codGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT DISTINCT tar_codigo, tar_nome, DATE_FORMAT (tar_prazo, '%d/%m/%Y') AS prazo, DATE_FORMAT (tar_data_criacao, '%d/%m/%Y') AS tar_data_criacao, tem_temas_tem_codigo,
                           tar_tipo, tar_qtde_membros, tar_descricao, tar_realizada, tar_status FROM tar_tarefas 
                           WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo and tar_status = 'A fazer'
                           ORDER BY tar_prazo ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public static DataSet SelectTarefasFazendo(int codGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT DISTINCT tar_codigo, tar_nome, DATE_FORMAT (tar_prazo, '%d/%m/%Y') AS prazo, DATE_FORMAT (tar_data_criacao, '%d/%m/%Y') AS tar_data_criacao, tem_temas_tem_codigo,
                           tar_tipo, tar_qtde_membros, tar_descricao, tar_realizada, tar_status FROM tar_tarefas 
                           WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo and tar_status = 'Fazendo'
                           ORDER BY tar_prazo ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public static DataSet SelectTarefasFeito(int codGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT DISTINCT tar_codigo, tar_nome, DATE_FORMAT (tar_prazo, '%d/%m/%Y') AS prazo, DATE_FORMAT (tar_data_criacao, '%d/%m/%Y') AS tar_data_criacao, tem_temas_tem_codigo,
                           tar_tipo, tar_qtde_membros, tar_descricao, tar_realizada, tar_status FROM tar_tarefas 
                           WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo and tar_status = 'Feito'
                           ORDER BY tar_prazo ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public Tarefa SelectTarefaInfo(int codTar)
        {
            Tarefa obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            string sql = @"SELECT tar_codigo, tar_nome, tar_prazo, DATE_FORMAT (tar_data_criacao, '%d/%m/%Y') AS tar_data_criacao,
                            tar_tipo, tar_qtde_membros, tar_descricao, tar_realizada, tar_status, tem_temas_tem_codigo
                            FROM tar_tarefas WHERE tar_codigo = ?tar_codigo;";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tar_codigo", codTar));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Tarefa();
                obj.Tar_codigo = Convert.ToInt32(objDataReader["tar_codigo"]);
                obj.Tar_nome = Convert.ToString(objDataReader["tar_nome"]);
                obj.Tar_prazo = Convert.ToString(objDataReader["tar_prazo"]);
                obj.Tar_data_criacao = Convert.ToString(objDataReader["tar_data_criacao"]);
                obj.Tar_tipo = Convert.ToString(objDataReader["tar_tipo"]);
                obj.Tar_qtde_membros = Convert.ToInt32(objDataReader["tar_qtde_membros"]);
                obj.Tar_descricao = Convert.ToString(objDataReader["tar_descricao"]);
                obj.Tar_realizada = Convert.ToString(objDataReader["tar_realizada"]);
                obj.Tar_status = Convert.ToString(objDataReader["tar_status"]);
                obj.Tem_codigo = new Temas();
                obj.Tem_codigo.Tem_codigo = Convert.ToInt32(objDataReader["tem_temas_tem_codigo"]);

            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public bool DelectTarefa(int codTar, int codGrupo)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = @"DELETE FROM tar_tarefas WHERE tar_codigo = ?tar_codigo and gru_grupo_gru_codigo = ?gru_grupo_gru_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?tar_codigo", codTar));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

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

        public bool UpdateStatusTarefa(string status, int codTar)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command("UPDATE tar_tarefas SET tar_status = ?tar_status WHERE tar_codigo = ?tar_codigo;",
                objConexao);

                objCommand.Parameters.Add(Mapped.Parameter("?tar_status", status));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_codigo", codTar));

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

        public bool UpdateTarefaRealizada(string realizada, int codTar)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command("UPDATE tar_tarefas SET tar_realizada = ?tar_realizada WHERE tar_codigo = ?tar_codigo;",
                objConexao);

                objCommand.Parameters.Add(Mapped.Parameter("?tar_realizada", realizada));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_codigo", codTar));

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

        public Tarefa SelectStatusTarefa(int codTar)
        {
            Tarefa obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            string sql = @"SELECT tar_status FROM tar_tarefas 
                           WHERE tar_codigo = ?tar_codigo LIMIT 1;";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tar_codigo", codTar));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Tarefa();
                obj.Tar_status = Convert.ToString(objDataReader["tar_status"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public static DataSet SelectTotalTarefas(int codigo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT (SELECT count(*) FROM tar_tarefas WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                            AND tar_status = 'Feito') AS Feito, (SELECT count(*) 
                            FROM tar_tarefas WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo) AS Total;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codigo));

            objDataAdapter.Fill(ds);
            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); 
            return ds;
        }

        public bool DelectMembrosDeSuasTarefas(int codGrupo, int codMembro)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = @"DELETE th FROM tar_tarefas_has_mem_membros th INNER JOIN tar_tarefas t 
                                ON t.tar_codigo = th.tar_tarefas_tar_codigo
                                WHERE t.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                                AND th.mem_membros_mem_codigo = ?mem_membros_mem_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
                objCommand.Parameters.Add(Mapped.Parameter("?mem_membros_mem_codigo", codMembro));

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

        public static DataSet SelectHistoricoTarefasGrupo(int codigo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT tar_nome, tar_descricao, DATE_FORMAT (tar_data_criacao, '%d/%m/%Y') AS dataCriacao, 
                            tar_tipo, tar_qtde_membros, tar_status FROM tar_tarefas 
                            WHERE gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                            ORDER BY tar_data_criacao DESC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codigo));

            objDataAdapter.Fill(ds);
            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public static int AlteraTarefa(Tarefa tar)
        {
            Grupo gru = new Grupo();
            Temas tem = new Temas();

            try
            {
                IDbConnection objConexao;
                IDbCommand objCommand;

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(@"UPDATE tar_tarefas SET tar_nome = ?tar_nome, 
                                              tar_prazo = ?tar_prazo, tem_temas_tem_codigo = ?tem_temas_tem_codigo, 
                                              tar_descricao = ?tar_descricao WHERE tar_codigo = ?tar_codigo;",
                objConexao);

                objCommand.Parameters.Add(Mapped.Parameter("?tar_nome", tar.Tar_nome));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_prazo", tar.Tar_prazo));
                objCommand.Parameters.Add(Mapped.Parameter("?tem_temas_tem_codigo", tar.Tem_codigo.Tem_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_descricao", tar.Tar_descricao));
                objCommand.Parameters.Add(Mapped.Parameter("?tar_codigo", tar.Tar_codigo));

                objCommand.ExecuteNonQuery();

                objConexao.Close();
                objCommand.Dispose();
                objConexao.Dispose(); //fecha conexão
            }

            catch (Exception e)
            {
                return -2;
                erro = e.GetBaseException().ToString();
            }

            return 0;
        }
    }
}