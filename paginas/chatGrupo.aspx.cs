using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Monitore.Classes;
using Monitore.Persistencias;

public partial class paginas_chatGrupo : System.Web.UI.Page
{
    Membros mem = new Membros();
    MembrosDB memDB = new MembrosDB();
    Mensagem men = new Mensagem();
    MensagemDB menDB = new MensagemDB();
    Plano pla = new Plano();
    PlanoDB plaDB = new PlanoDB();
    GrupoDB gruDB = new GrupoDB();

    private string chave;
    private int codUsu, codGrupo;

    protected void Page_Load(object sender, EventArgs e)
    {
        chave = Request["grupo"];
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        codUsu = Convert.ToInt32(Session["ID"]);

        if (Session["ID"] == null)
        {
            Response.Redirect("sair.aspx");
        }

        if (!Page.IsPostBack)
        {
            carregaAbasMembros(codGrupo);
            carregaFuncionalidadesPremium();
        }
    }

    private void carregaAbasMembros(int codGrupo)
    {
        DataSet abasMembrosDS = MembrosDB.SelectAllOutrosMembrosGrupos(codGrupo, codUsu);
        int qtde = abasMembrosDS.Tables[0].Rows.Count;
        rptAbasMembros.DataSource = abasMembrosDS.Tables[0].DefaultView;
        rptAbasMembros.DataBind();

        if (qtde == 0)
        {
            ltrMensagemAbas.Text = @"<br/><div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 22px;'>
                Não existem membros disponíveis.</div><br/>";
        }

        else
        {
            ltrMensagemAbas.Text = "";
        }
    }

    protected void rptAbasMembros_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Abrir")
        {
            lblCodDestinatario.Text = Convert.ToString(e.CommandArgument);

            mem = memDB.SelectCodigoMembroPorCodigo(codGrupo, codUsu);
            int codRemetente = mem.Mem_codigo;
            carregaMensagens(Convert.ToInt32(lblCodDestinatario.Text), codRemetente);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('boxChat').style.display = 'block';</script>", false);
        }
    }
    protected void rptMensagensChat_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if (txtMensagem.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Tente novamente!','Digite algo para enviar.','error');
                </script>", false);
        }

        else
        {
            men.Men_conteudo = txtMensagem.Text;

            string dataMensagem = Convert.ToString(DateTime.Now);

            string dia = dataMensagem.Substring(0, 2);
            string mes = dataMensagem.Substring(3, 2);
            string ano = dataMensagem.Substring(6, 4);

            string hora = dataMensagem.Substring(11, 2);
            string minutos = dataMensagem.Substring(14, 2);
            string segundos = dataMensagem.Substring(17, 2);

            men.Men_data = ano + "-" + mes + "-" + dia; //intervalo da data só
            men.Men_horario = hora + ":" + minutos + ":" + segundos;

            codUsu = Convert.ToInt32(Session["ID"]);
            mem = memDB.SelectCodigoMembroPorCodigo(codGrupo, codUsu);

            int codRemetente = mem.Mem_codigo;
            men.Men_remetente_mem_codigo = mem;

            int codDestinatario = Convert.ToInt32(lblCodDestinatario.Text);
            mem.Mem_codigo = codDestinatario;
            men.Men_destinatario_mem_codigo = mem;

            switch (menDB.Insert(men, codRemetente, codDestinatario))
            {
                case 0:
                    txtMensagem.Text = "";
                    carregaMensagens(Convert.ToInt32(lblCodDestinatario.Text), codRemetente);
                    break;

                case -2:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Erro não identificado!','Sua mensagem não foi enviada.','error');
                </script>", false);
                    break;
            }


        }
    }

    private void carregaMensagens(int codDestinatario, int codRemetente)
    {
        DataSet msgDS = MensagemDB.SelectMensagens(Convert.ToInt32(lblCodDestinatario.Text), codRemetente);
        int qtde = msgDS.Tables[0].Rows.Count;
        rptMensagensChat.DataSource = msgDS.Tables[0].DefaultView;
        rptMensagensChat.DataBind();

        if (qtde == 0)
        {
            ltrLegenda.Text = @"<br/><div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 16px;'>
                Ainda não existem mensagens entre vocês. <p><b>Que tal enviar uma?</b></p></div><br/>";
        }

        else
        {
            ltrLegenda.Text = "";
        }
    }

    private void carregaFuncionalidadesPremium()
    {
        codUsu = Convert.ToInt32(Session["ID"]);
        pla = plaDB.SelectTipoPlano(codUsu);

        if (pla.Pla_tipo.Equals("Premium"))
        {
            btnEnviar.Enabled = true;
            txtMensagem.Enabled = true;
        }

        else
        {
            btnEnviar.Enabled = false;
            txtMensagem.Enabled = false;
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('boxChat').style.display = 'none';</script>", false);

    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"];

        Response.Redirect("grupo.aspx?grupo=" + chave);
    }
}