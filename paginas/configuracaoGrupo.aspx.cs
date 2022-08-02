using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Monitore.Classes;
using Monitore.Persistencias;

public partial class paginas_configuracaoGrupo : System.Web.UI.Page
{
    Grupo gru = new Grupo();
    GrupoDB gruDB = new GrupoDB();

    private string chave;
    private int codGrupo;

    protected void Page_Load(object sender, EventArgs e)
    {
        verificaSeLider();

        if (Session["ID"] == null)
        {
            Response.Redirect("sair.aspx");
        }

        if (!Page.IsPostBack) //executa ao selecionar um componente
        {
            int codigo = Convert.ToInt32(Session["ID"]);

            chave = Request["grupo"]; //Pegar código do grupo pela URL
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            carregarDadosGrupo();
            verificaSeLider();
        }
    }

    private void verificaSeLider()
    {
        chave = Request["grupo"];
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        int codigoUsu = Convert.ToInt32(Session["ID"]);

        gru = gruDB.SelectAlunoLider(codigoUsu, codGrupo);

        if (!LiderEncontrado(gru)) //se for o líder
        {
            btnAlterarDados.Enabled = true;
        }
        else //se não for o líder
        {
            btnAlterarDados.Enabled = false;
        }
    }

    private bool LiderEncontrado(Grupo gru)
    {
        bool retorno = true;
        if (gru != null)
        {
            retorno = false;
        }
        return retorno;
    }

    private void carregarDadosGrupo()
    {
        chave = Request["grupo"];
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        gru = gruDB.SelectGrupoInfo(codGrupo);

        txtNomeGrupoConfig.Text = gru.Gru_nome;
        dllCor.SelectedValue = gru.Gru_cor;

        string cor = gru.Gru_cor;
        ltrCor.Text = "<div style='background-color: " + cor + ";border-color: " + cor + "; width: 350px; height: 10px;'> </div>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('criarGrupo').style.display = 'block';</script>", false);

        txtNomeGrupoConfig.Enabled = false;
        dllCor.Enabled = false;

        //INFORMAÇÕES

        DataSet infosDS = GrupoDB.SelectInformacoesConfigGrupo(codGrupo);
        int qtde = infosDS.Tables[0].Rows.Count;

        if (qtde > 0)
        {
            foreach (DataRow dr in infosDS.Tables[0].Rows)
            {
                ltrInfosGrupo.Text = @"<br/><div class='row text-left' style='font-size:18px;'>
                <div class='col-xs-4 col-sm-6 col-md-5 col-lg-4'>
                    <p class='text-icones'><b>Data de criação:</b> " + dr["dataCriacao"] + @"</p>
                </div>

                <div class='col-xs-8 col-sm-6 col-md-7 col-lg-8'>
                    <p></p>
                </div>
            </div>
            <div class='row text-left' style='font-size:18px;'>
            <div class='col-xs-4 col-sm-6 col-md-5 col-lg-4'>
                    <p class='text-icones'><b>Total de membros:</b> " + dr["totalMembros"] + @"</p>
                </div>

                <div class='col-xs-8 col-sm-6 col-md-7 col-lg-8'>
                    <p></p>
                </div></div>
            <div class='row text-left' style='font-size:18px;'>
            <div class='col-xs-4 col-sm-6 col-md-5 col-lg-4'>
                    <p class='text-icones'><b>Total de tarefas:</b> " + dr["totalTarefas"] + @"</p>
                </div>

                <div class='col-xs-8 col-sm-6 col-md-7 col-lg-8'>
                    <p></p>
                </div></div>";
            }
            
        }

        else
        {
            ltrInfosGrupo.Text = "";
        }

        //MEMBROS

        DataSet gruMembrosDS = GrupoDB.SelectInfoMembrosGrupo(codGrupo);
        qtde = gruMembrosDS.Tables[0].Rows.Count;
        rptMembrosConfig.DataSource = gruMembrosDS.Tables[0].DefaultView;
        rptMembrosConfig.DataBind();

        if (qtde == 0)
        {
            ltrMensagemMembros.Text = @"<br/><tr>
                Não existem membros nesse grupo.</tr><br/>";
        }

        else
        {
            ltrMensagemMembros.Text = "";
        }

        //HISTÓRICO TAREFAS

        DataSet historicoTarefasDS = TarefaDB.SelectHistoricoTarefasGrupo(codGrupo);
        qtde = historicoTarefasDS.Tables[0].Rows.Count;
        rptHistoricoTarefas.DataSource = historicoTarefasDS.Tables[0].DefaultView;
        rptHistoricoTarefas.DataBind();

        if (qtde == 0)
        {
            ltrMensagemHistorico.Text = @"<br/><tr>
                Não existem tarefas nesse grupo.</tr><br/>";
        }

        else
        {
            ltrMensagemHistorico.Text = "";
        }
    }
    protected void dllCor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cor = dllCor.SelectedValue;
        ltrCor.Text = "<div style='background-color: " + cor + ";border-color: " + cor + "; width: 350px; height: 10px;'> </div>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('criarGrupo').style.display = 'block';</script>", false);
    }
    protected void btnAlterarDados_Click(object sender, EventArgs e)
    {
        txtNomeGrupoConfig.Enabled = true;
        dllCor.Enabled = true;
        btnAlterarDados.Visible = false;
        btnOkConfig.Visible = true;
    }
    protected void btnOkConfig_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"]; 
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        Grupo gruConfig = new Grupo();

        gruConfig.Gru_codigo = codGrupo;
        gruConfig.Gru_nome = txtNomeGrupoConfig.Text;
        gruConfig.Gru_cor = dllCor.SelectedValue;

        switch (gruDB.Update(gruConfig))
        {
            case true:
                txtNomeGrupoConfig.Enabled = false;
                dllCor.Enabled = false;
                btnAlterarDados.Visible = true;
                btnOkConfig.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"
                        <script>
                            swal('Bom trabalho!','Informações alteradas com sucesso.','success'); 
                        </script>", false);
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"
                        <script>
                            swal('Erro não identificado!','Informações não foram alteradas.','success'); 
                        </script>", false);
                break;
        }
    }
    protected void rptMembrosConfig_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void rptHistoricoTarefas_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"];

        Response.Redirect("grupo.aspx?grupo=" + chave);
    }
}