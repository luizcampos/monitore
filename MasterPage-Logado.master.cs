using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monitore.Classes;
using Monitore.Persistencias;

public partial class MasterPage_Logado : System.Web.UI.MasterPage
{
    private int codigo, dia2, mes2, ano2, cont;
    private string email, cpf, palavra, senha, senha2, mensagem, msg, data, novaSenha, username;
    private char letra;

    Usuario usuario = new Usuario();
    UsuarioDB usuarioDB = new UsuarioDB();
    PerfilDB db = new PerfilDB();
    Sha512 sha512 = new Sha512();
    Contato con = new Contato();
    ContatoDB conDB = new ContatoDB();
    Plano pla = new Plano();
    PlanoDB plaDB = new PlanoDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        codigo = Convert.ToInt32(Session["ID"]); //guarda o ID do usuário da sessão aberta
        usuario = usuarioDB.SelectUsername(codigo); //enviar o ID para o método, instanciando o objeto
        btnAbrirPerfil.Text = usuario.Usu_login; //exibe o username
    }

    private void Application_Error(object sender, EventArgs e)
    {
        Response.Redirect("/paginas/erro.aspx");
    }

    private void SelectPerfil()
    {
        //PERFIL

        usuario = usuarioDB.Select(codigo); //enviar o ID para o método, instanciando o objeto

        txtNomePerfil.Text = usuario.Usu_nome;
        txtCPFPerfil.Text = usuario.Usu_cpf;

        data = usuario.Usu_dataNascimento;

        string dia = data.Substring(0, 2);
        string mes = data.Substring(3, 2);
        string ano = data.Substring(6, 4);

        txtDataPerfilExibicao.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);
        txtEmailPerfil.Text = usuario.Usu_email;
        txtUsernamePerfil.Text = usuario.Usu_login;

        if (usuario.Usu_sexo.Equals("F"))
        {
            ddlSexoPerfil.SelectedIndex = 0;
        }

        if (usuario.Usu_sexo.Equals("M"))
        {
            ddlSexoPerfil.SelectedIndex = 1;
        }

        //ÍCONE DO TIPO DE CONTA
        codigo = Convert.ToInt32(Session["ID"]);
        pla = plaDB.SelectTipoPlano(codigo);

        if (pla.Pla_tipo.Equals("Premium"))
        {
            ltrIcone.Text = @"<img src='/img/icones/premium.png' width='60px' height='20px'/>";
        }

        else
        {
            ltrIcone.Text = @"<img src='/img/icones/basic.png' width='60px' height='20px'/>";
        }
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("index.aspx");
    }

    protected void btnOkPerfil_Click(object sender, EventArgs e)
    {
        email = txtEmailPerfil.Text;
        data = txtDataPerfil.Text;

        dia2 = Convert.ToInt32(data.Substring(8, 2));
        mes2 = Convert.ToInt32(data.Substring(5, 2));
        ano2 = Convert.ToInt32(data.Substring(0, 4));

        cpf = txtCPFPerfil.Text;
        username = txtUsernamePerfil.Text;

        char letra;
        string palavra = txtNomePerfil.Text;

        //Não permitir caracteres especiais no Nome Perfil
        for (int i = 0; i < txtNomePerfil.Text.Length; i++)
        {
            letra = palavra[i];

            if ((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) <= 47)
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 63))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) <= 96))) //caracteres especiais
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite caracteres especiais no Nome. Ex: /#$%¨&*()@!.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                txtNomePerfil.Focus();
            }
        }

        palavra = txtEmailPerfil.Text;

        //Não permitir caracteres especiais no Email Perfil
        for (int i = 0; i < txtEmailPerfil.Text.Length; i++)
        {
            letra = palavra[i];

            if (Convert.ToInt32(letra).Equals(32)) //espaço
            {
                txtEmailPerfil.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite espaços no Email.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
            }

            if (((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) < 46))
                || ((Convert.ToInt32(letra) == 47)) //46 = .
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 63))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) < 95)) //95 = _
                || (Convert.ToInt32(letra) == 96)) //caracteres especiais
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite caracteres especiais no Email. Ex: /#$%¨&*()@!.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                txtEmailPerfil.Focus();
            }
        }

        palavra = txtUsernamePerfil.Text;

        //Não permitir espaços e caracteres especiais no Username (ASCII)
        for (int i = 0; i < txtUsernamePerfil.Text.Length; i++)
        {
            letra = palavra[i];
            if (Convert.ToInt32(letra).Equals(32)) //espaço
            {
                txtUsernamePerfil.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite espaços no username.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
            }

            if ((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) <= 47)
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 63))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) <= 96))) //caracteres especiais
            {
                txtUsernamePerfil.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite caracteres especiais no nome. Ex: /#$%¨&*()@!.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
            }
        }

        if ((txtNomePerfil.Text.Length < 4) || ((email.Contains("@") == false) || (email.Contains(".com") == false) || (txtEmailPerfil.Text == ""))
            || (txtCPFPerfil.Text.Length < 14)
            || (dia2 <= 0) || (dia2 > 31)
            || (mes2 <= 0) || (mes2 > 12)
            || (ano2 <= 1920) || (ano2 > 2018)
            || (txtUsernamePerfil.Text.Length < 4))
        {
            if (txtNomePerfil.Text.Length < 4)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Nome muito pequeno! Digite no mínimo oito caracteres.','error'); document.getElementById('perfil').style.display = 'block';</script>", false);
                SelectPerfil();
            }

            else if ((dia2 <= 0) || (dia2 > 31) || (mes2 <= 0) || (mes2 > 12) || (ano2 <= 1920) || (ano2 > 2018))
            {
                if ((dia2 <= 0) || (dia2 > 31))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Data inválida: Dia (" + dia2 + ") não existe.','error'); document.getElementById('perfil').style.display = 'block';</script>", false);
                }

                if ((mes2 <= 0) || (mes2 > 12))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Data inválida: Mês (" + mes2 + ") não existe.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                }

                if ((ano2 <= 1920) || (ano2 > 2018))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Data inválida: Ano (" + ano2 + ") não aprovado.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                }
                SelectPerfil();
            }

            else if ((email.Contains("@") == false) || (email.Contains(".com") == false))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Email inválido.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                SelectPerfil();
            }

            else if (txtCPFPerfil.Text.Length < 14)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','CPF inválido! Tente novamente.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                SelectPerfil();
            }

            else if (!ValidaCpf(txtCPFPerfil.Text))
            {
                txtCPFPerfil.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','CPF inv�lido! Tente novamente.','error');</script>", false);
            }  

            else if (txtUsernamePerfil.Text.Length < 4)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Digite um username maior.','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                SelectPerfil();
            }

            else
            {

            }

        }

        else
        {
            codigo = Convert.ToInt32(Session["ID"]);

            PerfilDB db = new PerfilDB();

            Usuario usuEmail = new Usuario();
            usuEmail = db.ValidarEmailPerfil(email, codigo); //recebe o objeto

            Usuario usuCPF = new Usuario();
            usuCPF = db.ValidarCPFPerfil(cpf, codigo); //recebe o objeto

            Usuario usuUsername = new Usuario();
            usuUsername = db.ValidarUsernamePerfil(username, codigo); //recebe o objeto

            if (!ValidarEmail(usuEmail)) //caso seja true
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Email já cadastrado em outra conta!','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                SelectPerfil();
            }

            else if (!ValidarCPF(usuCPF)) //caso seja true
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','CPF já cadastrado em outra conta!','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                txtCPFPerfil.Focus();
                SelectPerfil();
            }

            else if (!ValidarUsername(usuUsername)) //caso seja true
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Username já cadastrado em outra conta!','error');document.getElementById('perfil').style.display = 'block';</script>", false);
                txtUsernamePerfil.Focus();
                SelectPerfil();
            }

            else
            {
                lblDadosCadastro.Text = "<b> Nome completo: </b>" + txtNomePerfil.Text
                    + "</br> <b> Sexo: </b>" + ddlSexoPerfil.SelectedItem
                    + "</br> <b> Data de nascimento: </b>" + dia2 + "/" + mes2 + "/" + ano2
                    + "</br> <b> Email: </b>" + txtEmailPerfil.Text
                    + "</br> <b> Username: </b>" + txtUsernamePerfil.Text
                    + "</br> <b> CPF: </b>" + txtCPFPerfil.Text;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('confirmacaoPerfil').style.display = 'block';</script>", false);
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
    private bool ValidarCPF(Usuario usuCPF)
    {
        bool retorno = true;
        if (usuCPF != null)
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

    protected void btnAbrirPerfil_Click(object sender, EventArgs e)
    {
        SelectPerfil();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('perfil').style.display = 'block';</script>", false);
    }
    protected void btnFecharPerfil_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('perfil').style.display = 'none';</script>", false);

    }
    protected void btnSim_Click(object sender, EventArgs e) //ALTERAÇÃO DO PERFIL (DADOS)
    {
        codigo = Convert.ToInt32(Session["ID"]);

        usuario.Usu_codigo = codigo;
        usuario.Usu_nome = txtNomePerfil.Text;
        usuario.Usu_cpf = txtCPFPerfil.Text;
        usuario.Usu_dataNascimento = txtDataPerfil.Text;
        usuario.Usu_email = txtEmailPerfil.Text;
        usuario.Usu_sexo = ddlSexoPerfil.SelectedValue;
        usuario.Usu_login = txtUsernamePerfil.Text;

        switch (usuarioDB.Update(usuario))
        {
            case true:
                codigo = Convert.ToInt32(Session["ID"]);
                usuario = usuarioDB.SelectUsername(codigo);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','Dados alterados com sucesso.','success');</script>", false);
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Ocorreu um erro!','Dados não alterados.','error');</script>", false);
                break;
        }
    }
    protected void btnOk_Click(object sender, EventArgs e) //ALTERAÇÃO DA SENHA PERFIL
    {
        senha = txtSenhaNova.Text;
        senha2 = txtRepitaSenha.Text;
        cont = 0;

        //Validações

        //Não permitir espaços na Senha Nova (ASCII)
        for (int i = 0; i < txtSenhaNova.Text.Length; i++)
        {
            letra = senha[i];

            if (Convert.ToInt32(letra).Equals(32)) //espaço
            {
                txtSenhaNova.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','NÃO digite espaços no campo (Senha nova).','error');</script>", false);
            }
        }

        //Contar caracteres especiais na Senha (ASCII)
        for (int i = 0; i < txtSenhaNova.Text.Length; i++)
        {
            letra = senha[i];

            if ((Convert.ToInt32(letra) >= 33) && (Convert.ToInt32(letra) <= 47)
                || ((Convert.ToInt32(letra) >= 58) && (Convert.ToInt32(letra) <= 64))
                || ((Convert.ToInt32(letra) >= 91) && (Convert.ToInt32(letra) <= 96))) //caracteres especiais
            {
                cont++;
            }
        }

        if ((txtSenhaAntiga.Text == "") || (txtSenhaNova.Text == "") || (txtRepitaSenha.Text == "")
            || (txtSenhaAntiga.Text.Length < 8) || (txtSenhaNova.Text.Length < 8)
            || (txtRepitaSenha.Text.Length < 8)
            || (txtSenhaNova.Text.Where(c => char.IsLetter(c)).Count() <= 0)
            || (txtSenhaNova.Text.Where(c => char.IsNumber(c)).Count() <= 0)
            || (cont <= 0)
            || (senha != senha2) || (senha2 != senha))
        {
            if (txtSenhaAntiga.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Campos não preenchidos','O campo (Senha antiga) não foi preenchido!','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }

            if (txtSenhaNova.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Campos não preenchidos','O campo (Senha nova) não foi preenchido!','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }

            if (txtRepitaSenha.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Campos não preenchidos','O campo (Repita a senha) não foi preenchido!','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }

            if (txtSenhaNova.Text.Length < 8)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','O campo (Senha nova) é menor que 8 caracteres.','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }

            if ((senha.Where(c => char.IsLetter(c)).Count() <= 0) || (senha.Where(c => char.IsNumber(c)).Count() <= 0) || (senha.Length < 8))
            {
                txtSenhaNova.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Campo (Senha nova) inválido! Digite números e letras, sendo maior que oito caracteres.','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }

            if (cont <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Campo (Senha nova) inválido! Digite no mínimo um caracter especial.','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }

            if (senha != senha2)
            {
                txtSenhaNova.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','As senhas novas digitadas não são iguais.','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }

            if (senha2 != senha)
            {
                txtRepitaSenha.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','As senhas novas digitadas não são iguais.','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }
        }

        else
        {
            codigo = Convert.ToInt32(Session["ID"]);

            Usuario usuSenha = new Usuario();

            usuSenha = db.ValidarSenhaPorEmail(txtEmailPerfil.Text, codigo);
            string senhaBanco = usuSenha.Usu_senha;

            //Criptografa para comparar de maneira correta com o DB
            senha = Convert.ToString(sha512.SHA512(txtSenhaAntiga.Text));

            usuSenha = db.ValidarSenhaAtual(senha, codigo); //recebe o objeto
            string novaSenhaRepetida = txtRepitaSenha.Text;

            if (!ValidarSenhaAtual(usuSenha)) //senha certa
            {
                novaSenha = Convert.ToString(sha512.SHA512(txtSenhaNova.Text)); //Criptografa para inserir                

                usuSenha.Usu_codigo = codigo;
                usuSenha.Usu_senha = novaSenha;

                switch (db.UpdateSenhaPerfil(usuSenha))
                {
                    case true:

                        //ENVIAR EMAIL
                        usuSenha.Usu_email = txtEmailPerfil.Text;

                        mensagem = "Sua senha foi alterada com sucesso! Nova senha: " + txtSenhaNova.Text;
                        msg = Email.EnviarEmail(usuSenha.Usu_email, "Alteração de senha - Monitore", mensagem);


                        //MENSAGEM DE CONFIRMAÇÃO
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','Sua senha foi alterada com sucesso e um e-mail foi enviado para você com a nova senha.','success');</script>", false);
                        txtSenhaAntiga.Text = "";
                        txtSenhaNova.Text = "";
                        txtRepitaSenha.Text = "";

                        break;

                    case false:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','Sua senha não foi alterada!','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
                        break;
                }
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Senha atual está errada!','error'); document.getElementById('perfil').style.display = 'block'; document.getElementById('alterarSenha').style.display = 'block';</script>", false);
            }
        }
    }

    private bool ValidarSenhaAtual(Usuario usuSenha)
    {
        bool retorno = true;
        if (usuSenha != null)
        {
            retorno = false;
        }
        return retorno;
    }

    protected void btnEnviarContato_Click(object sender, EventArgs e)
    {
        //Validações

        if ((txtContato.Text == "") || (txtMensagem.Text == "")
            || (txtContato.Text.Length < 10) || (txtMensagem.Text.Length < 10))
        {
            if (txtContato.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Digite algo no assunto.','error'); document.getElementById('contatoUser').style.display = 'block';</script>", false);
            }

            if (txtContato.Text.Length < 10)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Digite mais de 10 caracteres no assunto.','error'); document.getElementById('contatoUser').style.display = 'block';</script>", false);
            }

            if (txtMensagem.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Digite algo na mensagem.','error'); document.getElementById('contatoUser').style.display = 'block';</script>", false);
            }

            if (txtMensagem.Text.Length < 10)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Digite mais de 10 caracteres na mensagem.','error'); document.getElementById('contatoUser').style.display = 'block';</script>", false);
            }
        }

        else
        {
            con.Con_titulo = txtContato.Text;
            con.Con_conteudo = txtMensagem.Text;

            string dataCriacao = Convert.ToString(DateTime.Now);

            string dia = dataCriacao.Substring(0, 2);
            string mes = dataCriacao.Substring(3, 2);
            string ano = dataCriacao.Substring(6, 4);

            con.Con_data = ano + "-" + mes + "-" + dia; //intervalo da data só

            //FK USUARIO
            int codUsu = Convert.ToInt32(Session["ID"]);
            usuario.Usu_codigo = Convert.ToInt32(codUsu);
            con.Usu_codigo = usuario;

            switch (conDB.InsertContato(con))
            {
                case true:
                    txtContato.Text = "";
                    txtMensagem.Text = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','Sua mensagem foi enviada com sucesso! Responderemos em breve no seu e-mail.','success');</script>", false);
                    break;

                case false:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','Sua mensagem não foi enviada.','error'); document.getElementById('contatoUser').style.display = 'block';</script>", false);
                    break;
            }
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
