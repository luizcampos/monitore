using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monitore.Classes;
using Monitore.Persistencias;
using System.Data;

public partial class paginas_index_Logado : System.Web.UI.Page
{
    Grupo gru = new Grupo();
    GrupoDB gruDB = new GrupoDB();
    Usuario usu = new Usuario();
    Membros mem = new Membros();
    Sha512 sha512 = new Sha512();
    private int codigo;
    private string senhaGerada;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
        {
            Response.Redirect("sair.aspx");
        }

        if (!Page.IsPostBack)
        {
            codigo = Convert.ToInt32(Session["ID"]);
            usu.Usu_codigo = codigo;
            carregaGrupos(codigo);
        }
        ltrCor.Text = "";
    }

    private void carregaGrupos(int usu)
    {
        DataSet ds = GrupoDB.SelectGruposUsuario(usu);
        int qtde = ds.Tables[0].Rows.Count;
        rptLista.DataSource = ds.Tables[0].DefaultView;
        rptLista.DataBind();
        if (qtde == 0)
        {
            lblGrupos.Text = @"<div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 22px;'>
            Você não está inserido em nenhum grupo.
            </div>";

            btnTutorial.Visible = true;
            ltrGif.Text = @"<img src='../img/gifs/setaGif.gif' width='50px' height='50px;'/>";
        }

        else
        {
            lblGrupos.Text = "";
            btnTutorial.Visible = false;
            ltrGif.Text = "";
        }
    }

    protected void btnCriarGrupo_Click(object sender, EventArgs e)
    {
        if (txtNomeGrupo.Text == "")
        {
            txtNomeGrupo.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Nome do grupo não inserido.','error');document.getElementById('criarGrupo').style.display = 'block';</script>", false);

        }

        else
        {
            gru.Gru_nome = txtNomeGrupo.Text;
            gru.Gru_cor = dllCor.SelectedValue;
            string data = Convert.ToString(DateTime.Now);
            string dia = data.Substring(0, 2);
            string mes = data.Substring(3, 2);
            string ano = data.Substring(6, 4);
            string hora = data.Substring(11, 2);
            string minuto = data.Substring(14, 2);
            string segundo = data.Substring(17, 2);

            gru.Gru_data_criacao = ano + "-" + mes + "-" + dia;

            codigo = Convert.ToInt32(Session["ID"]);
            usu.Usu_codigo = codigo;
            gru.Usu_codigo = usu;

            gru.Gru_status = "Ativo";
            senhaGerada = GeraSenha(); //Outra fragmento aleatório para a chave do grupo

            gru.Gru_chave = Convert.ToString(sha512.SHA512(txtNomeGrupo.Text)) + senhaGerada + segundo + 
                minuto + hora + ano + mes + dia;

            if (gruDB.InsertGrupoFull(gru))
            {

                txtNomeGrupo.Text = "";
                dllCor.SelectedIndex = 0;

                codigo = Convert.ToInt32(Session["ID"]);
                usu.Usu_codigo = codigo;
                mem.Usu_codigo = usu;

                gru = gruDB.SelectCodigoGrupo(codigo);
                mem.Gru_codigo = gru;

                mem.Mem_tipo = "Aluno-lider";

                switch (gruDB.InsertLider(mem))
                {
                    case true:
                        btnTutorial.Visible = false;
                        break;

                    case false:
                        break;
                }
            }

            carregaGrupos(codigo);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','Grupo criado com sucesso.','success'); document.getElementById('infosGrupo').style.display = 'block';</script></script>", false);
        }
    }

    protected void btnEntrarGrupos_Click(object sender, EventArgs e)
    {
        Response.Redirect("grupo.aspx");
    }

    private bool IsUsuario(int tipo)
    {
        bool retorno = false;
        if (tipo == 2)
        {
            retorno = true;
        }
        return retorno;
    }
    protected void rptLista_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Entrar")
        {
            string chave = Convert.ToString(e.CommandArgument);
            gru.Gru_chave = chave;

            Response.Redirect("grupo.aspx?grupo=" + chave);

        }
    }
    protected void dllCor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cor = dllCor.SelectedValue;
        ltrCor.Text = "<div style='background-color: " + cor + ";border-color: " + cor + "; width: 216px; height: 10px;'> </div>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('criarGrupo').style.display = 'block';</script>", false);
    }
    protected void btnNaoEntendi_Click(object sender, EventArgs e)
    {
        Response.Redirect("ajuda-Logado.aspx");
    }
    protected void btnTutorial_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"
                <script>
                    document.getElementById('tutorial1').style.display = 'block';
                    document.getElementById('msgTutorial1').style.display = 'block';
                </script>", false);

        btnTutorial.Visible = false;
        ltrGif.Text = "";
    }

    private string GeraSenha()
    {
        string guid = Guid.NewGuid().ToString().Replace("-", "");

        Random aleatorio = new Random();
        Int32 tamanhoSenha = aleatorio.Next(6, 10);

        for (Int32 i = 0; i <= tamanhoSenha; i++)
        {
            senhaGerada += guid.Substring(aleatorio.Next(1, guid.Length), 1);
        }

        return senhaGerada;
    }
}