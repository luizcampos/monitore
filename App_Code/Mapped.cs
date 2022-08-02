using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//Importa funções do MySQL
using MySql.Data.MySqlClient;

//Trabalhar com Dataset
using System.Data;

//Permite visualizar o web.config
using System.Configuration;

public class Mapped
{
    //Método para abrir a conexão com o BD
    public static IDbConnection Connection()
    {
        MySqlConnection objConexao = new MySqlConnection(ConfigurationManager.AppSettings["strConexao"]);
        objConexao.Open(); return objConexao;
    }

    // Comandos SQL - Cria o objeto e valida o comando a ser executado (para querys)
    public static IDbCommand Command(string query, IDbConnection objConexao)
    {
        IDbCommand command = objConexao.CreateCommand();
        command.CommandText = query; return command;
    }

    // Executa as querys (Comandos)
    public static IDataAdapter Adapter(IDbCommand command)
    {
        IDbDataAdapter adap = new MySqlDataAdapter();
        adap.SelectCommand = command; return adap;
    }

    // Parametrização // Valida as entradas de dados antes de executar o comando Sql 
    public static IDbDataParameter Parameter(string nomeDoParametro, object valor)
    {
        return new MySqlParameter(nomeDoParametro, valor);
    }
}