<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Adm.master" AutoEventWireup="true" CodeFile="planos.aspx.cs" Inherits="paginas_planos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/popup.js"></script>

    <script>
        function abrirContatoCliente() {
            document.getElementById('contatoCliente').style.display = 'block';
        }

        function fecharContatoCliente() {
            document.getElementById('contatoCliente').style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <br />

    <section class="masthead text-black text-center" style="background-color: #FFF;">
        <div class="container">
            <br />
            <h3>PLANOS</h3>
            <%--<br />
            <br />

            <div class="row">

                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <h4>FILTRO</h4>

                    <asp:DropDownList ID="ddlFiltroPlanos" CssClass="form-control" runat="server">
                        <asp:ListItem Selected="True">Todos</asp:ListItem>
                        <asp:ListItem>Basic</asp:ListItem>
                        <asp:ListItem>Premium</asp:ListItem>
                        <asp:ListItem>Pendentes</asp:ListItem>
                        <asp:ListItem>Bloqueados</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" id="filtroProcurar">

                    <h4>PROCURAR:</h4>
                    <asp:TextBox ID="txtProcurarPlano" CssClass="form-control" placeholder="Digite o nome ou código" runat="server"></asp:TextBox>

                </div>
            </div>--%>

            <br />
            <br />

            <div class="row">

                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 tabela-planosAdm">
                    <table class="table table-hover">
                        <tr>
                            <h4>GERAL</h4>
                        </tr>

                        <tr>
                            <td>Código
                            </td>
                            <td>Nome
                            </td>
                            <td>Informações
                            </td>
                            <td>Status
                            </td>
                        </tr>

                        <asp:Repeater ID="rptListaClientes" runat="server" OnItemCommand="rptListaClientes_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <p><%#Eval("usu_codigo") %></p>
                                    </td>

                                    <td>
                                        <p><%#Eval("usu_nome") %></p>
                                    </td>

                                    <td style="cursor: pointer">
                                        <asp:LinkButton ID="lkbLupaMais" CommandArgument='<%#Eval("usu_codigo") %>' CommandName="Ver"
                                            runat="server">
                                            <img src="/img/icones/lupaMais.jpg" width="20" height="20" title="Visualizar informações do cliente"/>
                                        </asp:LinkButton>

                                    </td>

                                    <td>
                                        <p><%#Eval("usu_status") %></p>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </table>
                </div>
            </div>
        </div>
    </section>

    <%--ALTERAÇÃO DESSA MODAL 30/03/2018--%>
    <!-- POP-UP PERFIL USUARIO ADM -->

    <div class="popupMaior" id="perfilUA">
        <br />
        <br />
        <div class="popup-header">
            <h3 class="text-secondary text-uppercase mb-0">PERFIL do USUÁRIO</h3>
            <a href="javascript: fecharPerfilUA();" class="btn-fechar">X</a>
        </div>
        <div class="popup-conteudo">

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/user.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Nome:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblNomePerfil" runat="server" Text="Label"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/sexo.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Sexo:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblSexoPerfil" runat="server" Text="Label"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/data.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Data:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblDataNascimento" runat="server" Text="Label"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/email2.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Email:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/cpf.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">CPF:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblCpf" runat="server" Text="Label"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/tipo.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Conta:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblTipoConta" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <%--<img class="img-icones" src="/img/icones/tipo.png" alt="" width="30" height="30" />--%>
                    <i class="fa fa-money img-icones" style="font-size: 25px;"></i>
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Preço:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblPreco" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <%--<img class="img-icones" src="/img/icones/tipo.png" alt="" width="30" height="30" />--%>
                    <i class="fa fa-calendar-check-o img-icones" style="font-size: 24px;"></i>
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Data de contratação:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblDataCon" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <%--<img class="img-icones" src="/img/icones/tipo.png" alt="" width="30" height="30" />--%>
                    <i class="fa fa-calendar-times-o img-icones" style="font-size: 24px;"></i>
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Data de validade:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblDataValidade" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <br />
            <asp:Button ID="btnBloquearUsuario" runat="server" CssClass="btn btn-danger" Text="Bloquear" OnClick="btnBloquearUsuario_Click" />
            <asp:Button ID="btnAtivarUsuario" runat="server" CssClass="btn btn-success" Text="Ativar" OnClick="btnAtivarUsuario_Click" />
            <asp:Button ID="btnRealizarContato" OnClick="btnRealizar_Contato_Click" runat="server" CssClass="btn btn-secondary" Text="Realizar Contato" Enabled="true" />
            <asp:Label ID="lblCodigoCliente" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblStatus" runat="server" Visible="false" Text=""></asp:Label>
        </div>
    </div>

    <!-- POP-UP CONTATO CLIENTE -->

    <div class="popupMaior" id="contatoCliente">
        <br />

        <div class="popup-header">
            <h3 class="text-secondary text-uppercase mb-0">ENVIAR MENSAGEM</h3>
            <a href="javascript: fecharContatoCliente();" class="btn-fechar">X</a>
        </div>

        <div class="popup-conteudo">

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <i class="fa fa-envelope" style="font-size: 25px;"></i>
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Username:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblUsernameMensagem" runat="server"></asp:Label>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <i class="fa fa-question-circle" style="font-size: 25px;"></i>
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Assunto:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:TextBox ID="txtAssuntoMensagem" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <i class="fa fa-pencil" style="font-size: 25px;"></i>
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Descrição:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:TextBox ID="txtTextoMensagem" CssClass="textarea form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                </div>
            </div>

            <br />
            <asp:Button ID="btnEnviarMsg" CssClass="btn btn-success" runat="server" Text="Enviar"
                OnClick="btnEnviarMsg_Click" />
        </div>
    </div>
</asp:Content>

