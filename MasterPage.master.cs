using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Monitore.Classes;
using Monitore.Persistencias;

public partial class MasterPage : System.Web.UI.MasterPage
{
    UsuarioDB db = new UsuarioDB();
    Usuario usuario = new Usuario();

    Usuario obj = new Usuario();
    RecuperacaoSenhaDB objdb = new RecuperacaoSenhaDB();

    Sha512 sha512 = new Sha512();
    private string senhaCrip;

    private int codigoPer;
    private string login, senhaGerada = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void Application_Error(object sender, EventArgs e)
    {
        Response.Redirect("/paginas/erro.aspx");
    }

    private bool UsuarioEncontrado(Usuario usuario)
    {
        bool retorno = false;
        if (usuario != null)
        {
            retorno = true;
        }
        return retorno;
    }
    
    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string senha = txtSenha.Text.Trim();

        // Se há texto no campo Username e senha
        if ((txtUsername.Text == "") && (txtSenha.Text == ""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Username e Senha não preenchido.','error'); document.getElementById('login').style.display = 'block';</script>", false);
            txtUsername.Focus();
        }

        // Sé há texto no campo Username OU Senha
        else if ((txtUsername.Text == "") || (txtSenha.Text == ""))
        {
            //Campo Username
            if (txtUsername.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Username não preenchido.','error'); document.getElementById('login').style.display = 'block';</script>", false);
                txtUsername.Focus();

            }
            //Campo Senha
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Senha não preenchida.','error'); document.getElementById('login').style.display = 'block';</script>", false);
                txtSenha.Focus();
            }
        }


        UsuarioDB db = new UsuarioDB();
        Usuario usuario = new Usuario();

        usuario = db.ValidarUsername(txtUsername.Text);

        if (!ValidarUsername(usuario)) //caso não exista o username
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Username não existe.','error'); document.getElementById('login').style.display = 'block';</script>", false);
        }

        else //caso exista o username
        {
            string senhaCrip = Convert.ToString(sha512.SHA512(senha));

            usuario = db.SelectSenha(username);
            string senhaBanco = usuario.Usu_senha; //senha do banco

            if (senhaBanco.Length <= 10) //é a nova senha da recuperação de senha
            {
                usuario = db.Autentica(username, senha);
            }

            if (senhaBanco.Length > 10) //é a senha criptografa
            {
                usuario = db.Autentica(username, Convert.ToString(sha512.SHA512(senha)));
            }

            if (!UsuarioEncontrado(usuario))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Verifique se os dados foram inseridos corretamente.','error'); document.getElementById('login').style.display = 'block';</script>", false);

                txtUsername.Focus();
                return;
            }

            if (usuario.Usu_status == "Inativo")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Usuário Bloqueado!','Não é possível logar com essa conta.','error'); document.getElementById('login').style.display = 'block';</script>", false);
            }

            else
            {
                Session["ID"] = usuario.Usu_codigo;

                switch (usuario.Tip_cod.Tip_cod)
                {
                    case 1:
                        Response.Redirect("/paginas/index-Adm.aspx");
                        break;
                    case 2:
                        Response.Redirect("/paginas/index-Logado.aspx");
                        break;
                    case 3:
                        Response.Redirect("/paginas/index-Logado.aspx");
                        break;
                    default:
                        Response.Redirect("/paginas/index.aspx");
                        break;
                }
            }
        }
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

    //ALTERAÇAO 12/04/2018
    public void lbEsqueceu_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Username não preenchido.','error'); document.getElementById('login').style.display = 'block';</script>", false);
            txtUsername.Focus();
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script> document.getElementById('esqueceuSenha').style.display = 'block';</script>", false);

            obj.Usu_login = txtUsername.Text;
            obj = objdb.SelectCodigoPergunta(obj.Usu_login);

            codigoPer = Convert.ToInt32(obj.Per_codigo.Per_codigo);
            login = txtUsername.Text;

            switch (codigoPer)
            {
                case 1:
                    lblPerguntas.Text = "Qual o nome da(o) seu melhor amiga(o) na infância?";
                    break;

                case 2:
                    lblPerguntas.Text = "Qual o nome do seu primeiro professor?";
                    break;

                case 3:
                    lblPerguntas.Text = "Qual o nome do seu primeiro amor?";
                    break;
            }

        }
    }

    private bool RespostaEncontrado(Usuario usuario)
    {
        bool retorno = false;
        if (usuario != null)
        {
            retorno = true;
        }
        return retorno;
    }

    protected void btnEnviarEsqueceuSenha_Click(object sender, EventArgs e)
    {
        RecuperacaoSenhaDB rec = new RecuperacaoSenhaDB();
        Usuario usu = new Usuario();
        string resposta = txtSenhaRecuperar.Text;
        obj.Usu_login = txtUsername.Text;
        obj = objdb.SelectCodigoPergunta(obj.Usu_login);

        string username = Convert.ToString(obj.Usu_login);
        int codigo = Convert.ToInt32(obj.Per_codigo.Per_codigo);
        string email = Convert.ToString(obj.Usu_email);
        string senha = Convert.ToString(obj.Usu_senha);

        if (txtSenhaRecuperar.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Resposta não preenchido.','error'); document.getElementById('login').style.display = 'block'; document.getElementById('esqueceuSenha').style.display = 'block';</script>", false);
        }
        else
        {
            usu = rec.SelectResposta(codigo, username, resposta);
            string mensagem;

            if (RespostaEncontrado(usu))
            {
                senhaGerada = GeraSenha(); //guarda senha gerada
                senhaCrip = Convert.ToString(sha512.SHA512(senhaGerada)); //criptografa a senha gerada

                //ltrCodigo.Text = "<br/>A sua nova senha é: <b>" + senhaGerada + "</b>. Acesse sua conta com essa senha, para poder alterá-la depois.<br/>";
                //ltrCodigo.Visible = true;

                mensagem = "A sua nova senha é: " + senhaGerada + ". Acesse sua conta com essa senha, para poder alterá-la depois."; // A mensagem é a senha
                string msg = Email.EnviarEmail(email, "Recuperação de Senha - Monitore", mensagem);

                //UPDATE NA SENHA ATUAL (SENHA ALEATÓRIA GERADA)

                switch (objdb.UpdateSenhaNova(senhaCrip, obj))
                {
                    case true:
                        txtSenhaRecuperar.Text = "";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('" + msg + "'); document.getElementById('login').style.display = 'block';</script>", false);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('login').style.display = 'block'; document.getElementById('esqueceuSenha').style.display = 'block';</script>", false);
                        break;

                    case false:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('" + msg + "'); document.getElementById('login').style.display = 'block';</script>", false);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('login').style.display = 'block'; document.getElementById('esqueceuSenha').style.display = 'block';</script>", false);
                        break;
                }

                return;
            }

            else
            {
                if (!RespostaEncontrado(usu))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Resposta errada.','error'); document.getElementById('login').style.display = 'block'; document.getElementById('esqueceuSenha').style.display = 'block';</script>", false);
                }

            }
        }
    }

    private bool ValidarUsername(Usuario usuario)
    {
        bool retorno = false;
        if (usuario != null)
        {
            retorno = true;
        }
        return retorno;
    }
}
