using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monitore.Classes;
using Monitore.Persistencias;
using System.Data;
public partial class paginas_contato_adm : System.Web.UI.Page
{
    Contato con = new Contato();
    ContatoDB conDB = new ContatoDB();
    Usuario usu = new Usuario();
    UsuarioDB usuDB = new UsuarioDB();
    Usuario usuEmail = new Usuario();

    private string div = "";
    public int codUsuario = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        carregarMensagens();

        if (Session["ID"] == null)
        {
            Response.Redirect("sair.aspx");
        }
    }

    private void carregarMensagens()
    {
        //NOMES PROFESSORES

        DataSet ds = ContatoDB.SelectMensagensAll();
        int qtdeMensagens = ds.Tables[0].Rows.Count;
        rptListaMensagens.DataSource = ds.Tables[0].DefaultView;
        rptListaMensagens.DataBind();

        if (qtdeMensagens > 0)
        {
            div = "";
        }

        if (qtdeMensagens <= 0)
        {
            div += @"<tr>
                         <td>
                             <p>Não há mensagens</p>
                         </td>
                     </tr>";
        }

        ltrMensagens.Text = div;
    }
    protected void rptListaMensagens_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            int codMsg = Convert.ToInt32(e.CommandArgument);
            con = conDB.SelectMensagemInfos(codMsg);

            //Nome do usuário
            usu = conDB.SelecNomeUsuarioMensagem(con.Usu_codigo.Usu_codigo);

            lblUsarioAdm.Text = usu.Usu_nome;
            lblCodigoUsuario.Text += Convert.ToString(con.Usu_codigo.Usu_codigo);
            lblAssuntoAdm.Text = con.Con_titulo;
            lblDescricaoAdm.Text = con.Con_conteudo;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('contatoAdm').style.display = 'block';</script>", false);
        }

        if (e.CommandName == "Perfil")
        {
            int codUsu = Convert.ToInt32(e.CommandArgument);
            usu = usuDB.Select(codUsu);

            txtNomePerfilCliente.Text = usu.Usu_nome;
            txtUsernameCliente.Text = usu.Usu_login;
            txtDataPerfilCliente.Text = usu.Usu_dataNascimento;
            txtEmailPerfilCliente.Text = usu.Usu_email;
            txtCPFPerfilCliente.Text = usu.Usu_cpf;

            if (usu.Usu_sexo.Equals("M"))
            {
                ddlSexoPerfilCliente.SelectedIndex = 1;
            }

            if (usu.Usu_sexo.Equals("F"))
            {
                ddlSexoPerfilCliente.SelectedIndex = 0;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('perfilCliente').style.display = 'block';</script>", false);
        }

        if (e.CommandName == "Excluir")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('excluirMensagem').style.display = 'block';</script>", false);

            int codMsg = Convert.ToInt32(e.CommandArgument);
            con = conDB.SelectMensagemInfos(codMsg);

            txtAssuntoDeletar.Text = con.Con_titulo;
            lblTextoDeletar.Text = con.Con_conteudo;
            lblCod.Text = Convert.ToString(codMsg);
        }
    }
    protected void btnEnviarContatoAdm_Click(object sender, EventArgs e)
    {
        if (txtMensagem.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Digite uma resposta.','error'); document.getElementById('contatoAdm').style.display = 'block';</script>", false);
        }

        else
        {
            int codUsu = Convert.ToInt32(lblCodigoUsuario.Text);
            Usuario usuEmail = new Usuario();
            usuEmail = usuDB.SelecEmailUsuario(codUsu);

            //ENVIAR EMAIL

            string email = usuEmail.Usu_email;
            string mensagem = "<p style='font-size: 15px; color: black;'>Olá <b>" + lblUsarioAdm.Text + "</b>, viemos informar a resposta referente a sua mensagem de contato: <br/>Título: " + lblAssuntoAdm.Text
                + "<br/>Mensagem: " + lblDescricaoAdm.Text + "<br/><br/><b>Resposta de Monitore:</b> " + txtMensagem.Text +
                "<br/><br/><b>NÃO RESPONDA ESSE EMAIL!</b> <br/>OBS: Caso não seja você, ignore essa mensagem.</p>";
            string msg = Email.EnviarEmailAdm(email, "Resposta a sua mensagem - Monitore", mensagem);

            if (msg.Equals("Erro ao enviar e-mail com a nova senha."))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('" + msg + "'); document.getElementById('contatoAdm').style.display = 'block';</script>", false);
            }

            else
            {
                txtMensagem.Text = "";
                lblCodigoUsuario.Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('" + msg + "');</script>", false);
            }
        }

    }
    protected void btnSimExcluir_Click(object sender, EventArgs e)
    {
        int codMsg = Convert.ToInt32(lblCod.Text);

        switch (conDB.DelectMensagem(codMsg))
        {
            case true:
                txtAssuntoDeletar.Text = "";
                lblTextoDeletar.Text = "";
                lblCod.Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','Mensagem foi excluída com sucesso.', 'success');</script>", false);
                carregarMensagens();
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','Mensagem não foi excluída.', 'error'); document.getElementById('excluirMensagem').style.display = 'block';</script>", false);
                break;
        }
    }
    protected void btnEnviarMsg_Click(object sender, EventArgs e)
    {
        if (ddlUsernames.SelectedIndex == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Selecione um username.','error');document.getElementById('contatoCliente').style.display = 'block';</script>", false);
        }

        if (txtAssuntoMensagem.Text.Length < 10)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Assunto muito curto.','error');document.getElementById('contatoCliente').style.display = 'block';</script>", false);
        }

        if (txtTextoMensagem.Text.Length < 10)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Mensagem muito curta.','error');document.getElementById('contatoCliente').style.display = 'block';</script>", false);
        }

        else
        {
            codUsuario = Convert.ToInt32(ddlUsernames.SelectedValue);
            usuEmail = usuDB.SelecEmailUsuario(codUsuario);

            string email = usuEmail.Usu_email;
            string mensagem = "<p style='font-size: 15px; color: black;'>Olá <b>" + ddlUsernames.SelectedItem
                + "</b>, <br/><br/>          " + txtTextoMensagem.Text + "<br/><br/><br/></p><p style='font-size: 12px; color: black;'><i>Nós agradecemos seu comprometimento!</i></p>";

            string msg = Email.EnviarEmailAdm(email, txtAssuntoMensagem.Text + " - Monitore", mensagem);

            if (msg.Equals("Erro ao enviar e-mail com a nova senha."))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('" + msg + "'); document.getElementById('contatoAdm').style.display = 'block';</script>", false);
            }

            else
            {
                txtAssuntoMensagem.Text = "";
                txtTextoMensagem.Text = "";
                ddlUsernames.SelectedIndex = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('" + msg + "');</script>", false);
            }
        }
    }

    public void carregarUsuarios()
    {
        DataSet ds = MembrosDB.SelectAllUsernames();

        ddlUsernames.DataSource = ds;
        ddlUsernames.DataTextField = "usu_username";

        ddlUsernames.DataValueField = "usu_codigo";

        ddlUsernames.DataBind();
        ddlUsernames.Items.Insert(0, "Selecione");
    }
    protected void btnEnviarMensagemCliente_Click(object sender, EventArgs e)
    {
        carregarUsuarios();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('contatoCliente').style.display = 'block';</script>", false);
    }
}