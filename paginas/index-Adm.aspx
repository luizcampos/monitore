<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Adm.master" AutoEventWireup="true" CodeFile="index-Adm.aspx.cs" Inherits="paginas_index_Adm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/index-Adm.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <br />
    <section class="masthead text-black text-center" style="background-color: #FFF;">
        <div class="container">

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <h3 style="font-family: Candara;">Olá, Administrador!</h3>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3 box-inf">
                    <p style="color: lightslategray; margin-top: 5px;">Lucro Mensal</p>
                    <i class="fa fa-usd" style="color: forestgreen; font-size: 40px;"></i>
                    <asp:Label ID="lblLucroMensal" runat="server" Text=""></asp:Label>
                </div>

                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3 box-inf">
                    <p style="color: lightslategray; margin-top: 5px;">Usuários Premium</p>
                    <i class="fa fa-diamond" style="color: deepskyblue; font-size: 40px;"></i>
                    <asp:Label ID="lblPremiumTotal" runat="server" Text=""></asp:Label>
                </div>

                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3 box-inf">
                    <p style="color: lightslategray; margin-top: 5px;">Total de Usuários</p>
                    <i class="fa fa-user" style="color: yellow; font-size: 40px;"></i>
                    <asp:Label ID="lblUsuarioTotal" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <br /><br /><br /><br />

            <div class="row">

                <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5 lista-novos" style="padding: 0;">
                    <h4 style="font-weight: bold;">Novos Usuários</h4>
                    <hr style="background-color: green; height:4px;"/>

                    <asp:Repeater ID="rptListaNovos" runat="server" OnItemCommand="rptListaNovos_ItemCommand">
                        <ItemTemplate>
                            <div style="padding: 20px;">
                                <i class="fa fa-user" style="float: left; font-size: 25px; color: green;"></i>
                                <p><%#Eval("usu_nome") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="ltrMensagemNovos" runat="server"></asp:Literal>
                </div>

                <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2"></div>

                <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5 lista-novos" style="padding: 0;">
                    <h4 style="font-weight: bold;">Usuários Premium</h4>
                    <hr style="background-color: deepskyblue; height:4px;"/>

                    <asp:Repeater ID="rptListaPremium" runat="server" OnItemCommand="rptListaPremium_ItemCommand">
                        <ItemTemplate>
                            <div style="padding: 20px;">
                                <i class="fa fa-user" style="float: left; font-size: 25px; color: deepskyblue;"></i>
                                <p><%#Eval("usu_nome") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="ltrMensagensPremium" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

