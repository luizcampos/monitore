<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Logado.master" AutoEventWireup="true" CodeFile="chatGrupo.aspx.cs" Inherits="paginas_chatGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <br />
    <br />

    <section class="masthead text-black text-center" style="background-color: #FFF;">
        <h3>CHAT DO GRUPO</h3>

        <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1" style="margin-bottom: 10px; font-size: 20px;">
            <asp:Button ID="btnVoltar" CssClass="btn btnAtivo" OnClick="btnVoltar_Click" runat="server" Text="Voltar" />     
        </div>

        <br />

        <div class="container">
            <div class="row">
                <asp:Repeater ID="rptAbasMembros" runat="server" OnItemCommand="rptAbasMembros_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbMembroChat" CommandName="Abrir" CommandArgument='<%#Eval("mem_codigo") %>'
                            CssClass="abaMembro col-xs-6 col-sm-6 col-md-4 col-lg-3" runat="server">
                            <p><%#Eval("usu_nome") %></p>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="ltrMensagemAbas" runat="server"></asp:Literal>
            </div>

            <div class="row">
                <div class="fundoChat col-xs-12 col-sm-12 col-md-12 col-lg-12" id="boxChat">
                    <br />
                    <br />

                    <asp:Repeater ID="rptMensagensChat" runat="server" OnItemCommand="rptMensagensChat_ItemCommand">
                        <ItemTemplate>
                            <div class="col-xs-12 col-sm-12 col-md-7 col-lg-7 text-left" id="fundoChat">
                                <p><%#Eval("destinatario") %> diz:</p>
                                <div class="fundoMsgChat">
                                    <p style="margin-left: 18px; font-size: 18px;"><%#Eval("men_conteudo") %></p>
                                    <div class="text-right">
                                        <p style="font-style: italic;"><%#Eval("men_data") %> às <%#Eval("men_horario") %></p>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="ltrLegenda" runat="server"></asp:Literal>

                    <br />
                    <br />

                    <asp:Panel ID="pnlMensagem" DefaultButton="btnEnviar" runat="server">
                        <div class="comentar col-xs-12 col-sm-12 col-md-12 col-lg-12" id="comentarArea" style="float: left;">
                            <div class="text-left">
                                <b>Enviar mensagem:</b>
                            </div>

                            <div class="row">
                                <div class="col-xs-11 col-sm-11 col-md-11 col-lg-11">
                                    <asp:TextBox ID="txtMensagem" TextMode="MultiLine" CssClass="textarea form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                                    <asp:Button ID="btnEnviar" OnClick="btnEnviar_Click" CssClass="btn btn-success"
                                        runat="server" Text="Enviar" />
                                </div>
                            </div>

                            <br />
                        </div>
                    </asp:Panel>

                    <asp:Label ID="lblCodDestinatario" runat="server" Text="0" Visible="false"></asp:Label>
                    <br />
                    <br />

                </div>
            </div>
        </div>
    </section>

</asp:Content>

