using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monitore.Classes;
using Monitore.Persistencias;
using System.Data;

public partial class paginas_planos : System.Web.UI.Page
{
    Contato con = new Contato();
    ContatoDB conDB = new ContatoDB();
    Usuario usu = new Usuario();
    UsuarioDB usuDB = new UsuarioDB();
    Plano pla = new Plano();
    PlanoDB plaDB = new PlanoDB();
    Usuario usuEmail = new Usuario();

    private string data;
    public int codUsuario;
    private int codUsu;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
        {
            Response.Redirect("sair.aspx");
        }

        if (!Page.IsPostBack)
        {
            carregarUsuarios();
        }

    }

    private string div = "";
    private void carregarUsuarios()
    {
        //NOMES DOS CLIENTES

        DataSet ds = ContatoDB.SelectClientesAll();
        int qtdeMensagens = ds.Tables[0].Rows.Count;
        rptListaClientes.DataSource = ds.Tables[0].DefaultView;
        rptListaClientes.DataBind();

        if (qtdeMensagens > 0)
        {
            div = "";
        }

        if (qtdeMensagens <= 0)
        {
            div += @"<tr>
                        <p>Não há usuários</p>
                     </tr>";
        }


    }
    protected void rptListaClientes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            usu = usuDB.Select(Convert.ToInt32(e.CommandArgument));

            lblNomePerfil.Text = usu.Usu_nome;
            lblSexoPerfil.Text = usu.Usu_sexo;
            lblCodigoCliente.Text = Convert.ToString(usu.Usu_codigo);
            lblStatus.Text = usu.Usu_status;
            data = usu.Usu_dataNascimento;

            string dia = data.Substring(0, 2);
            string mes = data.Substring(3, 2);
            string ano = data.Substring(6, 4);

            lblDataNascimento.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);
            lblEmail.Text = usu.Usu_email;
            lblCpf.Text = usu.Usu_cpf;

            pla = plaDB.SelectTipoPlano(Convert.ToInt32(e.CommandArgument));

            lblTipoConta.Text = pla.Pla_tipo;
            lblPreco.Text = "R$" + pla.Pla_preco;

            data = pla.Pla_data_contratacao;

            dia = data.Substring(0, 2);
            mes = data.Substring(3, 2);
            ano = data.Substring(6, 4);

            lblDataCon.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);

            data = pla.Pla_validade; ;

            dia = data.Substring(0, 2);
            mes = data.Substring(3, 2);
            ano = data.Substring(6, 4);

            lblDataValidade.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);

            if (lblStatus.Text.Equals("Ativo"))
            {
                btnAtivarUsuario.Visible = false;
                btnBloquearUsuario.Visible = true;
            }

            if (lblStatus.Text.Equals("Inativo"))
            {
                btnAtivarUsuario.Visible = true;
                btnBloquearUsuario.Visible = false;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('perfilUA').style.display = 'block';</script>", false);
        }
    }

    protected void btnRealizar_Contato_Click(object sender, EventArgs e)
    {
        lblUsernameMensagem.Text = lblNomePerfil.Text;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('contatoCliente').style.display = 'block';</script>", false);
    }

    protected void btnEnviarMsg_Click(object sender, EventArgs e)
    {
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
            codUsuario = Convert.ToInt32(lblCodigoCliente.Text);
            usuEmail = usuDB.SelecEmailUsuario(codUsuario);

            string email = usuEmail.Usu_email;
            string mensagem = "<p style='font-size: 15px; color: black;'>Olá <b>" + lblUsernameMensagem.Text
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('" + msg + "');</script>", false);
            }
        }
    }

    protected void btnBloquearUsuario_Click(object sender, EventArgs e)
    {
        codUsu = Convert.ToInt32(lblCodigoCliente.Text);

        switch (usuDB.UpdateStatusUsario("Inativo", Convert.ToInt32(codUsu)))
        {
            case true:

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Usuário bloqueado com sucesso!','O usuário foi inativo do sistema.','success');</script>", false);
                carregarUsuarios();
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro ao bloquear o usuário!','O usuário não foi bloquado com sucesso.','error');</script>", false);
                break;
        }
    }

    protected void btnAtivarUsuario_Click(object sender, EventArgs e)
    {
        codUsu = Convert.ToInt32(lblCodigoCliente.Text);

        switch (usuDB.UpdateStatusUsario("Ativo", Convert.ToInt32(codUsu)))
        {
            case true:

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Usuário ativado com sucesso!','O usuário foi ativado no sistema.','success');</script>", false);
                carregarUsuarios();
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro ao ativar o usuário!','O usuário não foi ativado com sucesso.','error');</script>", false);
                break;
        }
    }
}
