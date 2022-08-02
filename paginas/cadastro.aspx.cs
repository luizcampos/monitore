using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Monitore.Classes;
using Monitore.Persistencias;

public partial class paginas_cadastro : System.Web.UI.Page
{
    private string email = "", username, cpf;
    private string senha;
    private string senha2;
    private string data;
    private int dia2, mes2, ano2, cont;

    Usuario usu = new Usuario(); //Objeto da classe UsuárioDB (comunicação)
    UsuarioDB db = new UsuarioDB();
    TipoUsuario t = new TipoUsuario();
    PerguntasSecretas p = new PerguntasSecretas();
    Sha512 sha512 = new Sha512();
    Plano pla = new Plano();
    PlanoDB plaDB = new PlanoDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtNome.Focus();

        if (!IsPostBack)
        {
            CarregarDllTipo();
            CarregarDllPerguntas();
        }
    }

    public void CarregarDllTipo()
    {
        //Carregar um DropDownList com o Banco de Dados 
        DataSet ds = TipoUsuarioDB.SelectAll();

        ddlTipo.DataSource = ds;
        ddlTipo.DataTextField = "tip_descricao";

        // Nome da coluna do Banco de dados 
        ddlTipo.DataValueField = "tip_cod";

        // ID da coluna do Banco 
        ddlTipo.DataBind();
        ddlTipo.Items.Insert(0, "Selecione");
    }

    public void CarregarDllPerguntas()
    {
        DataSet ds = PerguntasSecretasDB.SelectAll();

        ddlPergunta.DataSource = ds;
        ddlPergunta.DataTextField = "per_descricao";
        ddlPergunta.DataValueField = "per_codigo";

        ddlPergunta.DataBind();
        ddlPergunta.Items.Insert(0, "Selecione");
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        cont = 0;
        usu.Usu_nome = txtNome.Text;
        usu.Usu_sexo = ddlSexo.SelectedValue;
        usu.Usu_email = txtEmail.Text;
        usu.Usu_login = txtLogin.Text;
        usu.Usu_senha = txtSenha.Text;

        data = txtData.Text;

        if (((txtData.Text != "") || (txtData.Text != "0000-00-00") || (txtData.Text != "00-00-0000")) && (txtData.Text.Length >= 8))
        {
            dia2 = Convert.ToInt32(data.Substring(8, 2));
            mes2 = Convert.ToInt32(data.Substring(5, 2));
            ano2 = Convert.ToInt32(data.Substring(0, 4));
        }

        usu.Usu_dataNascimento = data;

        //FK TIPO
        t.Tip_cod = ddlTipo.SelectedIndex + 1;
        usu.Tip_cod = t;

        //FK TIPO
        p.Per_codigo = ddlPergunta.SelectedIndex;
        usu.Per_codigo = p;

        usu.Usu_resposta = txtResposta.Text;
        usu.Usu_cpf = txtCpf.Text;

        email = txtEmail.Text;
        senha = txtSenha.Text;
        senha2 = txtRepetirSenha.Text;
        username = txtLogin.Text;
        cpf = txtCpf.Text;

        char letra;
        string palavra = txtNome.Text;

        //Não permitir caracteres especiais no Nome
        for (int i = 0; i < txtNome.Text.Length; i++)
        {
            letra = palavra[i];

            if ((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) <= 47)
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 63))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) <= 96))) //caracteres especiais
            {
                txtNome.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite caracteres especiais no Nome. Ex: /#$%¨&*()@!.','error');</script>", false);
            }
        }

        palavra = txtLogin.Text;

        //Não permitir espaços no Login (ASCII)
        for (int i = 0; i < txtLogin.Text.Length; i++)
        {
            letra = palavra[i];
            if (Convert.ToInt32(letra).Equals(32)) //espaço
            {
                txtLogin.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite espaços no Login.','error');</script>", false);
            }

            if ((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) <= 47)
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 63))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) <= 96))) //caracteres especiais
            {
                txtLogin.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite caracteres especiais no Login. Ex: /#$%¨&*()@!.','error');</script>", false);
            }
        }

        palavra = txtEmail.Text;

        //Não permitir espaços no Email (ASCII)
        for (int i = 0; i < txtEmail.Text.Length; i++)
        {
            letra = palavra[i];
            if (Convert.ToInt32(letra).Equals(32)) //espaço
            {
                txtEmail.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite espaços no Email.','error');</script>", false);
            }

            if (((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) < 46))
                || ((Convert.ToInt32(letra) == 47)) //46 = .
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 63))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) < 95)) //95 = _
                || (Convert.ToInt32(letra) == 96)) //caracteres especiais
            {
                txtEmail.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite caracteres especiais no Email. Ex: /#$%¨&*()@!.','error');</script>", false);
            }
        }

        palavra = txtSenha.Text;

        //Não permitir espaços na Senha (ASCII)
        for (int i = 0; i < txtSenha.Text.Length; i++)
        {
            letra = palavra[i];
            if (Convert.ToInt32(letra).Equals(32)) //espaço
            {
                txtSenha.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite espaços na Senha.','error');</script>", false);
            }
        }

        palavra = txtResposta.Text;

        //Não permitir espaços na Resposta (ASCII)
        for (int i = 0; i < txtResposta.Text.Length; i++)
        {
            letra = palavra[i];

            if ((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) <= 47)
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 63))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) <= 96))) //caracteres especiais
            {
                txtResposta.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite caracteres especiais na Resposta. Ex: /#$%¨&*()@!.','error');</script>", false);
            }
        }

        palavra = txtSenha.Text;

        //Contar caracteres especiais na Senha (ASCII)
        for (int i = 0; i < txtSenha.Text.Length; i++)
        {
            letra = palavra[i];

            if ((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) <= 47)
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 64))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) <= 96))) //caracteres especiais
            {
                cont++;
            }
        }

        if ((txtNome.Text.Length < 4) || ((email.Contains("@") == false) || (email.Contains(".com") == false)
            || (txtEmail.Text == ""))
            || (txtLogin.Text.Length < 4) || ((senha.Where(c => char.IsLetter(c)).Count() <= 0)
            || (senha.Where(c => char.IsNumber(c)).Count() <= 0) || (senha.Length < 8))
            || (cont <= 0)
            || (ddlTipo.SelectedIndex == 0) || (ddlPergunta.SelectedIndex == 0)
            || (senha2 != senha) || (txtResposta.Text.Length < 1) || (txtCpf.Text.Length < 14)
            || (dia2 <= 0) || (dia2 > 31)
            || (mes2 <= 0) || (mes2 > 12)
            || (ano2 <= 1920) || (ano2 > 2018))
        {
            if (txtNome.Text.Length < 4)
            {
                txtNome.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Nome muito pequeno! Digite no mínimo oito caracteres.','error'); </script>", false);
            }

            if (txtData.Text == "")
            {
                txtData.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Digite uma data.','error');</script>", false);
            }

            else if ((dia2 <= 0) || (dia2 > 31) || (mes2 <= 0) || (mes2 > 12) || (ano2 <= 1920) || (ano2 > 2018))
            {
                txtData.Focus();

                if ((dia2 <= 0) || (dia2 > 31))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Data inválida: Dia (" + dia2 + ") não existe.','error');</script>", false);
                }

                if ((mes2 <= 0) || (mes2 > 12))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Data inválida: Mês (" + mes2 + ") não existe.','error');</script>", false);
                }

                if ((ano2 <= 1920) || (ano2 > 2018))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Data inválida: Ano (" + ano2 + ") não aprovado.','error');</script>", false);
                }
            }

            else if (txtLogin.Text.Length < 4)
            {
                txtLogin.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Login inválido! Digite no mínimo oito caracteres.','error');</script>", false);
            }

            else if ((email.Contains("@") == false) || (email.Contains(".com") == false))
            {
                txtEmail.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Email inválido.','error');</script>", false);
            }

            else if (txtCpf.Text.Length < 14)
            {
                txtCpf.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','CPF inválido! Tente novamente.','error');</script>", false);
            }

            else if (!ValidaCpf(txtCpf.Text))
            {
                txtCpf.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','CPF inválido! Tente novamente.','error');</script>", false);
            }

            //Verifica a presença de números e letras na string e se é maior que 8 caracteres
            else if ((senha.Where(c => char.IsLetter(c)).Count() <= 0) || (senha.Where(c => char.IsNumber(c)).Count() <= 0) || (senha.Length < 8))
            {
                txtSenha.Focus();
                ltrSenha.Text = @"<marquee style='background-color: #DC3545; color: white; border-radius: 5px; margin-top: 5px;'>
                                    Senha deve conter: Letras | Números | Um ou mais símbolos | 8 ou + caracteres
                                  </marquee>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Senha inválida!','error');</script>", false);
            }

            else if (cont <= 0)
            {
                txtSenha.Focus();
                ltrSenha.Text = @"<marquee style='background-color: #DC3545; color: white; border-radius: 5px; margin-top: 5px;'>
                                    Senha deve conter: Letras | Números | Um ou mais símbolos | 8 ou + caracteres
                                  </marquee>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Senha inválida! Digite no mínimo um caracter especial.','error');</script>", false);
            }

            else if (senha != senha2)
            {
                ltrSenha.Text = "";
                txtSenha.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','As senhas digitadas não são iguais.','error');</script>", false);
            }

            else if (senha2 != senha)
            {
                ltrSenha.Text = "";
                txtRepetirSenha.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','As senhas não conferem.','error');</script>", false);
            }

            else if (ddlTipo.SelectedIndex == 0)
            {
                ltrSenha.Text = "";
                ddlTipo.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Selecione um tipo de usuário.','error');</script>", false);
            }

            else if (ddlPergunta.SelectedIndex == 0)
            {
                ltrSenha.Text = "";
                ddlPergunta.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Selecione uma pergunta.','error');</script>", false);
            }

            else if (txtResposta.Text.Length < 1)
            {
                ltrSenha.Text = "";
                txtResposta.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Digite uma resposta para a pergunta.','error');</script>", false);
            }

            else
            {
                ltrSenha.Text = "";
            }

        }

        else
        {
            ltrSenha.Text = "";

            Usuario usuEmail = new Usuario();
            usuEmail = db.ValidarEmail(email); //recebe o objeto

            Usuario usuUsername = new Usuario();
            usuUsername = db.ValidarUsername(username); //recebe o objeto

            Usuario usuCPF = new Usuario();
            usuCPF = db.ValidarCPF(cpf); //recebe o objeto

            if (!ValidarUsername(usuUsername)) //caso seja true
            {
                txtLogin.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Login/Username já cadastrado em outra conta!','error');</script>", false);
            }

            else if (!ValidarEmail(usuEmail)) //caso seja true
            {
                txtEmail.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Email já cadastrado em outra conta!','error');</script>", false);
            }

            else if (!ValidarCPF(usuCPF)) //caso seja true
            {
                txtCpf.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','CPF já cadastrado em outra conta!','error');</script>", false);
            }

            else
            {
                lblDadosCadastro.Text = "<b> Nome completo: </b>" + usu.Usu_nome
                    + "</br> <b> Data de nascimento: </b>" + usu.Usu_dataNascimento
                    + "</br> <b> Login: </b>" + usu.Usu_login
                    + "</br> <b> Sexo: </b>" + usu.Usu_sexo
                    + "</br> <b> Email: </b>" + usu.Usu_email
                    + "</br> <b> CPF: </b>" + usu.Usu_cpf
                    + "</br> <b> Senha: </b>" + usu.Usu_senha
                    + "</br> <b> Tipo de usuário: </b>" + ddlTipo.SelectedItem
                    + "</br> <b> Pergunta de segurança: </b>" + ddlPergunta.SelectedItem
                    + "</br> <b> Resposta: </b>" + usu.Usu_resposta;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('confirmacaoCadastro').style.display = 'block';</script>", false);
            }
        }
    }

    private bool ValidarEmail(Usuario usuEmail)
    {
        bool retorno = true;
        if (usuEmail != null)
        {
            retorno = false;
        }
        return retorno;
    }

    private bool ValidarUsername(Usuario usuUsername)
    {
        bool retorno = true;
        if (usuUsername != null)
        {
            retorno = false;
        }
        return retorno;
    }

    private bool ValidarCPF(Usuario usuCPF)
    {
        bool retorno = true;
        if (usuCPF != null)
        {
            retorno = false;
        }
        return retorno;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
    protected void btnSim_Click(object sender, EventArgs e)
    {
        usu.Usu_nome = txtNome.Text;
        usu.Usu_sexo = ddlSexo.SelectedValue;
        usu.Usu_email = txtEmail.Text;
        usu.Usu_login = txtLogin.Text;
        usu.Usu_senha = txtSenha.Text;
        usu.Usu_dataNascimento = txtData.Text;
        usu.Usu_resposta = txtResposta.Text;
        usu.Usu_cpf = txtCpf.Text;

        //FK TIPO
        t.Tip_cod = ddlTipo.SelectedIndex + 1;
        usu.Tip_cod = t;

        //FK TIPO
        p.Per_codigo = ddlPergunta.SelectedIndex;
        usu.Per_codigo = p;

        usu.Usu_senha = Convert.ToString(sha512.SHA512(txtSenha.Text));

        switch (UsuarioDB.Insert(usu))
        {
            case 0:

                pla.Pla_tipo = "Basic";
                pla.Pla_preco = 0;
                string dataCriacao = Convert.ToString(DateTime.Now);
                string dia = dataCriacao.Substring(0, 2);
                string mes = dataCriacao.Substring(3, 2);
                string ano = dataCriacao.Substring(6, 4);

                pla.Pla_data_contratacao = ano + "-" + mes + "-" + dia; //intervalo da data só

                int anoVenci = Convert.ToInt32(ano) + 100;
                pla.Pla_validade = Convert.ToString(anoVenci) + "-" + mes + "-" + dia;

                usu = plaDB.SelectCodigoCadastrado(txtLogin.Text);
                pla.Usu_codigo = usu;

                //Cadastrar plano
                switch (PlanoDB.Insert(pla))
                {
                    case true:
                        break;

                    case false:
                        break;
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','Cadastro completado com sucesso.','success'); document.getElementById('login').style.display = 'block';</script>", false);
                txtNome.Text = "";
                txtData.Text = "";
                txtLogin.Text = "";
                ddlSexo.SelectedIndex = 0;
                txtEmail.Text = "";
                txtCpf.Text = "";
                txtSenha.Text = "";
                txtRepetirSenha.Text = "";
                ddlTipo.SelectedIndex = 0;
                ddlPergunta.SelectedIndex = 0;
                txtResposta.Text = "";
                ckbTermosUso.Checked = false;
                break;

            case -2:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','Cadastro não concluído. :(','error');</script>", false);
                break;
        }
    }
    protected void ckbTermosUso_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbTermosUso.Checked == true)
        {
            btnCadastrar.Enabled = true;
            ltrMensagem.Text = "";
        }

        if (ckbTermosUso.Checked == false)
        {
            btnCadastrar.Enabled = false;
            ltrMensagem.Text = @"<div class='col-xs-12 col-sm-12 col-md-8 col-lg-8'>
<div style='background-color: #DC3545; color: white; border-radius: 5px; margin-top: 5px;'>
                                    É NECESSÁRIO ACEITAR O TERMO DE USO PARA HABILITAR O BOTÃO CADASTRAR
                                  </div>";
        }
    }

    public bool ValidaCpf(string cpf)
    {

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;

        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
        {
            return false;
        }

        tempCpf = cpf.Substring(0, 9);

        soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * (multiplicador1[i]);
        }
        resto = soma % 11;

        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = 11 - resto;
        }

        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        int soma2 = 0;

        for (int i = 0; i < 10; i++)
        {
            soma2 += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        }

        resto = soma2 % 11;

        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = 11 - resto;
        }

        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }
}