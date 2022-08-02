using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Monitore.Persistencias
{
    public class PerguntasSecretasDB
    {
        public static DataSet SelectAll()  //DataSet é para um grande volume de dados
        {
            DataSet ds = new DataSet();

            IDbConnection objConnection;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            objConnection = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM per_perguntas ORDER BY per_codigo", objConnection);
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