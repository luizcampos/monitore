<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Logado.master" AutoEventWireup="true" CodeFile="configuracaoGrupo.aspx.cs" Inherits="paginas_configuracaoGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <br />
    <br />

    <section class="masthead text-black text-center" style="background-color: #FFF;">

        <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1" style="margin-bottom: 10px; font-size: 20px;">
            <asp:Button ID="btnVoltar" CssClass="btn btnAtivo" OnClick="btnVoltar_Click" runat="server" Text="Voltar" />
        </div>

        <img src="../img/icones/informacao.png" width="30" height="30" />
        <h3>INFORMAÇÕES DO GRUPO</h3>

        <br />

        <div class="container">

            <asp:Panel ID="pnlConfig" DefaultButton="btnOkConfig" runat="server">
                <div class="row">

                    <div class="col-xs-2 col-sm-1 col-md-1 col-lg-1">
                        <img src="/img/icones/tipo.png" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-2 col-sm-2 col-md-3 col-lg-2">
                        <p class="text-icones">Nome do Grupo:</p>
                    </div>

                    <div class="col-xs-8 col-sm-6 col-md-5 col-lg-4">
                        <asp:TextBox ID="txtNomeGrupoConfig" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-xs-8 col-sm-3 col-md-3 col-lg-1">
                        <asp:Button ID="btnAlterarDados" runat="server" Text="Alterar dados" OnClick="btnAlterarDados_Click"
                            CssClass="btn efeitoBotao1 btn-amarelo" />

                        <asp:Button ID="btnOkConfig" runat="server" OnClick="btnOkConfig_Click"
                            CssClass="btn btn-success text-left" Text="Ok" Visible="false" />
                    </div>
                </div>
            </asp:Panel>

            <div class="row">
                <div class="col-xs-2 col-sm-1 col-md-1 col-lg-1">
                    <img src="../img/icones/descricao.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-2 col-sm-2 col-md-3 col-lg-2">
                    <p class="text-icones">Cor:</p>
                </div>

                <div class="col-xs-8 col-sm-6 col-md-5 col-lg-4">
                    <asp:DropDownList ID="dllCor" runat="server" CssClass="form-control" AutoPostBack="true"
                        OnSelectedIndexChanged="dllCor_SelectedIndexChanged" Enabled="true">
                        <asp:ListItem Value="#000000">Preto</asp:ListItem>
                        <asp:ListItem Value="#33B4E5">Azul</asp:ListItem>
                        <asp:ListItem Value="#23ce6b">Verde</asp:ListItem>
                        <asp:ListItem Value="#ea0e0e">Vermelho</asp:ListItem>
                        <asp:ListItem Value="#8B0000">Vinho</asp:ListItem>
                        <asp:ListItem Value="#828282">Cinza</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="ltrCor" runat="server"></asp:Literal>
                </div>
            </div>

            <asp:Literal ID="ltrInfosGrupo" runat="server"></asp:Literal>

            <br />
            <br />

            <img src="../img/icones/membros.png" width="30" height="30" />
            <h3>MEMBROS DO GRUPO</h3>

            <br />

            <div class="row text-center">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 tabela-planosAdm">
                    <table class="table table-hover">
                        <tr style="background-color: rgba(24,188,156,.9); color: #FFFFFF; font-size: 18px;">
                            <td>Nome
                            </td>
                            <td>Email
                            </td>
                            <td>Username
                            </td>
                            <td>Tipo
                            </td>
                        </tr>

                        <asp:Repeater ID="rptMembrosConfig" runat="server" OnItemCommand="rptMembrosConfig_ItemCommand">
                            <ItemTemplate>

                                <tr>
                                    <td>
                                        <p><%#Eval("usu_nome") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("usu_email") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("usu_username") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("tipo") %></p>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Literal ID="ltrMensagemMembros" runat="server"></asp:Literal>
                </div>
            </div>

            <br />
            <br />
            <br />

            <img src="../img/icones/tarefasHistorico.png" width="30" height="30" />
            <h3>HISTÓRICO DE TAREFAS DO GRUPO</h3>

            <br />

            <div class="row text-center">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 tabela-planosAdm">
                    <table class="table table-hover">
                        <tr style="background-color: rgba(24,188,156,.9); color: #FFFFFF; font-size: 18px;">
                            <td>Nome
                            </td>
                            <td>Descrição
                            </td>
                            <td>Data de criação
                            </td>
                            <td>Tipo
                            </td>
                            <td>Qtde de membros
                            </td>
                            <td>Status
                            </td>
                        </tr>

                        <asp:Repeater ID="rptHistoricoTarefas" runat="server" OnItemCommand="rptHistoricoTarefas_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <p><%#Eval("tar_nome") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("tar_descricao") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("dataCriacao") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("tar_tipo") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("tar_qtde_membros") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("tar_status") %></p>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Literal ID="ltrMensagemHistorico" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

