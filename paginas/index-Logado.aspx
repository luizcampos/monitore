<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Logado.master" AutoEventWireup="true" CodeFile="index-Logado.aspx.cs" Inherits="paginas_index_Logado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/tbl-index-logado.css" rel="stylesheet" />
    <link href="/css/tutorial.css" rel="stylesheet" />
    <script src="/js/tutorial.js"></script>

    <script type="text/javascript">
        /* CRIAR GRUPO */

        function abrirCriarGrupo() {
            document.getElementById('criarGrupo').style.display = 'block';
        }
        function fecharCriarGrupo() {
            document.getElementById('criarGrupo').style.display = 'none';
        }

        /* INFORMAÇÕES GRUPO */

        function fecharInfosGrupo() {
            document.getElementById('infosGrupo').style.display = 'none';
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <section class="masthead text-black text-center" style="background-color: #FFF;">
        <div class="container">
            <div class="row">
                <div class="col-lg-2" id="meusGrupos">
                    <h3>Meus Grupos</h3>
                </div>

                <div class="col-lg-8">
                    <a href="javascript: abrirCriarGrupo()">
                        <i class="fa fa-plus-circle iconCriarGrupo"></i>
                    </a>
                </div>
            </div>
            <br />

            <div class="row">
                <asp:Repeater ID="rptLista" runat="server" OnItemCommand="rptLista_ItemCommand">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-4 col-sm-6">
                            <div class="blocoGrupo" style="background-color: <%#Eval("gru_cor") %>; border-color: <%#Eval("gru_cor") %>;">
                                <div class='scrollNomeGrupo' style="overflow-y: auto;">
                                    <h4 class="NomeGrupo" style="color: white;"><%#Eval("gru_nome") %></h4>
                                </div>
                                <hr />

                                <div class="progress" title="<%#Eval("tarefas")%>% das tarefas concluídas">
                                    <div class="progress-bar bg-success progress-bar-striped" style="width: <%#Eval("tarefas") %>%; background-color: #FFF; font-size: 14px;"><%#Eval("tarefas") %>%</div>
                                </div>

                                <i class="fa fa-calendar iconeCriarGrupo" title="Data de criação"></i>
                                <div class="textoGrupo">
                                    <asp:Label ID="lblPrazo" runat="server"><%#Eval("DataCriacao") %></asp:Label>
                                </div>
                                <i class="fa fa-users iconeCriarGrupo" title="Quantidade de alunos"></i>
                                <div class="textoGrupo">
                                    <asp:Label ID="lblIntegrantes" runat="server"><%#Eval("totalAl") %> Alunos</asp:Label>
                                </div>
                                <i class="fa fa-user-secret iconeCriarGrupo" title="Quantidade de professores"></i>
                                <div class="textoGrupo">
                                    <asp:Label ID="lblProfessores" runat="server"><%#Eval("totalPr") %> Professores</asp:Label>
                                </div>
                                <br />
                                <asp:Button ID="btnEntrarGrupos" CommandArgument='<%#Eval("gru_chave") %>' CommandName="Entrar"
                                    runat="server" CssClass="btn btn-success" Text="Entrar" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Literal ID="lblGrupos" runat="server"></asp:Literal><br />
                <br />
                <div class="text-center col-lg-12 col-md-12 col-sm-12">
                    <asp:Literal ID="ltrGif" runat="server"></asp:Literal>
                    <asp:Button ID="btnTutorial" runat="server" Text="Tutorial" CssClass="btn btn-success espaceTuto" Visible="false"
                        OnClick="btnTutorial_Click" />
                </div>
            </div>
        </div>
    </section>

    <!-- CRIAR GRUPO -->

    <div class="popup" id="criarGrupo">
        <br />
        <div class="popup-header">
            <h3 class="text-secondary text-uppercase mb-0">CRIAR GRUPO</h3>
            <a href="javascript: fecharCriarGrupo();" class="btn-fechar">X</a>
        </div>
        <div class="popup-conteudo">

            <div class="row">
                <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="/img/icones/tipo.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-2 col-sm-2 col-md-2 col-lg-3">
                    <p class="text-icones">Nome do Grupo*:</p>
                </div>

                <div class="col-xs-8 col-sm-8 col-md-8 col-lg-7">
                    <asp:TextBox ID="txtNomeGrupo" placeholder="Ex: Estrutura de Design" CssClass="form-control" runat="server"></asp:TextBox>
                    <p style="font-size: 12px;"><i><b>Sugestões:</b> Trabalho de Ética, TCC...</i></p>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                    <img class="img-icones" src="../img/icones/descricao.png" alt="" width="30" height="30" />
                </div>

                <div class="col-xs-2 col-sm-2 col-md-2 col-lg-3">
                    <p class="text-icones">Cor:</p>
                </div>

                <div class="col-xs-8 col-sm-8 col-md-8 col-lg-7">
                    <asp:DropDownList ID="dllCor" runat="server" CssClass="form-control" AutoPostBack="true"
                        OnSelectedIndexChanged="dllCor_SelectedIndexChanged">
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
            <br />
            <br />

            <asp:Button ID="btnFechasGrupo" href="javascript: fecharCriarGrupo();" runat="server" CssClass="btn btn-danger" Text="Cancelar" />
            <asp:Button ID="btnCriarGrupo" runat="server" CssClass="btn btn-success btnEspaco" Text="Criar" OnClick="btnCriarGrupo_Click" />
        </div>
    </div>

    <!-- POP-UP INFORMAÇÕES DO GRUPO -->

    <div class="popupMaiorTermos" id="infosGrupo">
        <br />
        <br />
        <div class="popup-header">
            <h3 class="text-secondary text-uppercase mb-0">INFORMAÇÕES IMPORTANTES</h3>
        </div>

        <div class="popup-conteudo">

            <h5>Você é o <b>aluno-líder</b> desse grupo!
                <img src='/img/icones/coroa.png' style='margin-bottom: 4px;' width='20' height='20' title='Aluno-líder' />
            </h5>
            <p>
                Você será a única pessoa capaz de <i>adicionar</i> e <i>remover membros</i>, <i>criar tarefa</i>, <i>excluir tarefa</i> e <i>encerrar grupo</i>.
            </p>
            <br />
            <h5>Alunos-integrantes e professores
                <img src='/img/icones/membros.png' style='margin-bottom: 4px;' width='20' height='20' title='Aluno-líder' />
            </h5>
            <p>
                Os membros que forem adicionados serão capazes de <i>alterar o status das tarefas</i> e <i>sair do grupo</i>.
            </p>
            <p>
                <b>Todos</b> poderão visualizar tarefas e o progresso do grupo.
            </p>
            <br />
            <br />
            <p>
                Aproveite! O Monitore agradece seu comprometimento.
            </p>
            <br />

            <asp:Button ID="btnNaoEntendi" CssClass="btn btn-danger" OnClick="btnNaoEntendi_Click"
                runat="server" Text="Não entendi" />
            <a id="btnEntendi" class="btn btn-success btnEspaco" href="javascript: fecharInfosGrupo();">Entendi!
            </a>
        </div>
    </div>

    <!-- TUTORIAL - Criar grupo -->
    <div id="tutorial1" class="criarGrupo"></div>

    <div id="msgTutorial1" class="msgCriarGrupo">
        Aqui você pode criar um grupo!
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial2();">Próximo</a>
    </div>

    <!-- TUTORIAL - Ver perfil -->
    <div id="tutorial2" class="verPerfil"></div>

    <div id="msgTutorial2" class="msgVerPerfil">
        Aqui você pode ver o seu perfil, alterar seus dados e adquirir o premium!<br />
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial3();">Próximo</a>
    </div>

    <!-- TUTORIAL - Contato -->
    <div id="tutorial3" class="verContato"></div>

    <div id="msgTutorial3" class="msgVerContato">
        Aqui você pode mandar uma mensagem aos desenvolvedores!<br />
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial4();">Próximo</a>
    </div>

    <!-- TUTORIAL - Logout -->
    <div id="tutorial4" class="verLogout"></div>

    <div id="msgTutorial4" class="msgVerLogout">
        Aqui você pode sair do sistema!<br />
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial5();">Pronto!</a>
    </div>

</asp:Content>

