using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monitore.Classes;
using Monitore.Persistencias;

public partial class paginas_progresso : System.Web.UI.Page
{
    private string chave, div;
    private int codGrupo, cont = 1;
    Temas tem = new Temas();
    TemasDB temDB = new TemasDB();
    GrupoDB gruDB = new GrupoDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        carregarDadosProgresso();
        carregaRanking();

        if (Session["ID"] == null)
        {
            Response.Redirect("sair.aspx");
        }

    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        chave = Request["grupo"]; 

        Response.Redirect("grupo.aspx?grupo=" + chave);
    }

    private void carregarDadosProgresso()
    {
        chave = Request["grupo"]; //Pegar código do grupo pela URL
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        //BARRA PROGRESSO

        DataSet ds = GrupoDB.SelectPorcentagemGrupo(codGrupo);
        int qtdeProgresso = ds.Tables[0].Rows.Count;

        if (qtdeProgresso > 0)
        {
            string div = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                div += @"<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12 barraProgressoGrupo' style='height: 60px; margin-top: 30px; background: -webkit-linear-gradient(left, #01a518 " + dr["tarefas"] + @"%, #c7c3c3 " + dr["tarefas"] + @"%); background: -moz-linear-gradient(left, #01a518 " + dr["tarefas"] + @"%, #c7c3c3 " + dr["tarefas"] + @"%);'
                         title='" + dr["tarefas"] + @"% das tarefas concluídas'>
                        </div>";
                lblProgressoConcluido.Text = dr["tarefas"] + "% Concluído";
                lblProgressoRestante.Text = (100 - Convert.ToInt32(dr["tarefas"])) + "% Restante";
            }

            ltrBarraProgresso.Text = div;
        }

        if (qtdeProgresso <= 0)
        {
            string div = "";
            div += @"<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12 barraProgressoGrupo' style='height: 60px; margin-top: 30px; background: -webkit-linear-gradient(left, #01a518 45%, #c7c3c3 0%); background: -moz-linear-gradient(left, #01a518 45%, #c7c3c3 0%);'>
                            <a id='lblPorcentagemGrupo' style='font-size: 40px;'>Impossível quantificar progresso</a>
                        </div>";
            lblProgressoConcluido.Text = "0% Concluído";
            ltrBarraProgresso.Text = div;
        }

        //TOTAL TAREFAS

        ds = TarefaDB.SelectTotalTarefas(codGrupo);
        int qtde = ds.Tables[0].Rows.Count;

        if (qtde > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lblTarefasConcluidas.Text = dr["Feito"] + " de " + dr["Total"] + " tarefas ao todo";
                lblTarefasRestantes.Text = "Faltam " + ((Convert.ToInt32(dr["Total"])) - (Convert.ToInt32(dr["Feito"]))) + " tarefas";
                double media = (((Convert.ToDouble(dr["Feito"])) / (Convert.ToDouble(dr["Total"]))) * 10);

                if ((Convert.ToDouble(dr["Feito"])) <= 0 || (Convert.ToDouble(dr["Total"])) <= 0)
                {
                    media = 0;
                }

                else
                {
                    ltrMediaGrupal.Text = @"<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12'>
                    <label style='font-weight: bold; background-color: #151414; color: #FFF; margin-bottom: 30px; font-size: 18px;'>&nbsp NOTA ESTIPULADA: "
                    + media.ToString("0.00") + @"</label></div>";
                }
            }
        }

        if (qtde <= 0)
        {
            lblTarefasConcluidas.Text = "0 de 0 tarefas";
            lblTarefasRestantes.Text = "Faltam 0 tarefas";
            ltrMediaGrupal.Text = @"<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12'>
                    <label style='font-weight: bold; background-color: #151414; color: #FFF; margin-bottom: 30px; font-size: 18px;'>&nbsp NOTA ESTIPULADA: 0
                    </label></div>";
        }

        //NOMES E PROGRESSO DOS INTEGRANTES (UM DE CADA VEZ)

        ds = MembrosTarefasDB.SelectNomesIntegrantes(codGrupo);
        int qtdeMembros = ds.Tables[0].Rows.Count;

        if (qtdeMembros > 0)
        {
            string div = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int codigoGrupo = Convert.ToInt32(dr["gru_grupo_gru_codigo"]);
                int codUser = Convert.ToInt32(dr["usu_codigo"]);
                int codMembro = Convert.ToInt32(dr["mem_codigo"]);

                div += @"<div class='col-xs-12 col-sm-12 col-md-4 col-lg-4'>
                        <img src='/img/icones/user.png' width='30' height='30' />
                        " + dr["usu_nome"] + @"
                        <br />";

                //PROGRESSO DOS INTEGRANTES

                DataSet ds2 = MembrosTarefasDB.SelectProgressoIntegrante(codigoGrupo, codMembro, codUser);
                int qtdeMembros2 = ds2.Tables[0].Rows.Count;

                if (qtdeMembros2 > 0)
                {
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        double media = 0;

                        if ((Convert.ToDouble(dr2["TotalFeito"])) <= 0 || (Convert.ToDouble(dr2["TotalIndividual"])) <= 0)
                        {
                            media = 0;
                        }

                        else
                        {
                            media = (((Convert.ToDouble(dr2["TotalFeito"])) / (Convert.ToDouble(dr2["TotalIndividual"]))) * 10);
                        }

                        div += @"<div class='barraProgressoIndividual' style='background: -webkit-linear-gradient(left, #01a518 " + dr2["Progresso"] + @"%, #c7c3c3 " + dr2["Progresso"] + @"%); background: -moz-linear-gradient(left, #01a518 " + dr2["Progresso"] + @"%, #c7c3c3 " + dr2["Progresso"] + @"%);'>
                            " + dr2["Progresso"] + @"%
                            </div>

                            <br />
                                Realizou " + dr2["TotalFeito"] + @" de suas " + dr2["Total"] + @" tarefas
                            <br />
                            <label style='font-weight: bold; background-color: #151414; color: #FFF; margin-bottom: 30px; font-size: 18px;'>&nbsp NOTA ESTIPULADA: 
                            " + media.ToString("0.00") + @"</label>
                        
                            </div>";
                    }
                }

                if (qtdeMembros2 <= 0)
                {
                    div += @"Dados não calculados.
                        </div>";
                }
                ltrProgressoMembros.Text = div;
            }
        }

        if (qtdeMembros <= 0)
        {
            string div = @"Dados não calculados.";
            ltrProgressoMembros.Text = div;
        }

        carregaProgressoTemas();
    }

    private void carregaProgressoTemas()
    {
        //TEMAS E PROGRESSO DOS INTEGRANTES (1 DE CADA VEZ)
        DataSet ds = TemasDB.SelectTemasGrupoAll(codGrupo);
        int qtdeMembros = ds.Tables[0].Rows.Count;

        if (qtdeMembros > 0)
        {
            string div = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int codigoGrupo = Convert.ToInt32(dr["gru_grupo_gru_codigo"]);
                int codTema = Convert.ToInt32(dr["tem_temas_tem_codigo"]);
                string correcaoTarefa = "tarefa";
                tem = temDB.SelectQtdeTarefaDoTema(codTema, codigoGrupo);

                if (tem.Qtde_tarefas_tema > 1)
                {
                    correcaoTarefa = "tarefas";
                }


                div += @"<div class='col-xs-12 col-sm-6 col-md-4 col-lg-3'>";

                div += @"<div class='blocoGrupo'>
                            <div class='NomeGrupo'>" + dr["tem_nome"] + @"</div>
                            <div style='color: #FFFF00; font-weight: bolder;'>"
                             + tem.Qtde_tarefas_tema + @" " + correcaoTarefa + @"</div>
                            <hr /><div class='scrollGrupo'>";

                //NOMES DOS MEMBROS (1 DE CADA VEZ)

                DataSet ds2 = MembrosTarefasDB.SelectNomesIntegrantes(codGrupo);
                int qtdeMembros2 = ds2.Tables[0].Rows.Count;

                if (qtdeMembros2 > 0)
                {
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        int codMembro = Convert.ToInt32(dr2["mem_codigo"]);

                        div += @"<div class='textoGrupo'>" + dr2["usu_nome"] + @"</div>";

                        //PROGRESSO DOS INTEGRANTES NOS TEMAS

                        DataSet ds3 = TemasDB.SelectProgressoTemaIntegrante(codTema, codigoGrupo, codMembro);
                        int qtdeMembros3 = ds3.Tables[0].Rows.Count;

                        if (qtdeMembros3 > 0)
                        {
                            foreach (DataRow dr3 in ds3.Tables[0].Rows)
                            {
                                int media = (Convert.ToInt32(dr3["TotalTemaFeito"]) * 100) / (Convert.ToInt32(dr3["TotalTema"]));
                                div += @"<div class='progress progresso'>
                                            <div id='barraTemaMembro1' class='progress-bar bg-success progress-bar-striped' style='text-align: center; width:" + media + @"%;'>
                                                <div class='porcentagem'>" + media + @"%</div>
                                            </div>
                                        </div>
                                        <br />";
                            }
                        }

                        if (qtdeMembros3 <= 0)
                        {
                            int media = 0;
                            div += @"<div class='progress progresso'>
                                            <div id='barraTemaMembro1' class='progress-bar bg-success progress-bar-striped' style='text-align: center; width:" + media + @"%;'>
                                                <div class='porcentagem'>" + media + @"%</div>
                                            </div>
                                        </div>
                                        <br />";
                        }
                    }
                }

                if (qtdeMembros2 <= 0)
                {
                    int media = 0;
                    div += @"<div class='progress progresso'>
                                            <div id='barraTemaMembro1' class='progress-bar bg-success progress-bar-striped' style='text-align: center; width:" + media + @"%;'>
                                                <div class='porcentagem'>" + media + @"%</div>
                                            </div>
                                        </div>
                                        <br />";
                }

                //Fechou um bloco de um tema
                cont = 1;
                div += @"</div></div></div>";
                ltrProgressoTemaUser.Text = div;
            }
        }

        if (qtdeMembros <= 0)
        {
            ltrProgressoTemaUser.Text = @"<div class='col-lg-12 col-md-12 col-sm-12' style='font-size: 22px;'>
                Impossível quantificar progresso!</div>
                <div class='col-lg-12 col-md-12 col-sm-12'>
                    <p style='font-size: 16px; font-weight: bold;'> Não existem tarefas nesse grupo.<p></div><br/>";
        }
    }

    private void carregaRanking()
    {
        chave = Request["grupo"];
        Grupo gruCodigo = gruDB.SelectCodigoGrupo(chave);
        codGrupo = Convert.ToInt32(gruCodigo.Gru_codigo);

        DataSet rankingDS = RankingDB.SelectRankingMembrosTarefasFeitas(codGrupo);
        int qtdeMensagens = rankingDS.Tables[0].Rows.Count;
        int posicao = 1;

        if (qtdeMensagens > 0)
        {
            foreach (DataRow dr in rankingDS.Tables[0].Rows)
            {
                string medalha = "";
                if (posicao == 1)
                {
                    medalha = "<img src='../img/icones/ouro.png' width='30px' height='30px'/>";
                }

                if (posicao == 2)
                {
                    medalha = "<img src='../img/icones/prata.png' width='30px' height='30px'/>";
                }

                if (posicao == 3)
                {
                    medalha = "<img src='../img/icones/bronze.png' width='30px' height='30px'/>";
                }

                div += @"<tr style='font-size: 17px;'>
                                    <td>
                                        <p style='font-weight: bolder;'>" + posicao + @"º</p>
                                    </td>
                                    <td>
                                        <p>" + dr["usu_nome"] + medalha + @"</p>
                                    </td>
                                    <td>
                                        <p style='font-size: 18px;'>" + dr["qtde"] + @"</p>
                                    </td>
                                </tr>";
                posicao++;
            }
            ltrRanking.Text = div;
        }

        if (qtdeMensagens <= 0)
        {
            ltrMensagem.Text = @"<tr>Não existem tarefas feitas.</tr>";
        }
    }
}