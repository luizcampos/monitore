<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Adm.master" AutoEventWireup="true" CodeFile="contato-adm.aspx.cs" Inherits="paginas_contato_adm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/admin.css" rel="stylesheet" />

    <link href="../css/popup.css" rel="stylesheet" />
    <script src="../js/popup.js"></script>

    <script>
        /* PERFIL CLIENTE */

        function fecharPerfilCliente() {
            document.getElementById('perfilCliente').style.display = 'none';
        }

        /* CONTATO CLIENTE */

        function abrirContatoCliente() {
            document.getElementById('contatoCliente').style.display = 'block';
        }

        function fecharContatoCliente() {
            document.getElementById('contatoCliente').style.display = 'none';
        }

        /* EXCLUIR MENSAGEM */

        function abrirExcluirMensagem() {
            document.getElementById('excluirMensagem').style.display = 'block';
        }

        function fecharExcluirMensagem() {
            document.getElementById('excluirMensagem').style.display = 'none';
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <section class="masthead text-black text-center" style="background: #FFF">
        <div class="text-center">
            <h3>CONTATO</h3>
        </div>
        <hr />
        <br />
        <div class="container">

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3"></div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <h4>Mensagem Recebidas</h4>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                    <asp:Button ID="btnEnviarMensagemCliente" CssClass="btn btn-success" 
                        runat="server" Text="Enviar Mensagem" OnClick="btnEnviarMensagemCliente_Click" />
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="tbl-receber">
                        <table class="table">
                            <tr>
                                <td>
                                    <p>CÓDIGO</p>
                                </td>
                                <td>
                                    <p>NOME</p>
                                </td>
                                <td>
                                    <p>ASSUNTO</p>
                                </td>
                                <td>
                                    <p>AÇÕES</p>
                                </td>
                            </tr>

                            <asp:Repeater ID="rptListaMensagens" runat="server" OnItemCommand="rptListaMensagens_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <p><%#Eval("usu_codigo") %></p>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lkbPerfilUsuario" CommandArgument='<%#Eval("usu_codigo") %>'
                                                CommandName="Perfil" runat="server">
                                                <p style="color: black;"><%#Eval("usu_nome") %></p>
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <p><%#Eval("con_titulo") %></p>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnExcluirMensagem" CommandArgument='<%#Eval("con_codigo") %>'
                                                CommandName="Excluir" runat="server">
                                                <img src="/img/icones/delete.png" class="img-olho" width="20" height="20" title="Excluir mensagem"/>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnVerMensagem" CommandArgument='<%#Eval("con_codigo") %>' CommandName="Ver"
                                                runat="server">
                                                <img src="/img/icones/olho.png" class="img-olho" width="30" height="30" title="Visualizar Mensagem"/>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:Literal ID="ltrMensagens" runat="server"></asp:Literal>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- POP-UP CONTATO -->

    <div class="popupMaior" id="contatoAdm">
        <br />

        <div class="popup-header">
            <h3 class="text-secondary text-uppercase mb-0">CONTATO</h3>
            <a href="javascript: fecharContatoAdm();" class="btn-fechar">X</a>
        </div>

        <div class="popup-conteudo">

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/user.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Username:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblUsarioAdm" runat="server" CssClass="form-control" Text=""></asp:Label>

                </div>
            </div>

            <div class="row">
                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblCodigoUsuario" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/tema.png" alt="" width="30" height="30" />

                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Assunto:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblAssuntoAdm" runat="server" CssClass="form-control" Text=""></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/status2.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Mensagem:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblDescricaoAdm" runat="server" CssClass="form-control text-center" Text=""></asp:Label>
                </div>
            </div>

            <p class="text-icones">Resposta:</p>
            <asp:TextBox ID="txtMensagem" TextMode="MultiLine" Height="140" CssClass="textarea form-control" runat="server"></asp:TextBox>

            <br />

            <asp:Button ID="btnCancelarContatoAdm" href="javascript: fecharContatoAdm();" CssClass="btn btn-danger"
                runat="server" Text="Cancelar" />
            <asp:Button ID="btnEnviarContatoAdm" CssClass="btn btn-success btnEspaco" runat="server" Text="Enviar"
                OnClick="btnEnviarContatoAdm_Click" />
        </div>
    </div>

    <!-- POP-UP PERFIL CLIENTE -->

    <div class="popupMaior" id="perfilCliente">
        <br />
        <br />
        <div class="popup-header">
            <h3 class="text-secondary text-uppercase mb-0">PERFIL CLIENTE</h3>
            <a href="javascript: fecharPerfilCliente();"
                class="btn btn-fechar" style="border-color: #ea0e0e; text-transform: uppercase; font-weight: bold; cursor: pointer; font-size: 17px;">x</a>
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
                    <asp:TextBox ID="txtNomePerfilCliente" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/nickname.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Username:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:TextBox ID="txtUsernameCliente" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
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
                    <asp:DropDownList ID="ddlSexoPerfilCliente" CssClass="form-control" Enabled="false" runat="server">
                        <asp:ListItem Value="F">Feminino</asp:ListItem>
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                    </asp:DropDownList>
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
                    <asp:TextBox ID="txtDataPerfilCliente" CssClass="data form-control" runat="server" Enabled="false"></asp:TextBox>
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
                    <asp:TextBox ID="txtEmailPerfilCliente" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
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
                    <asp:TextBox ID="txtCPFPerfilCliente" CssClass="cpf form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
            </div>
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
                    <div class="ddlRemover">
                        <asp:DropDownList ID="ddlUsernames" CssClass="ddlSelect" data-live-search="true" runat="server">
                        </asp:DropDownList>
                    </div>
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
            <asp:Button ID="btnEnviarMsg" OnClick="btnEnviarMsg_Click" CssClass="btn btn-success" runat="server" Text="Enviar" />
        </div>
    </div>

    <!-- POP-UP DELETAR MENSAGEM -->

    <div class="popup" id="excluirMensagem">
        <br />
        <br />
        <div class="popup-header">
            <h3 class="text-secondary text-uppercase mb-0">EXCLUIR MENSAGEM</h3>
            <a href="javascript: fecharExcluirMensagem();" class="btn-fechar">X</a>
        </div>
        <div class="popup-conteudo">

            <p>Você tem certeza que deseja excluir a mensagem? Isso a excluirá permanentemente!</p>
            <asp:Label ID="lblCod" runat="server" Style="color: white;" Text=""></asp:Label>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/tema.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Assunto:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="txtAssuntoDeletar" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/status2.png" alt="" width="30" height="30" />

                </div>

                <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                    <p class="text-icones">Mensagem:</p>
                </div>

                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                    <asp:Label ID="lblTextoDeletar" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <asp:Button ID="btnSimExcluir" CssClass="btn btn-success" OnClick="btnSimExcluir_Click"
                runat="server" Text="Sim" />
            <asp:Button ID="btnNaoExcluir" href="javascript: fecharExcluirTMensagem();" CssClass="btn btn-danger btnEspaco" runat="server" Text="Não" />
        </div>
    </div>
</asp:Content>

