using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Monitore.Classes;

namespace Monitore.Persistencias
{
    public class TipoUsuarioDB
    {
        public static DataSet SelectAll()  //DataSet é para um grande volume de dados
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            objConnection = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM tip_tipo ORDER BY tip_descricao LIMIT 1,3;", objConnection);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            // O objeto DataAdapter vai preencher o DataSet com os dados do BD, O método Fill 
            //é o responsável por preencher o DataSet 

            objConnection.Close();
            objCommand.Dispose();
            objConnection.Dispose();
            return ds;
        }
    }
}