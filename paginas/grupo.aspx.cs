using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using Monitore.Classes;
using Monitore.Persistencias;
using System.IO;

public partial class paginas_grupo : System.Web.UI.Page
{
    Grupo gru = new Grupo();
    GrupoDB gruDB = new GrupoDB();
    Tarefa tar = new Tarefa();
    TarefaDB tarDB = new TarefaDB();
    Temas tem = new Temas();
    TemasDB temDB = new TemasDB();
    MembrosTarefas memtar = new MembrosTarefas();
    MembrosTarefasDB memtarDB = new MembrosTarefasDB();
    Membros mem = new Membros();
    MembrosDB memDB = new MembrosDB();
    UsuarioDB usuDB = new UsuarioDB();
    TipoUsuario tipUser = new TipoUsuario();
    Usuario usu = new Usuario();
    Comentario com = new Comentario();
    ComentarioDB comDB = new ComentarioDB();
    PlanoDB planDB = new PlanoDB();
    Plano pla = new Plano();
    Arquivo arq = new Arquivo();
    ArquivoDB arqDB = new ArquivoDB();

    private string data, tipo, nomeLider, prazo, seLider, nomeMiniatura, chave;
    private int dia2 = 0, mes2 = 0, ano2 = 0, cont = 0, codTarefa, cont2 = 0, codMembro, codigoUsu,
        tamanhoMin = 0, tamanhoMax = 0, codGrupo;

    private int[] vetorCodMembrosTarefa = new int[100];
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

            carregarDadosGrupo();
            CarregarDllTemas();

            chave = Request["grupo"]; //Pegar código do grupo pela URL
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            carregaTarefasAFazer(codGrupo);
            carregaTarefasFazendo(codGrupo);
            carregaTarefasFeito(codGrupo);
            verificaSeLider();
        }
    }

    private void verificaSeLider()
    {
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        int codigoUsu = Convert.ToInt32(Session["ID"]);

        gru = gruDB.SelectAlunoLider(codigoUsu, codGrupo);

        if (!LiderEncontrado(gru)) //se for o líder
        {
            btnCriarTarefa.Enabled = true; //habilitado
            btnAdicionarMembro.Enabled = true;
            btnRemoverMembro.Enabled = true;
            btnEncerrarGrupo.Enabled = true;
            btnSairDoGrupo.Enabled = false;
            verificaTutorial(); //Tutorial só aparece para o líder
            btnAlterarTarefa.Enabled = true;
            seLider = "sim";
        }
        else //se não for o líder
        {
            btnCriarTarefa.Enabled = false; //desabilitado
            btnAdicionarMembro.Enabled = false;
            btnRemoverMembro.Enabled = false;
            btnEncerrarGrupo.Enabled = false;
            btnSairDoGrupo.Enabled = true;
            btnAlterarTarefa.Enabled = false;
            seLider = "não";
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

    private void carregaTarefasAFazer(int codGrupo)
    {
        DataSet ds = TarefaDB.SelectTarefasAFazer(codGrupo);
        int qtde = ds.Tables[0].Rows.Count;
        rptTarefasAFazer.DataSource = ds.Tables[0].DefaultView;
        rptTarefasAFazer.DataBind();

        if (qtde == 0)
        {
            lblTarefasAFazer.Text = @"<br/><div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 22px;'>
                Não existem tarefas a fazer.</div><br/>";
        }

        else
        {
            lblTarefasAFazer.Text = "";
        }
    }

    private void carregaTarefasFazendo(int codGrupo)
    {
        DataSet ds = TarefaDB.SelectTarefasFazendo(codGrupo);
        int qtde = ds.Tables[0].Rows.Count;
        rptTarefasFazendo.DataSource = ds.Tables[0].DefaultView;

        rptTarefasFazendo.DataBind();
        if (qtde == 0)
        {
            lblTarefasFazendo.Text = @"<br/><div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 22px;'>
                Não existem tarefas sendo feitas.</div><br/><br/>";
        }

        else
        {
            lblTarefasFazendo.Text = "";
        }
    }

    private void carregaTarefasFeito(int codGrupo)
    {
        DataSet ds = TarefaDB.SelectTarefasFeito(codGrupo);
        int qtde = ds.Tables[0].Rows.Count;
        rptTarefasFeito.DataSource = ds.Tables[0].DefaultView;

        rptTarefasFeito.DataBind();
        if (qtde == 0)
        {
            lblTarefasFeito.Text = @"<br/><div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 22px;'>
                Não existem tarefas feitas.</div> <br/><br/>";
        }

        else
        {
            lblTarefasFeito.Text = "";
        }
    }

    private void verificaTutorial()
    {
        if ((lblTarefasAFazer.Text != "")
                && (lblTarefasFazendo.Text != "")
                && (lblTarefasFeito.Text != "")
               )
        {
            btnTutorial.Visible = true;
            ltrGif.Text = @"<img src='../img/gifs/setaGif.gif' width='50px' height='50px;'/>";
        }
    }

    public void CarregarDllTemas()
    {
        //Carregar um DropDownList com o Banco de Dados 
        DataSet ds = TemasDB.SelectAll();

        ddlTema.DataSource = ds;
        ddlTema.DataTextField = "tem_nome";

        // Nome da coluna do Banco de dados 
        ddlTema.DataValueField = "tem_codigo";

        // ID da coluna do Banco 
        ddlTema.DataBind();
        ddlTema.Items.Insert(0, "Selecione um tema");

        ddlTema.SelectedIndex = 0;
    }

    public void CarregarDllTemasAlterar(int codTema)
    {
        DataSet ds = TemasDB.SelectAll();

        ddlTemaTarefaVer.DataSource = ds;
        ddlTemaTarefaVer.DataTextField = "tem_nome";
        ddlTemaTarefaVer.DataValueField = "tem_codigo";
        ddlTemaTarefaVer.DataBind();

        tem = temDB.SelectTemaTarefa(codTema);

        ddlTemaTarefaVer.Items.Insert(0, "Selecione um tema");
        ddlTemaTarefaVer.SelectedValue = Convert.ToString(tem.Tem_codigo);
    }

    protected void btnProgresso_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"];

        Response.Redirect("progresso.aspx?grupo=" + chave);
    }

    protected void btnCriar_Click(object sender, EventArgs e)
    {
        int diaAtual = 0, mesAtual = 0, anoAtual = 0;
        string dataAtual = DateTime.Now.ToString();

        diaAtual = Convert.ToInt32(dataAtual.Substring(0, 2));
        mesAtual = Convert.ToInt32(dataAtual.Substring(3, 2));
        anoAtual = Convert.ToInt32(dataAtual.Substring(6, 4));

        cont = 0;
        //VALIDAÇÕES 

        prazo = txtPrazo.Text;

        if ((prazo != "") && (prazo != "0000-00-00") && (prazo != "00-00-0000") && (prazo != "dd/mm/aaaa"))
        {
            data = txtPrazo.Text;
            dia2 = Convert.ToInt32(data.Substring(8, 2));
            mes2 = Convert.ToInt32(data.Substring(5, 2));
            ano2 = Convert.ToInt32(data.Substring(0, 4));
        }

        if ((txtNomeTarefa.Text == "") || (txtPrazo.Text == "") || (dia2 <= 0) || (dia2 > 31)
            || (mes2 <= 0) || (mes2 > 12) || (ano2 < anoAtual) || (ano2 > (anoAtual + 100))
            || (ddlTema.SelectedIndex == 0) || (lblMembrosTarefa.Text.Length <= 22)
            || (dia2 < diaAtual) && (mes2 < mesAtual) && (ano2 < anoAtual)
            || ((dia2 < diaAtual) && (mes2 < mesAtual))
            || (dia2 < diaAtual))
        {
            if (txtNomeTarefa.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Digite um nome para a tarefa.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
                txtNomeTarefa.Focus();
            }

            if (prazo == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Digite um prazo para a tarefa.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
                txtPrazo.Focus();
            }

            if ((dia2 <= 0) || (dia2 > 31) || (mes2 <= 0) || (mes2 > 12) || (ano2 < anoAtual) || (ano2 > (anoAtual + 100)))
            {
                if ((dia2 <= 0) || (dia2 > 31))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Dia (" + dia2 + ") não existe.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
                }

                if ((mes2 <= 0) || (mes2 > 12))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Mês (" + mes2 + ") não existe.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
                }

                if ((ano2 < anoAtual) || (ano2 > (anoAtual + 100)))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Ano (" + ano2 + ") não permitido.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
                }
                txtPrazo.Focus();
            }

            //Não deixar cadastrar prazos anteriores a data atual

            if ((dia2 < diaAtual) && (mes2 < mesAtual) && (ano2 <= anoAtual)
                || ((dia2 < diaAtual) && (mes2 <= mesAtual)))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Essa data é antiga.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
            }

            if (ddlTema.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Selecione um tema para a tarefa.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
                ddlTema.Focus();
            }

            if (lblMembrosTarefa.Text.Length <= 22)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente Novamente!','Adicione no mínimo um membro.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
            }
        }

        if ((txtNomeTarefa.Text != "") || (txtPrazo.Text != "dd/mm/aaaa") || (dia2 > 0) || (dia2 < 31)
            || (mes2 > 0) || (mes2 < 12) || (ano2 > 1920) || (ano2 < 2018)
            || (ddlTema.SelectedIndex != 0) || (lblMembrosTarefa.Text.Length > 21))
        {
            tar.Tar_nome = txtNomeTarefa.Text;

            string dataCriacao = Convert.ToString(DateTime.Now);
            string dia = dataCriacao.Substring(0, 2);
            string mes = dataCriacao.Substring(3, 2);
            string ano = dataCriacao.Substring(6, 4);

            tar.Tar_data_criacao = ano + "-" + mes + "-" + dia; //intervalo da data só
            tar.Tar_prazo = txtPrazo.Text;

            string nomeTema = Convert.ToString(ddlTema.SelectedItem);

            tem = temDB.SelectCodigoTema(nomeTema);

            //FK TEMA
            tem = temDB.SelectCodigoTema(nomeTema);
            tar.Tem_codigo = tem;

            //FK GRUPO
            chave = Request["grupo"]; //Pegar código do grupo pela URL
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            gru.Gru_codigo = codGrupo;
            tar.Gru_codigo = gru;

            tar.Tar_qtde_membros = 0;

            if (txtDescricao.Text == "")
            {
                tar.Tar_descricao = "Sem descrição";
            }

            if (txtDescricao.Text != "")
            {
                tar.Tar_descricao = txtDescricao.Text;
            }

            tar.Tar_realizada = "Não";
            tar.Tar_status = "A fazer";

            switch (TarefaDB.Insert(tar))
            {
                case 0:

                    //Inserir os membros
                    for (int i = 0; i < ltbCodigos.Items.Count; i++) //roda o listBox invisível
                    {
                        string codigoMem = ltbCodigos.Items[i].ToString();
                        mem.Mem_codigo = Convert.ToInt32(codigoMem);
                        memtar.Mem_codigo = mem;

                        //FK GRUPO
                        chave = Request["grupo"]; //Pegar código do grupo pela URL
                        gruCodigo = gruDB.SelectCodigoGrupo(chave);
                        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

                        //FK TAREFA
                        tar = tarDB.SelectCodigoUltimaTarefa(codGrupo);
                        codTarefa = tar.Tar_codigo;

                        memtar.Tar_codigo = tar;

                        lblMembrosTarefa.Text = Convert.ToString(memtar.Mem_codigo);

                        switch (TarefaDB.InsertMembrosTarefa(memtar))
                        {
                            case 0:
                                break;

                            case -2:
                                break;
                        }
                    }

                    //Atualizar tipo tarefa

                    string tipoTarefa = "";

                    cont = ltbCodigos.Items.Count;

                    if (cont <= 1)
                    {
                        tipoTarefa = "Individual";
                    }

                    if (cont > 1)
                    {
                        tipoTarefa = "Colaborativa";
                    }

                    //FK GRUPO
                    chave = Request["grupo"]; //Pegar código do grupo pela URL
                    gruCodigo = gruDB.SelectCodigoGrupo(chave);
                    codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

                    //FK TAREFA
                    tar = tarDB.SelectCodigoUltimaTarefa(codGrupo);
                    codTarefa = tar.Tar_codigo;

                    tar.Tar_tipo = tipoTarefa;

                    tar.Tar_qtde_membros = cont;

                    carregaTarefasAFazer(codGrupo);
                    carregaTarefasFazendo(codGrupo);
                    carregaTarefasFeito(codGrupo);

                    switch (tarDB.UpdateTipoTarefa(tar, codGrupo))
                    {
                        case 0:
                            txtNomeTarefa.Text = "";
                            ddlTema.SelectedIndex = 0;
                            txtDescricao.Text = "";
                            lblMembrosTarefa.Text = "";
                            txtPrazo.Text = "";
                            ddlMembrosTarefa.SelectedIndex = 0;
                            ltbCodigos.Items.Clear();
                            btnCriar.Enabled = false;
                            verificaTutorial();
                            btnTutorial.Visible = false;
                            ltrGif.Visible = false;
                            carregarDadosGrupo();
                            break;

                        case -2:
                            break;
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','Tarefa foi criada com sucesso.','success'); document.getElementById('criarTarefa').style.display = 'none';</script>", false);
                    break;

                case -2:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','Tarefa não foi criada.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
                    break;
            }
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlMembrosTarefa.SelectedIndex == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Selecione um membro.','error'); document.getElementById('criarTarefa').style.display = 'block';</script>", false);
        }

        else
        {
            //FK MEMBRO
            string nomeMembro = Convert.ToString(ddlMembrosTarefa.SelectedItem);
            int codMembro = Convert.ToInt32(ddlMembrosTarefa.SelectedValue);

            ltbCodigos.Items.Insert(0, Convert.ToString(codMembro));

            btnCriar.Enabled = true;
            lblMembrosTarefa.Text += ddlMembrosTarefa.SelectedItem + "; ";

            ddlMembrosTarefa.Items[ddlMembrosTarefa.SelectedIndex].Enabled = false;
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('criarTarefa').style.display = 'block';</script>", false);
    }

    public void CarregarMembrosRemover()
    {
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        DataSet ds = MembrosDB.SelectAllMembrosGrupos(codGrupo);

        ddlMembrosRemover.DataSource = ds;
        ddlMembrosRemover.DataTextField = "usu_nome";

        ddlMembrosRemover.DataValueField = "mem_codigo";

        ddlMembrosRemover.DataBind();
        ddlMembrosRemover.Items.Insert(0, "Selecione");
    }

    private void carregarDadosGrupo()
    {
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        //NOME DO GRUPO

        gru = gruDB.SelectGrupoInfo(codGrupo);
        ltrTituloGrupo.Text = @"<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12 fundoTitulo' style='background-color: " + gru.Gru_cor + "; font-weight: bold; color: white;'>"
                            + gru.Gru_nome + "</div>";

        //NOMES PROFESSORES

        DataSet ds = GrupoDB.SelectProfessoresGrupo(codGrupo);
        int qtdeProfessores = ds.Tables[0].Rows.Count;

        if (qtdeProfessores > 0)
        {
            string div = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                div += "<br/>" + dr["usu_nome"];
            }

            ltrNomesProfessores.Text = div;
        }

        if (qtdeProfessores <= 0)
        {
            string div = "";
            div += @"<br/>Não há professores";
            ltrNomesProfessores.Text = div;
        }

        //NOMES ALUNOS

        Usuario usuLider = gruDB.SelectAlunoLiderCoroa(codGrupo);

        if (usuLider.Usu_nome != null)
        {
            nomeLider = usuLider.Usu_nome;
        }

        if (usuLider.Usu_nome == null)
        {
            nomeLider = "Líder não encontrado";
        }

        ds = GrupoDB.SelectAlunosGrupo(codGrupo);
        int qtdeAlunos = ds.Tables[0].Rows.Count;

        if (qtdeAlunos > 0)
        {
            string div = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["usu_nome"].Equals(nomeLider))
                {
                    div += "<br/>" + dr["usu_nome"] + "<img src='/img/icones/coroa.png' style='margin-bottom: 4px;' width='20' height='20' title='Aluno-líder' />";
                }

                else
                {
                    div += "<br/>" + dr["usu_nome"];
                }
            }

            ltrNomesAlunos.Text = div;
        }

        if (qtdeAlunos <= 0)
        {
            string div = "";
            div += @"<br/>Não há alunos";
            ltrNomesAlunos.Text = div;
        }

        //PROGRESSO DO GRUPO

        ds = GrupoDB.SelectPorcentagemGrupo(codGrupo);
        int qtdeProgresso = ds.Tables[0].Rows.Count;

        if (qtdeProgresso > 0)
        {
            string div = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                div += @"<div class='col-xs-12 col-sm-12 col-md-7 col-lg-8 barraProgresso' style='background: -webkit-linear-gradient(left, #01a518 " + dr["tarefas"] + @"%, #c7c3c3 " + dr["tarefas"] + @"%); background: -moz-linear-gradient(left, #01a518 " + dr["tarefas"] + @"%, #c7c3c3 " + dr["tarefas"] + @"%);'
                         title='" + dr["tarefas"] + @"% das tarefas concluídas'>
                            <a id='lblPorcentagemGrupo' style='font-size: 40px;'>" + dr["tarefas"] + @"%</a>
                        </div>";
            }

            ltrProgressoGrupo.Text = div;
        }

        if (qtdeProgresso <= 0)
        {
            string div = "";
            div += @"<div class='col-xs-12 col-sm-12 col-md-7 col-lg-8 barraProgresso' style='background: -webkit-linear-gradient(left, #01a518 45%, #c7c3c3 0%); background: -moz-linear-gradient(left, #01a518 45%, #c7c3c3 0%);'>
                            <a id='lblPorcentagemGrupo' style='font-size: 40px;'>Impossível quantificar progresso</a>
                        </div>";
            ltrProgressoGrupo.Text = div;
        }
    }
    protected void rptTarefasAFazer_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            int codTar = Convert.ToInt32(e.CommandArgument);

            tar = tarDB.SelectTarefaInfo(codTar);

            //INFORMAÇÕES DA TAREFA

            lblNomeTarefaVer.Text = tar.Tar_nome;
            lblDescricaoVer.Text = tar.Tar_descricao;

            int codTema = tar.Tem_codigo.Tem_codigo;
            tem = temDB.SelectTemaTarefa(codTema);
            txtTemaTarefaVer.Text = tem.Tem_nome;

            txtTipoTarefaVer.Text = tar.Tar_tipo;

            data = tar.Tar_prazo;

            string dia = data.Substring(0, 2);
            string mes = data.Substring(3, 2);
            string ano = data.Substring(6, 4);

            TxtDataTarefaVer.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);

            lblCodTar.Text = Convert.ToString(codTar);

            //MEMBROS NA LIST

            DataSet ds = MembrosTarefasDB.SelectMembrosTarefa(codTar);
            int qtdeMembros = ds.Tables[0].Rows.Count;

            if (qtdeMembros > 0)
            {
                string div = "";
                lsbMembros.Items.Clear(); //limpa e zero o list box

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    div = "";
                    div += dr["Nomes"];
                    lsbMembros.Items.Add(div.ToString()); //add novo item (nome membro)
                }
            }

            //COMENTÁRIOS DA TAREFA
            carregaComentariosTarefa(codTar);
            lblCodTarefa.Text = Convert.ToString(codTar);

            //ARQUIVOS
            carregaArquivos(codTar);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('visualizarTarefa').style.display = 'block';</script>", false);
        }

        if (e.CommandName == "Excluir")
        {
            int codTar = Convert.ToInt32(e.CommandArgument);
            int codigoUsu = Convert.ToInt32(Session["ID"]);

            //gru = gruDB.SelectAlunoLider(codigoUsu, Convert.ToInt32(codGrupo));

            if (seLider.Equals("sim")) //se for o líder
            {
                lblCodigoTarefa.Text = Convert.ToString(codTar);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('excluirTarefa').style.display = 'block';</script>", false);
            }
            else //se não for o líder
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Você não é o Aluno-líder!','Apenas o aluno-líder pode excluir tarefas.','error');</script>", false);
            }
        }
    }
    protected void rptTarefasFazendo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            int codTar = Convert.ToInt32(e.CommandArgument);

            tar = tarDB.SelectTarefaInfo(codTar);

            //INFORMAÇÕES DA TAREFA

            lblNomeTarefaVer.Text = tar.Tar_nome;
            lblDescricaoVer.Text = tar.Tar_descricao;

            int codTema = tar.Tem_codigo.Tem_codigo;
            tem = temDB.SelectTemaTarefa(codTema);
            txtTemaTarefaVer.Text = tem.Tem_nome;

            txtTipoTarefaVer.Text = tar.Tar_tipo;

            data = tar.Tar_prazo;

            string dia = data.Substring(0, 2);
            string mes = data.Substring(3, 2);
            string ano = data.Substring(6, 4);

            TxtDataTarefaVer.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);

            lblCodTar.Text = Convert.ToString(codTar);

            //MEMBROS NA LIST

            DataSet ds = MembrosTarefasDB.SelectMembrosTarefa(codTar);
            int qtdeMembros = ds.Tables[0].Rows.Count;

            if (qtdeMembros > 0)
            {
                string div = "";
                lsbMembros.Items.Clear(); //limpa e zero o list box

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    div = "";
                    div += dr["Nomes"];
                    lsbMembros.Items.Add(div.ToString()); //add novo item (nome membro)
                }
            }

            //COMENTÁRIOS DA TAREFA
            carregaComentariosTarefa(codTar);
            lblCodTarefa.Text = Convert.ToString(codTar);

            //ARQUIVOS
            carregaArquivos(codTar);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('visualizarTarefa').style.display = 'block';</script>", false);
        }

        if (e.CommandName == "Excluir")
        {
            int codTar = Convert.ToInt32(e.CommandArgument);
            int codigoUsu = Convert.ToInt32(Session["ID"]);

            //gru = gruDB.SelectAlunoLider(codigoUsu, Convert.ToInt32(codGrupo));

            if (seLider.Equals("sim")) //se for o líder
            {
                lblCodigoTarefa.Text = Convert.ToString(codTar);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('excluirTarefa').style.display = 'block';</script>", false);
            }
            else //se não for o líder
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Você" + codTar + codigoUsu + " não é o Aluno-líder!','Apenas o aluno-líder pode excluir tarefas.','error');</script>", false);
            }
        }
    }
    protected void rptTarefasFeito_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            int codTar = Convert.ToInt32(e.CommandArgument);

            tar = tarDB.SelectTarefaInfo(codTar);

            //INFORMAÇÕES DA TAREFA

            lblNomeTarefaVer.Text = tar.Tar_nome;
            lblDescricaoVer.Text = tar.Tar_descricao;

            int codTema = tar.Tem_codigo.Tem_codigo;
            tem = temDB.SelectTemaTarefa(codTema);
            txtTemaTarefaVer.Text = tem.Tem_nome;

            txtTipoTarefaVer.Text = tar.Tar_tipo;

            data = tar.Tar_prazo;

            string dia = data.Substring(0, 2);
            string mes = data.Substring(3, 2);
            string ano = data.Substring(6, 4);

            TxtDataTarefaVer.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);

            lblCodTar.Text = Convert.ToString(codTar);

            //MEMBROS NA LIST

            DataSet ds = MembrosTarefasDB.SelectMembrosTarefa(codTar);
            int qtdeMembros = ds.Tables[0].Rows.Count;

            if (qtdeMembros > 0)
            {
                string div = "";
                lsbMembros.Items.Clear(); //limpa e zero o list box

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    div = "";
                    div += dr["Nomes"];
                    lsbMembros.Items.Add(div.ToString()); //add novo item (nome membro)
                }
            }

            //COMENTÁRIOS DA TAREFA
            carregaComentariosTarefa(codTar);
            lblCodTarefa.Text = Convert.ToString(codTar);

            //ARQUIVOS
            carregaArquivos(codTar);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('visualizarTarefa').style.display = 'block';</script>", false);
        }

        if (e.CommandName == "Excluir")
        {
            int codTar = Convert.ToInt32(e.CommandArgument);
            int codigoUsu = Convert.ToInt32(Session["ID"]);

            //gru = gruDB.SelectAlunoLider(codigoUsu, Convert.ToInt32(codGrupo));

            if (seLider.Equals("sim")) //se for o líder
            {
                lblCodigoTarefa.Text = Convert.ToString(codTar);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('excluirTarefa').style.display = 'block';</script>", false);
            }

            else //se não for o líder
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Você não é o Aluno-líder!','Apenas o aluno-líder pode excluir tarefas.','error');</script>", false);
            }
        }
    }
    protected void btnAdicionarMembro_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('adicionarMembro').style.display = 'block';</script>", false);
    }
    protected void btnRemoverMembro_Click(object sender, EventArgs e)
    {
        CarregarMembrosRemover();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('removerMembro').style.display = 'block';</script>", false);
    }
    protected void btnCriarTarefa_Click(object sender, EventArgs e)
    {
        //MEMBROS CRIAR TAREFA
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        DataSet membrosDS = MembrosDB.SelectAllMembrosGrupos(codGrupo);

        ddlMembrosTarefa.DataSource = membrosDS;
        ddlMembrosTarefa.DataTextField = "usu_nome";
        ddlMembrosTarefa.DataValueField = "mem_codigo";
        ddlMembrosTarefa.DataBind();
        ddlMembrosTarefa.Items.Insert(0, "Selecione");


        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('criarTarefa').style.display = 'block';</script>", false);
    }
    protected void btnEncerrarGrupo_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('encerrarGrupo').style.display = 'block';</script>", false);
    }

    protected void btnSim_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        switch (gruDB.UpdateEncerrarGrupo("Inativo", codGrupo))
        {
            case true:

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Seu grupo foi encerrado com sucesso!', { buttons: { catch: { text: 'OK', value: 'catch',},},}).then((value) => { switch (value) {case 'catch': window.location.href = 'index-Logado.aspx'; break; default:window.location.href = 'index-Logado.aspx'; }});</script>", false);
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro ao encerrar!','Grupo não foi encerrado.','error');</script>", false);
                break;
        }
    }
    protected void btnSimExcluir_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        int codTar = Convert.ToInt32(lblCodigoTarefa.Text);

        switch (memtarDB.DelectMembrosTarefa(codTar)) //Excluir tarefa
        {
            case true:
                switch (tarDB.DelectTarefa(codTar, codGrupo))
                {
                    case true:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','A tarefa foi excluída com sucesso.','success');</script>", false);
                        carregaTarefasAFazer(codGrupo);
                        carregaTarefasFazendo(codGrupo);
                        carregaTarefasFeito(codGrupo);
                        carregarDadosGrupo();
                        break;

                    case false:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','A tarefa não foi excluída.','error');</script>", false);
                        break;
                }

                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','A tarefa não foi excluída." + codTar + codGrupo + Convert.ToString(TarefaDB.erro) + "','error');</script>", false);
                break;
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList d = (DropDownList)sender;
        Label lbl = (Label)d.Parent.FindControl("lblCodTarefa");

        string status = "";
        string realizada = "Não";
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        if (d.SelectedIndex == 0)
        {
            status = "A fazer";
        }

        if (d.SelectedIndex == 1)
        {
            status = "Fazendo";
        }

        if (d.SelectedIndex == 2)
        {
            status = "Feito";
            realizada = "Sim";
        }

        int codTar = Convert.ToInt32(d.Attributes["Rel"].ToString());

        switch (tarDB.UpdateStatusTarefa(status, codTar))
        {
            case true:

                switch (tarDB.UpdateTarefaRealizada(realizada, codTar))
                {
                    case true:
                        carregaTarefasAFazer(codGrupo);
                        carregaTarefasFazendo(codGrupo);
                        carregaTarefasFeito(codGrupo);
                        carregarDadosGrupo();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','O status da tarefa foi alterado.','success');</script>", false);
                        break;

                    case false:
                        break;
                }
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','O status da tarefa não foi alterado.','error');</script>", false);
                break;
        }
    }


    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Digite um username.','error');document.getElementById('adicionarMembro').style.display = 'block'</script>", false);
        }

        else
        {
            chave = Request["grupo"]; //Pegar código do grupo pela URL
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            Usuario usuUsername = new Usuario();
            usuUsername = usuDB.ValidarUsername(txtUsername.Text); //recebe o objeto

            if (!ValidarUsername(usuUsername)) //caso seja true
            {
                mem = memDB.ValidaUsernameComMembros(txtUsername.Text, codGrupo);

                if (usuUsername.Tip_cod.Tip_cod == 1) //se for um user ADM
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Ação negada!','Você não tem autorização para adicionar um administrador.','error');document.getElementById('adicionarMembro').style.display = 'block'</script></script>", false);
                }

                else if (ValidarUsuarioSeExiste(mem)) //se usuário já é membro
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Usuário já é membro!','" + txtUsername.Text + " já participa do grupo.','error');document.getElementById('adicionarMembro').style.display = 'block'</script></script>", false);
                }

                else //se ainda não é membro
                {
                    tipUser.Tip_cod = usuUsername.Tip_cod.Tip_cod;

                    if (tipUser.Tip_cod == 2)
                    {
                        tipo = "Aluno-integrante";
                    }

                    if (tipUser.Tip_cod == 3)
                    {
                        tipo = "Professor";
                    }

                    switch (gruDB.InsertMembroGrupo(tipo, codGrupo, usuUsername))
                    {
                        case true:
                            carregarDadosGrupo();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','" + txtUsername.Text + " foi adicionado(a) ao grupo com sucesso.','success');</script>", false);
                            break;

                        case false:
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','" + txtUsername.Text + " não foi adicionado(a) ao grupo.','error');document.getElementById('adicionarMembro').style.display = 'block'</script></script>", false);
                            break;
                    }
                }
            }

            else
            {
                txtUsername.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Username não encontrado','error');document.getElementById('adicionarMembro').style.display = 'block'</script></script>", false);
            }

        }

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

    private bool ValidarUsuarioSeExiste(Membros mem)
    {
        bool retorno = false;
        if (mem != null)
        {
            retorno = true;
        }
        return retorno;
    }

    protected void btnRemover_Click(object sender, EventArgs e)
    {
        codMembro = 0;
        if (ddlMembrosRemover.SelectedIndex == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Tente novamente!','Selecione um username','error');document.getElementById('removerMembro').style.display = 'block'</script>", false);
        }

        else
        {
            codMembro = Convert.ToInt32(ddlMembrosRemover.SelectedValue);
            codigoUsu = Convert.ToInt32(Session["ID"]);
            chave = Request["grupo"]; //Pegar código do grupo pela URL
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            mem = memDB.SelectTipoMembro(codMembro);

            if (mem.Mem_tipo.Equals("Aluno-líder") || (ddlMembrosRemover.Items.Count == 2)) //se o membro selecionado for o líder
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Você é o Aluno-líder!','Não é possível remover o aluno-líder. Que tal encerrar o grupo?','error');document.getElementById('removerMembro').style.display = 'block'</script></script>", false);
            }

            else //se não for o líder
            {
                //1 - excluir esse membro de todas as tarefas que ele esteja
                switch (tarDB.DelectMembrosDeSuasTarefas(codGrupo, codMembro))
                {
                    case true:

                        //2 - excluir membro do grupo
                        switch (gruDB.DelectMembro(codMembro))
                        {
                            case true:
                                carregarDadosGrupo();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Bom trabalho!','" + ddlMembrosRemover.SelectedItem + " foi removido(a) ao grupo com sucesso.','success');</script>", false);
                                break;

                            case false:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','" + ddlMembrosRemover.SelectedItem + " não foi removido(a) ao grupo.','error');document.getElementById('removerMembro').style.display = 'block'</script></script>", false);
                                break;
                        }
                        break;

                    case false:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','" + ddlMembrosRemover.SelectedItem + " não foi removido(a) ao grupo.','error');document.getElementById('removerMembro').style.display = 'block'</script></script>", false);
                        break;
                }
            }
        }
    }
    protected void btnSairDoGrupo_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('confirmacaoSair').style.display = 'block'</script></script>", false);
    }
    protected void btnSimSair_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        mem = memDB.SelectCodigoMembroPorCodigo(codGrupo, (Convert.ToInt32(Session["ID"])));

        switch (gruDB.DelectMembro(mem.Mem_codigo))
        {
            case true:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Você não é mais membro desse grupo!', { buttons: { catch: { text: 'OK', value: 'catch',},},}).then((value) => { switch (value) {case 'catch': window.location.href = 'index-Logado.aspx'; break; default:window.location.href = 'index-Logado.aspx'; }});</script>", false);
                break;

            case false:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>swal('Erro não identificado!','Erro ao sair do grupo.','error');</script>", false);
                break;
        }
    }
    protected void btnTutorial_Click(object sender, EventArgs e)
    {
        ltrGif.Text = "";
        btnTutorial.Visible = false;

        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"
                <script>
                    document.getElementById('tutorial6').style.display = 'block';
                    document.getElementById('msgTutorial6').style.display = 'block';
                    jQuery('body,html').animate({
                        scrollTop: 0
                    }, 800);
                </script>", false);
    }

    private void carregaComentariosTarefa(int codTar)
    {
        DataSet comentariosDS = ComentarioDB.SelectComentariosTarefa(codTar);
        int qtde = comentariosDS.Tables[0].Rows.Count;
        rptComentariosTarefa.DataSource = comentariosDS.Tables[0].DefaultView;
        rptComentariosTarefa.DataBind();

        if (qtde == 0)
        {
            ltrLegenda.Text = @"<br/><div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 16px;'>
                Não existem comentários nessa tarefa. <p><b>Que tal adicionar um?</b></p></div><br/>";
        }

        else
        {
            ltrLegenda.Text = "";
        }
    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if (txtComentario.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Tente novamente!','Digite um comentário.','error');
                document.getElementById('visualizarTarefa').style.display = 'block';
                document.getElementById('abaComentarios').click();
                $('#abaComentarios').click();
                </script>", false);
        }

        else
        {
            com.Com_conteudo = txtComentario.Text;

            string dataComentario = Convert.ToString(DateTime.Now);

            string dia = dataComentario.Substring(0, 2);
            string mes = dataComentario.Substring(3, 2);
            string ano = dataComentario.Substring(6, 4);

            string hora = dataComentario.Substring(11, 2);
            string minutos = dataComentario.Substring(14, 2);
            string segundos = dataComentario.Substring(17, 2);

            com.Com_data = ano + "-" + mes + "-" + dia; //intervalo da data só
            com.Com_horario = hora + ":" + minutos + ":" + segundos;

            int codigo = Convert.ToInt32(Session["ID"]);
            usu.Usu_codigo = codigo;
            com.Usu_codigo = usu;

            int codigoTarefa = Convert.ToInt32(lblCodTarefa.Text);
            tar.Tar_codigo = codigoTarefa;
            com.Tar_codigo = tar;

            Plano plaPlano = new Plano();

            plaPlano = planDB.SelectTipoPlano(Convert.ToInt32(Session["ID"]));
            if (plaPlano.Pla_tipo.Equals("Premium"))
            {
                com.Com_cor = "#1D1D1D";
                com.Com_borda = "#F1AF08";
                com.Com_cor_texto = "#FFFFFF";
            }

            if (plaPlano.Pla_tipo.Equals("Basic"))
            {
                com.Com_cor = "#FFFFFF";
                com.Com_borda = "#FFFFFF";
                com.Com_cor_texto = "#000000";
            }

            switch (comDB.Insert(com))
            {
                case 0:
                    txtComentario.Text = "";
                    carregaComentariosTarefa(Convert.ToInt32(lblCodTarefa.Text));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Bom trabalho!','Seu comentário foi enviado.','success');
                document.getElementById('visualizarTarefa').style.display = 'block';
                document.getElementById('abaComentarios').click();
                </script>", false);
                    break;

                case -2:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Erro não identificado!','Seu comentário não foi enviado.','error');
                document.getElementById('visualizarTarefa').style.display = 'block';
                </script>", false);
                    break;
            }


        }
    }
    protected void rptComentariosTarefa_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Excluir")
        {
            int codigo = Convert.ToInt32(Session["ID"]);
            int codCom = Convert.ToInt32(e.CommandArgument);

            chave = Request["grupo"]; //Pegar código do grupo pela URL
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            gru = gruDB.SelectAlunoLider(codigo, codGrupo);

            //Verifica se logado é o líder
            if (LiderEncontrado(gru)) //se for o líder
            {
                switch (comDB.DelectComentario(codCom))
                {
                    case true:
                        carregaComentariosTarefa(Convert.ToInt32(lblCodTarefa.Text));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Bom trabalho!','O comentário foi excluído com sucesso.','success');
                document.getElementById('visualizarTarefa').style.display = 'block';
                </script>", false);
                        break;

                    case false:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Erro não identificado!','O comentário não foi excluído.','error');
                document.getElementById('visualizarTarefa').style.display = 'block';
                </script>", false);
                        break;
                }
            }

            else
            {
                //Verifica se o usuário é o user que comentou
                com = comDB.ValidaSeLogadoComentou(codCom, codigo);

                if (ValidarUsuarioDonoComentario(com))//usuário é o dono do comentário
                {
                    switch (comDB.DelectComentario(codCom))
                    {
                        case true:
                            carregaComentariosTarefa(Convert.ToInt32(lblCodTarefa.Text));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Bom trabalho!','Seu comentário foi excluído com sucesso.','success');
                document.getElementById('visualizarTarefa').style.display = 'block';
                </script>", false);
                            break;

                        case false:
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Erro não identificado!','Seu comentário não foi excluído.','error');
                document.getElementById('visualizarTarefa').style.display = 'block';
                </script>", false);
                            break;
                    }
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                swal('Você não tem permissão!','Você não pode excluir um comentário que não é seu.','error');
                document.getElementById('visualizarTarefa').style.display = 'block';
                </script>", false);
                }
            }
        }
    }

    private bool ValidarUsuarioDonoComentario(Comentario comentario)
    {
        bool retorno = false;
        if (comentario != null)
        {
            retorno = true;
        }
        return retorno;
    }
    protected void rptTarefasFeito_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlStatus");
        ddl.SelectedIndexChanged += ddlStatus_SelectedIndexChanged;
    }
    protected void rptTarefasFazendo_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlStatus");
        ddl.SelectedIndexChanged += ddlStatus_SelectedIndexChanged;
    }
    protected void rptTarefasAFazer_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlStatus");
        ddl.SelectedIndexChanged += ddlStatus_SelectedIndexChanged;
    }

    protected void lkbChatGrupo_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"];

        Response.Redirect("chatGrupo.aspx?grupo=" + chave);
    }
    protected void lkbConfiguracaoGrupo_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"];

        Response.Redirect("configuracaoGrupo.aspx?grupo=" + chave);
    }
    protected void btnAnexarArquivo_Click(object sender, EventArgs e)
    {
        int codigoTarefa = Convert.ToInt32(lblCodTarefa.Text);
        chave = Request["grupo"];
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        string dataNow = "/" + DateTime.Now.ToString("yyyMMddHHmmssfff");


        //Diretório que a imagem será salva no projeto
        string dir = Request.PhysicalApplicationPath + "Uploads/arquivos/tarefa" + codigoTarefa
            + "grupo" + codGrupo + dataNow + "\\";

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        if (flpArquivo.HasFile)
        {
            string ext = Path.GetExtension(flpArquivo.FileName).ToLower();
            double tam = flpArquivo.PostedFile.ContentLength / 1024;

            //VERIFICA TIPO CONTA
            Plano plaPlano = new Plano();
            plaPlano = planDB.SelectTipoPlano(Convert.ToInt32(Session["ID"]));

            if (plaPlano.Pla_tipo.Equals("Premium"))
            {
                tamanhoMin = 0;
                tamanhoMax = 50000; //50MB
            }

            if (plaPlano.Pla_tipo.Equals("Basic"))
            {
                tamanhoMin = 0;
                tamanhoMax = 5000; //5MB
            }

            if ((tam >= tamanhoMin) && (tam <= tamanhoMax))
            {
                if ((ext == ".jpg") || (ext == ".png") || (ext == ".jpeg") || (ext == ".gif")
                    || (ext == ".ico"))
                {
                    nomeMiniatura = "../Uploads/arquivos/tarefa" + codigoTarefa
                        + "grupo" + codGrupo + "/" + dataNow
                        + "\\" + Path.GetFileName(flpArquivo.FileName);
                }

                else if ((ext == ".doc") || (ext == ".docx") || (ext == ".xls") || (ext == ".xlsx")
                    || (ext == ".pdf") || (ext == ".asta") || (ext == ".ppt") || (ext == ".pptx")
                    || (ext == ".pps") || (ext == ".ppsx") || (ext == ".wmv") || (ext == ".wma")
                    || (ext == ".avi") || (ext == ".mp3") || (ext == ".3gp") || (ext == ".zip")
                    || (ext == ".rar") || (ext == ".exe") || (ext == ".dll") || (ext == ".ai")
                    || (ext == ".psd") || (ext == ".cdr") || (ext == ".xml") || (ext == ".css")
                    || (ext == ".html") || (ext == ".js") || (ext == ".asp") || (ext == ".aspx")
                    || (ext == ".php") || (ext == ".indd") || (ext == ".fla") || (ext == ".aed")
                    || (ext == ".bat") || (ext == ".swf") || (ext == ".svg") || (ext == ".ppj")
                    || (ext == ".tif") || (ext == ".dwg") || (ext == ".sql") || (ext == ".mwb")
                    || (ext == ".bak") || (ext == ".asf") || (ext == ".bmp") || (ext == ".iso")
                    || (ext == ".bin") || (ext == ".dxf") || (ext == ".eps") || (ext == ".vob")
                    || (ext == ".wri") || (ext == ".veg") || (ext == ".txt") || (ext == ".wri"))
                {
                    int tamanhoT = ext.Length;
                    nomeMiniatura = "../Uploads/miniaturas/" + ext.Substring(1, tamanhoT - 1) + ".png";
                }

                else
                    nomeMiniatura = "../Uploads/miniaturas/outro.png";

                arq.Arq_miniatura = nomeMiniatura;

                //string nome = DateTime.Now.ToString("yyyMMddHHmmssfff") + ext;
                string nome = Path.GetFileName(flpArquivo.FileName);
                flpArquivo.SaveAs(dir + nome);

                arq.Arq_caminho = dataNow + "\\" + nome;

                string dataCriacao = Convert.ToString(DateTime.Now);
                string dia = dataCriacao.Substring(0, 2);
                string mes = dataCriacao.Substring(3, 2);
                string ano = dataCriacao.Substring(6, 4);

                string hora = dataCriacao.Substring(11, 2);
                string minutos = dataCriacao.Substring(14, 2);
                string segundos = dataCriacao.Substring(17, 2);

                arq.Arq_dataEnvio = ano + "-" + mes + "-" + dia; //intervalo da data só
                arq.Arq_horario = hora + ":" + minutos + ":" + segundos;

                tar.Tar_codigo = codigoTarefa;
                arq.Tar_codigo = tar;

                if (ArquivoDB.Inserir(arq) != -2)
                {
                    carregaArquivos(codigoTarefa);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "swal('Bom trabalho!', 'O arquivo foi salvo com sucesso.', 'success');document.getElementById('visualizarTarefa').style.display = 'block';", true);
                }

                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "swal('Bom trabalho!', 'O arquivo foi salvo com sucesso.', 'success');document.getElementById('visualizarTarefa').style.display = 'block';", true);
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "swal('Tente novamente!', 'Tamanho máximo " + (tamanhoMax / 1000) + "MB.', 'error');document.getElementById('visualizarTarefa').style.display = 'block';", true);
        }

        else
            Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "swal('Tente novamente!', 'Selecione um arquivo.', 'error');document.getElementById('visualizarTarefa').style.display = 'block';", true);
    }

    private void carregaArquivos(int codTar)
    {
        DataSet ArquivosDS = ArquivoDB.SelectArquivosTarefa(codTar);
        rptArquivosTarefa.DataSource = ArquivosDS.Tables[0].DefaultView;
        rptArquivosTarefa.DataBind();
    }
    protected void rptArquivosTarefa_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Excluir")
        {
            int codigoTarefa = Convert.ToInt32(lblCodTarefa.Text);

            chave = Request["grupo"];
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            arq = ArquivoDB.Selecionar(Convert.ToInt32(e.CommandArgument));
            string dir = Request.PhysicalApplicationPath + "Uploads/arquivos/tarefa" + codigoTarefa +
                "grupo" + codGrupo + "\\";

            int tamanho = (arq.Arq_caminho.Length) - 19; //tamanho total do nome do arquivo


            if (File.Exists(dir + arq.Arq_caminho.Substring(0, 18) + "\\"
                + arq.Arq_caminho.Substring(19, tamanho)))  //Verifica se o arquivo existe
            {
                File.Delete(dir + arq.Arq_caminho.Substring(0, 18) + "\\" + arq.Arq_caminho.Substring(19, tamanho)); //apagar o arquivo
                Directory.Delete(dir + "\\" + arq.Arq_caminho.Substring(1, 18), true); //apagar a pasta
            }

            if (ArquivoDB.Excluir(arq.Arq_codigo) != -2)
            {
                codigoTarefa = Convert.ToInt32(lblCodTarefa.Text);
                carregaArquivos(codigoTarefa);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "swal('Bom trabalho!', 'Arquivo excluído.', 'success');document.getElementById('visualizarTarefa').style.display = 'block';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "swal('Tente novamente!', 'Erro ao excluir arquivo.', 'error');document.getElementById('visualizarTarefa').style.display = 'block';", true);
            }
        }

        if (e.CommandName == "Baixar")
        {
            int codigoTarefa = Convert.ToInt32(lblCodTarefa.Text);

            chave = Request["grupo"];
            Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
            codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

            string argumento = Convert.ToString(e.CommandArgument);
            string caminho = argumento.Substring(0, 18); //Ex: /2018786764352671
            string nome = argumento.Substring(19, (argumento.Length) - 19); //Ex: MER_19_04.mwb

            //Pega o nome completo do arquivo desejado
            string nomeArquivo = Server.MapPath("~/Uploads/arquivos/tarefa") + codigoTarefa +
                "grupo" + codGrupo + caminho + "\\" + nome;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "swal('Tente" + nomeArquivo + "!', 'Erro ao excluir arquivo.', 'error');document.getElementById('visualizarTarefa').style.display = 'block';", true);

            //Pega os dados do arquivo para baixar
            FileInfo arquivo = new FileInfo(nomeArquivo);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nome);
            Response.AddHeader("Content-Length", arquivo.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(nomeArquivo);
            Response.End();
        }
    }
    protected void txtUsername_TextChanged(object sender, EventArgs e)
    {
        //if (e.KeyChar == (char)13)
        //{

        //}
    }

    protected void btnAlterarTarefa_Click(object sender, EventArgs e)
    {
        lblNomeTarefaVer.Visible = false;
        txtNomeTarefaVer.Visible = true;
        txtTemaTarefaVer.Visible = false;
        TxtDataTarefaVer.Visible = false;
        txtDescricaoTarefaVer.Visible = true;
        lblDescricaoVer.Visible = false;
        txtPrazoTarefaVer.Visible = true;
        ddlTemaTarefaVer.Visible = true;
        lsbMembros.Enabled = false;
        btnAlterarTarefa.Visible = false;
        btnSalvar.Visible = true;

        int codTar = Convert.ToInt32(lblCodTar.Text);
        tar = tarDB.SelectTarefaInfo(codTar);

        //INFORMAÇÕES DA TAREFA

        txtNomeTarefaVer.Text = tar.Tar_nome;
        txtDescricaoTarefaVer.Text = tar.Tar_descricao;

        int codTema = tar.Tem_codigo.Tem_codigo;
        tem = temDB.SelectTemaTarefa(codTema);
        txtTemaTarefaVer.Text = tem.Tem_nome;

        txtTipoTarefaVer.Text = tar.Tar_tipo;

        data = tar.Tar_prazo;

        string dia = data.Substring(0, 2);
        string mes = data.Substring(3, 2);
        string ano = data.Substring(6, 4);

        TxtDataTarefaVer.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);

        string dataInput = ano + "-" + mes + '-' + dia;
        txtPrazoTarefaVer.Text = dataInput;

        lblCodTar.Text = Convert.ToString(codTar);

        CarregarDllTemasAlterar(codTema);

        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", @"<script>
                document.getElementById('visualizarTarefa').style.display = 'block';
                </script>", false);

    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        int codTar = Convert.ToInt32(lblCodTar.Text);
        tar.Tar_codigo = codTar;

        tar.Tar_nome = txtNomeTarefaVer.Text;
        tar.Tar_prazo = txtPrazoTarefaVer.Text;

        tem.Tem_codigo = Convert.ToInt32(ddlTemaTarefaVer.SelectedValue);
        tar.Tem_codigo = tem;

        tar.Tar_descricao = txtDescricaoTarefaVer.Text;

        switch (TarefaDB.AlteraTarefa(tar))
        {
            case 0:
                lblNomeTarefaVer.Visible = true;
                txtNomeTarefaVer.Visible = false;
                txtTemaTarefaVer.Visible = true;
                TxtDataTarefaVer.Visible = true;
                txtDescricaoTarefaVer.Visible = false;
                lblDescricaoVer.Visible = true;
                txtPrazoTarefaVer.Visible = false;
                ddlTemaTarefaVer.Visible = false;
                lsbMembros.Enabled = true;
                btnAlterarTarefa.Visible = true;
                btnSalvar.Visible = false;

                chave = Request["grupo"]; //Pegar código do grupo pela URL
                Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
                codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

                carregaTarefasAFazer(codGrupo);
                carregaTarefasFazendo(codGrupo);
                carregaTarefasFeito(codGrupo);

                //ATUALIZAR INFORMAÇÕES NOVAS DA TAREFA
                tar = tarDB.SelectTarefaInfo(codTar);

                lblNomeTarefaVer.Text = tar.Tar_nome;
                lblDescricaoVer.Text = tar.Tar_descricao;

                int codTema = tar.Tem_codigo.Tem_codigo;
                tem = temDB.SelectTemaTarefa(codTema);
                txtTemaTarefaVer.Text = tem.Tem_nome;

                txtTipoTarefaVer.Text = tar.Tar_tipo;

                data = tar.Tar_prazo;

                string dia = data.Substring(0, 2);
                string mes = data.Substring(3, 2);
                string ano = data.Substring(6, 4);

                TxtDataTarefaVer.Text = Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + Convert.ToString(ano);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>" +
                    "swal('Bom trabalho!','Dados alterados com sucesso.','success');" +
                    "document.getElementById('visualizarTarefa').style.display = 'block';</script>", false);
                break;

            case -2:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>" +
                    "swal('Erro não identificado!','Dados não alterados.','error');" +
                    "document.getElementById('visualizarTarefa').style.display = 'block';</script>", false);
                break;
        }
    }

    protected void btnFecharTarefa_Click(object sender, EventArgs e)
    {
        lblNomeTarefaVer.Visible = true;
        txtNomeTarefaVer.Visible = false;
        txtTemaTarefaVer.Visible = true;
        TxtDataTarefaVer.Visible = true;
        txtDescricaoTarefaVer.Visible = false;
        lblDescricaoVer.Visible = true;
        txtPrazoTarefaVer.Visible = false;
        ddlTemaTarefaVer.Visible = false;
        lsbMembros.Enabled = true;
        btnAlterarTarefa.Visible = true;
        btnSalvar.Visible = false;

        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>document.getElementById('visualizarTarefa').style.display = 'none';</script>", false);
    }
}