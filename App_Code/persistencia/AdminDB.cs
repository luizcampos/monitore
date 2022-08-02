using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monitore.Classes;
using System.Data;

namespace Monitore.Persistencias
{
    public class AdminDB
    {
        public static string erro;

        public static DataSet SelectTotalUsersPremium()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT count(pla_codigo) AS totalUsersPremium FROM pla_plano WHERE pla_tipo = 'Premium'";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose(); 
            return ds;
        }

        public static DataSet SelectTotalUsers()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT DISTINCT count(usu_codigo) AS totalUsers FROM usu_usuario;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public static DataSet SelectTotalLucroMensal()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT SUM(pla_preco) AS totalLucro FROM pla_plano WHERE pla_tipo = 'Premium';";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public static DataSet SelectTotalUltimosUsers()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT * FROM usu_usuario ORDER BY usu_codigo DESC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }

        public static DataSet SelectTotalUltimosUsersPremium()
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;
            objConnection = Mapped.Connection();

            string sql = @"SELECT * FROM usu_usuario u INNER JOIN pla_plano p
                            ON u.usu_codigo = p.usu_usuario_usu_codigo 
                            WHERE p.pla_tipo = 'Premium' ORDER BY usu_codigo DESC;";

            objCommand = Mapped.Command(sql, objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);

            objDataAdapter.Fill(ds);

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }
    }
}