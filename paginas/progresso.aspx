<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Logado.master" AutoEventWireup="true" CodeFile="progresso.aspx.cs" Inherits="paginas_progresso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/tbl-index-logado.css" rel="stylesheet" />

    <style>
        .blocoGrupo {
            background-color: black;
            width: 100%;
            min-height: 300px;
            border: 1px solid gray;
            border-radius: 10px;
            margin-top: 10px;
        }

        .NomeGrupo {
            color: white;
            font-size: 20px;
        }

        hr {
            border-color: dimgray;
        }

        .progresso {
            width: 90%;
            margin-left: auto;
            margin-right: auto;
        }

        .iconeCriarGrupo {
            color: white;
            font-size: 30px;
            margin-top: 20px;
            /*margin-right: 150px;*/
        }

        .textoGrupo {
            margin-top: 1px;
            color: white;
        }

        .porcentagem {
            color: black;
            font-size: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section style="background-color: #FFF;">
        <div class="container">
            <div class="text-center">
                <br />
                <h3>PROGRESSO DO GRUPO</h3>
            </div>

            <div class="row">
                <asp:Literal ID="ltrBarraProgresso" runat="server"></asp:Literal>
            </div>

            <div class="row" style="text-align: center; margin-top: 10px;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4" style="margin-top: 10px; font-size: 20px;">
                    <asp:Button ID="btnVoltar" CssClass="btn btnAtivo" OnClick="btnVoltar_Click" runat="server" Text="Voltar" />
                </div>

                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4" style="margin-top: 10px; font-size: 20px;">
                    <asp:Label ID="lblProgressoConcluido" Style="font-weight: bold;" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblTarefasConcluidas" CssClass="fundoTarefas" runat="server" Text=""></asp:Label>
                </div>

                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4" style="margin-top: 10px; font-size: 20px;">
                    <asp:Label ID="lblProgressoRestante" Style="font-weight: bold;" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblTarefasRestantes" CssClass="fundoTarefas" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <br />

            <div class="row" style="text-align: center; margin-top: 10px;">
                <asp:Literal ID="ltrMediaGrupal" runat="server"></asp:Literal>
            </div>

            <br />
            <br />

            <!-- PROGRESSO INDIVIDUAL -->

            <div class="container text-center">
                <div class="text-center">
                    <h3>PROGRESSO INDIVIDUAL</h3>
                </div>
                <br />
                <div class="row">
                    <asp:Literal ID="ltrProgressoMembros" runat="server"></asp:Literal>
                </div>

                <br />
                <br />

                <!-- PROGRESSO NOS TEMAS -->
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <h3>PROGRESSO NOS TEMAS</h3>
                    </div>
                </div>

                <br />
                <br />

                <div class="row">
                    <asp:Literal ID="ltrProgressoTemaUser" runat="server"></asp:Literal>
                </div>
            </div>

            <div class="text-center">
                <br /><br />
                <h3>RANKING DAS TAREFAS</h3>
                <br />
            </div>

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3"></div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <table class="table text-center">
                        <tr>
                            <td><b>POSIÇÃO</b>
                            </td>
                            <td><b>MEMBRO</b>
                            </td>
                              <td><b>Nº DE TAREFAS FEITAS</b>
                            </td>
                        </tr>
                        <asp:Literal ID="ltrRanking" runat="server"></asp:Literal>
                        <asp:Literal ID="ltrMensagem" runat="server"></asp:Literal>
                    </table>
                    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3"></div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

