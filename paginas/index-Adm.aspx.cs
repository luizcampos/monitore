using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monitore.Classes;
using Monitore.Persistencias;
using System.Data;

public partial class paginas_index_Adm : System.Web.UI.Page
{
    private int codigo;
    private string div;
    Usuario usuario = new Usuario();
    UsuarioDB usuarioDB = new UsuarioDB();
    AdminDB admDB = new AdminDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
        {
            Response.Redirect("sair.aspx");
        }

        if(!Page.IsPostBack)
        {
            carregaDadosIndex();
        }
    }

    private bool IsAdministrador(int tipo)
    {
        bool retorno = false;
        if (tipo == 1)
        {
            retorno = true;
        }
        return retorno;
    }

    public void carregaDadosIndex()
    {
        //LUCRO MENSAL

        DataSet lucroDS = AdminDB.SelectTotalLucroMensal();
        int qtde = lucroDS.Tables[0].Rows.Count;

        if (qtde > 0)
        {
            foreach (DataRow dr in lucroDS.Tables[0].Rows)
            {
                div += dr["totalLucro"];
            }
        }

        if (qtde <= 0)
        {
            div = "";
        }

        lblLucroMensal.Text = div;
        div = "";

        //USUÁRIOS PREMIUM

        DataSet usersPremiumDS = AdminDB.SelectTotalUsersPremium();
        qtde = usersPremiumDS.Tables[0].Rows.Count;

        if (qtde > 0)
        {
            foreach (DataRow dr in usersPremiumDS.Tables[0].Rows)
            {
                div += dr["totalUsersPremium"];
            }
        }

        if (qtde <= 0)
        {
            div = "";
        }

        lblPremiumTotal.Text = div;
        div = "";

        //TOTAL USUÁRIOS

        DataSet usersDS = AdminDB.SelectTotalUsers();
        qtde = usersDS.Tables[0].Rows.Count;

        if (qtde > 0)
        {
            foreach (DataRow dr in usersDS.Tables[0].Rows)
            {
                div += dr["totalUsers"];
            }
        }

        if (qtde <= 0)
        {
            div = "";
        }

        lblUsuarioTotal.Text = div;
        div = "";

        //NOVOS USUÁRIOS TODOS (LISTA)

        DataSet NovosDS = AdminDB.SelectTotalUltimosUsers();
        int qtdeUsersNovos = NovosDS.Tables[0].Rows.Count;
        rptListaNovos.DataSource = NovosDS.Tables[0].DefaultView;
        rptListaNovos.DataBind();

        if (qtdeUsersNovos > 0)
        {
            div = "";
        }

        if (qtdeUsersNovos <= 0)
        {
            div += @"<tr>
                        <p>Não há novos usuários</p>
                     </tr>";
        }
        ltrMensagemNovos.Text = div;

        //NOVOS USUÁRIOS PREMIUM (LISTA)

        DataSet PremiumDS = AdminDB.SelectTotalUltimosUsersPremium();
        int qtdeUsersPremium = PremiumDS.Tables[0].Rows.Count;
        rptListaPremium.DataSource = PremiumDS.Tables[0].DefaultView;
        rptListaPremium.DataBind();

        if (qtdeUsersPremium > 0)
        {
            div = "";
        }

        if (qtdeUsersPremium <= 0)
        {
            div += @"<tr>
                        <p>Não há novos usuários</p>
                     </tr>";
        }
        ltrMensagensPremium.Text = div;
    }
    protected void rptListaNovos_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        
    }
    protected void rptListaPremium_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}