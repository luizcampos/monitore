using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;
using System.Data;

namespace Monitore.Persistencias
{

    public class GrupoDB
    {
        public static string erro;

        public static DataSet SelectGruposUsuario(int codigo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT gru_codigo, gru_nome, gru_cor, gru_status, gru_chave, DATE_FORMAT (gru_data_criacao, '%d/%m/%Y') AS DataCriacao, (select count(*) from mem_membros where 
                        gru_grupo_gru_codigo = g.gru_codigo and mem_tipo in ('Aluno-líder', 'Aluno-integrante')) as totalAl,
                        (select count(*) from mem_membros where gru_grupo_gru_codigo = g.gru_codigo and mem_tipo = 'Professor') as totalPr,
                        ifnull(mediaFeito(g.gru_codigo),0) as tarefas
                            FROM gru_grupo g INNER JOIN mem_membros m ON m.gru_grupo_gru_codigo = g.gru_codigo 
                            WHERE m.usu_usuario_usu_codigo = ?codigo and g.gru_status = 'Ativo'
                            ORDER BY gru_data_criacao DESC, gru_nome ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataAdapter.Fill(ds);

            // O objeto DataAdapter vai preencher o DataSet com os dados do BD, O método Fill é o responsável por preencher o DataSet 

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;
        }

        public Grupo SelectCodigoGrupo (string chave)
        {
            Grupo obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT gru_codigo FROM gru_grupo WHERE gru_chave = ?gru_chave",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_chave", chave));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Grupo();
                obj.Gru_codigo = Convert.ToInt32(objDataReader["gru_codigo"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public static DataSet SelectPorcentagemGrupo(int codigo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT gru_codigo, gru_nome, gru_cor, gru_status, 
                            DATE_FORMAT (gru_data_criacao, '%d/%m/%Y') AS gru_data_criacao, 
                            ifnull(mediaFeito(g.gru_codigo),0) as tarefas FROM gru_grupo g 
                           WHERE g.gru_codigo = ?codigo;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataAdapter.Fill(ds);

            // O objeto DataAdapter vai preencher o DataSet com os dados do BD, O método Fill é o responsável por preencher o DataSet 

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public static DataSet SelectProfessoresGrupo(int codGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT u.usu_nome FROM mem_membros m INNER JOIN usu_usuario u 
                           ON m.usu_usuario_usu_codigo = u.usu_codigo and m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                           and m.mem_tipo = 'Professor' ORDER BY u.usu_nome ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

            objDataAdapter.Fill(ds);

            // O objeto DataAdapter vai preencher o DataSet com os dados do BD, O método Fill é o responsável por preencher o DataSet 

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public static DataSet SelectAlunosGrupo(int codGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT u.usu_nome FROM mem_membros m INNER JOIN usu_usuario u 
                           ON m.usu_usuario_usu_codigo = u.usu_codigo and m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                           and (m.mem_tipo = 'Aluno-líder' or m.mem_tipo = 'Aluno-integrante') ORDER BY u.usu_nome ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

            objDataAdapter.Fill(ds);

            // O objeto DataAdapter vai preencher o DataSet com os dados do BD, O método Fill é o responsável por preencher o DataSet 

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public Grupo SelectGrupoInfo(int codigoGru)
        {
            Grupo obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            string sql = @"SELECT gru_codigo, gru_nome, gru_cor, gru_status,
                            DATE_FORMAT (gru_data_criacao, '%d/%m/%Y') AS gru_data_criacao
                            FROM gru_grupo WHERE gru_codigo = ?gru_codigo;";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_codigo", codigoGru));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Grupo();
                obj.Gru_codigo = Convert.ToInt32(objDataReader["gru_codigo"]);
                obj.Gru_nome = Convert.ToString(objDataReader["gru_nome"]);
                obj.Gru_cor = Convert.ToString(objDataReader["gru_cor"]);
                obj.Gru_data_criacao = Convert.ToString(objDataReader["gru_data_criacao"]);
                obj.Gru_status = Convert.ToString(objDataReader["gru_status"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public bool InsertGrupoFull(Grupo grupo)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = @"INSERT INTO gru_grupo(gru_nome, gru_cor, gru_data_criacao, usu_usuario_usu_codigo, 
                            gru_status, gru_chave) VALUES (?gru_nome, ?gru_cor, ?gru_data_criacao, ?usu_usuario_usu_codigo, 
                            ?gru_status, ?gru_chave)";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?gru_nome", grupo.Gru_nome));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_cor", grupo.Gru_cor));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_data_criacao", grupo.Gru_data_criacao));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", grupo.Usu_codigo.Usu_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_status", grupo.Gru_status));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_chave", grupo.Gru_chave));
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

        public bool InsertLider(Membros membros)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = "INSERT INTO mem_membros(mem_tipo, gru_grupo_gru_codigo, usu_usuario_usu_codigo) VALUES (?mem_tipo, ?gru_grupo_gru_codigo ,?usu_usuario_usu_codigo)";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);

                objCommand.Parameters.Add(Mapped.Parameter("?mem_tipo", membros.Mem_tipo));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", membros.Gru_codigo.Gru_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", membros.Usu_codigo.Usu_codigo));

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

        public Grupo SelectCodigoGrupo(int codigoUsu)
        {
            Grupo obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            objCommand = Mapped.Command("SELECT gru_codigo FROM gru_grupo Where usu_usuario_usu_codigo = ?usu_usuario_usu_codigo order by gru_codigo desc limit 1;",
            objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", codigoUsu));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Grupo();
                obj.Gru_codigo = Convert.ToInt32(objDataReader["gru_codigo"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public Grupo SelectAlunoLider(int codigoUsu, int codGrupo)
        {
            Grupo obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            string sql = @"SELECT g.usu_usuario_usu_codigo, g.gru_codigo, u.usu_nome FROM gru_grupo g INNER JOIN usu_usuario u
                            ON g.usu_usuario_usu_codigo = u.usu_codigo
                            WHERE g.usu_usuario_usu_codigo = ?usu_usuario_usu_codigo 
                            AND g.gru_codigo = ?gru_codigo;";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", codigoUsu));
            objCommand.Parameters.Add(Mapped.Parameter("?gru_codigo", codGrupo));
            objDataReader = objCommand.ExecuteReader();

            while (objDataReader.Read())
            {
                obj = new Grupo();
                obj.Usu_codigo = new Usuario();
                obj.Usu_codigo.Usu_codigo = Convert.ToInt32(objDataReader["usu_usuario_usu_codigo"]);
                obj.Gru_codigo = Convert.ToInt32(objDataReader["gru_codigo"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return obj;
        }

        public bool UpdateEncerrarGrupo(string status, int codGrupo)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command("UPDATE gru_grupo SET gru_status = ?gru_status WHERE gru_codigo = ?gru_codigo;",
                objConexao);

                objCommand.Parameters.Add(Mapped.Parameter("?gru_codigo", codGrupo));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_status", status));

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

        public bool InsertMembroGrupo(string tipo, int codGrupo, Usuario usuario)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = "INSERT INTO mem_membros(mem_tipo, gru_grupo_gru_codigo, usu_usuario_usu_codigo) VALUES (?mem_tipo, ?gru_grupo_gru_codigo, ?usu_usuario_usu_codigo)";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?mem_tipo", tipo));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
                objCommand.Parameters.Add(Mapped.Parameter("?usu_usuario_usu_codigo", usuario.Usu_codigo));
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

        public bool DelectMembro(int codMembro)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = @"DELETE FROM mem_membros WHERE mem_codigo = ?mem_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?mem_codigo", codMembro));

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

        public Usuario SelectAlunoLiderCoroa(int codGrupo)
        {
            Usuario obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            objConexao = Mapped.Connection();

            string sql = @"SELECT u.usu_nome FROM mem_membros m INNER JOIN
                            usu_usuario u ON u.usu_codigo = m.usu_usuario_usu_codigo
                            WHERE mem_tipo = 'Aluno-líder' AND gru_grupo_gru_codigo = ?gru_grupo_gru_codigo;";

            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));
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

        public bool Update(Grupo gru)
        {
            try
            {
                System.Data.IDbConnection objConexao;
                System.Data.IDbCommand objCommand;

                string sql = "UPDATE gru_grupo ";
                sql += "SET gru_nome = ?gru_nome, ";
                sql += "gru_cor = ?gru_cor ";
                sql += "WHERE ";
                sql += "gru_codigo = ?gru_codigo;";

                objConexao = Mapped.Connection();
                objCommand = Mapped.Command(sql, objConexao);
                objCommand.Parameters.Add(Mapped.Parameter("?gru_codigo", gru.Gru_codigo));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_nome", gru.Gru_nome));
                objCommand.Parameters.Add(Mapped.Parameter("?gru_cor", gru.Gru_cor));

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

        public static DataSet SelectInfoMembrosGrupo(int codGrupo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT u.usu_codigo, u.usu_nome, u.usu_email, u.usu_username, u.tip_tipo_tip_cod, (select t.tip_descricao 
                        from tip_tipo t inner join usu_usuario us on t.tip_cod = us.tip_tipo_tip_cod 
                        where us.tip_tipo_tip_cod = u.tip_tipo_tip_cod and us.usu_codigo = u.usu_codigo) AS tipo
                        FROM mem_membros m INNER JOIN usu_usuario u 
                        ON m.usu_usuario_usu_codigo = u.usu_codigo and m.gru_grupo_gru_codigo = ?gru_grupo_gru_codigo
                        ORDER BY u.usu_nome ASC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?gru_grupo_gru_codigo", codGrupo));

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;

        }

        public static DataSet SelectInformacoesConfigGrupo(int codigo)
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT DATE_FORMAT (gru_data_criacao, '%d/%m/%Y') AS dataCriacao,
                            (SELECT count(*) FROM tar_tarefas WHERE gru_grupo_gru_codigo = ?codigo) AS totalTarefas,
                            (SELECT count(*) FROM mem_membros WHERE gru_grupo_gru_codigo = ?codigo) AS totalMembros
                            FROM gru_grupo WHERE gru_codigo = ?codigo;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataAdapter.Fill(ds);
            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); return ds;
        }
    }
}