<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Logado.master" AutoEventWireup="true" CodeFile="grupo.aspx.cs" Inherits="paginas_grupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/tutorial.css" rel="stylesheet" />
    <script src="/js/tutorial.js"></script>
    <script type="text/javascript">

        /* CRIAR TAREFA */

        function abrirCriarTarefa() {
            document.getElementById('criarTarefa').style.display = 'block';
        }
        function fecharCriarTarefa() {
            document.getElementById('criarTarefa').style.display = 'none';
        }

        /* ALTERAR SENHA */

        function abrirAlterarSenha() {
            document.getElementById('alterarSenha').style.display = 'block';
        }
        function fecharAlterarSenha() {
            document.getElementById('alterarSenha').style.display = 'none';
        }

        /* VISUALIZAR TAREFA */

        function abrirTarefa() {
            document.getElementById('visualizarTarefa').style.display = 'block';
        }
        function fecharTarefa() {
            document.getElementById('visualizarTarefa').style.display = 'none';
        }

        /* ADICIONAR MEMBRO TAREFA */

        function abrirAdicionarMembroTarefa() {
            document.getElementById('adicionarMembroTarefa').style.display = 'block';
        }
        function fecharAdicionarMembroTarefa() {
            document.getElementById('adicionarMembroTarefa').style.display = 'none';
        }

        /* ADICIONAR MEMBRO */

        function abrirAdicionarMembro() {
            document.getElementById('adicionarMembro').style.display = 'block';
        }
        function fecharAdicionarMembro() {
            document.getElementById('adicionarMembro').style.display = 'none';
        }

        /* REMOVER MEMBRO */

        function abrirRemoverMembro() {
            document.getElementById('btnProgresso').style.boxShadow = '10px blue';

        }
        function fecharRemoverMembro() {
            document.getElementById('removerMembro').style.display = 'none';
        }

        /* VISUALIZAR MEMBROS */

        function abrirMembros() {
            document.getElementById('membros').style.display = 'block';
        }
        function fechaMembros() {
            document.getElementById('membros').style.display = 'none';
        }

        /* BLOQUEAR MEMBROS */

        function abrirBloquear() {
            document.getElementById('bloquear').style.display = 'block';
        }
        function fecharBloquear() {
            document.getElementById('bloquear').style.display = 'none';
        }

        /* ENCERRAR GRUPO */

        function abrirEncerrar() {
            document.getElementById('encerrarGrupo').style.display = 'block';
        }
        function fecharEncerrar() {
            document.getElementById('encerrarGrupo').style.display = 'none';
        }

        /* ENCERRAR GRUPO */

        function abrirExcluirTarefa() {
            document.getElementById('excluirTarefa').style.display = 'block';
        }
        function fecharExcluirTarefa() {
            document.getElementById('excluirTarefa').style.display = 'none';
        }

        $(document).ready(function () {
            $('.data').mask('00/00/0000');
        });

        /* SAIR DO GRUPO */

        function fecharSairDoGrupo() {
            document.getElementById('confirmacaoSair').style.display = 'none';
        }

        /* CONFIGURAÇÕES DO GRUPO*/

        function abrirConfiguracaoGrupo() {
            document.getElementById('configuracoesGrupo').style.display = 'block';
        }

        function fecharConfiguracaoGrupo() {
            document.getElementById('btnAlterarDados').disabled = false;
            document.getElementById('txtNomeGrupoConfig').disabled = true;
            document.getElementById('dllCor').disabled = true;
            document.getElementById('btnOkConfig').disabled = true;
            document.getElementById('configuracoesGrupo').style.display = 'none';
        }

        function alterarDadosConfig() {
            document.getElementById('btnAlterarDados').disabled = true;
            document.getElementById('txtNomeGrupoConfig').disabled = false;
            document.getElementById('dllCor').disabled = false;
            document.getElementById('btnOkConfig').disabled = false;
        }


        $(document).ready(function () {
            $("#comentarArea").hide();

            $("#content div:nth-child(1)").show();
            $(".abas li:first div").addClass("selecionado");

            $("#abaInfos").click(function () {
                $(".aba").removeClass("selecionado");
                $(this).addClass("selecionado");
                var indice = $(this).parent().index();
                indice++;
                $("#comentarios").hide();
                $("#comentarios2").hide();
                $("#comentarArea").hide();
                $("#infos").show();
                $("#arquivos").hide();
                document.getElementById('arquivos').style.display = 'none';
            });

            $("#abaComentarios").click(function () {
                $(".aba").removeClass("selecionado");
                $(this).addClass("selecionado");
                var indice = $(this).parent().index();
                indice++;
                $("#infos").hide();
                $("#comentarios2").show();
                $("#comentarios").show();
                $("#comentarArea").show();
                $("#arquivos").hide();
                document.getElementById('arquivos').style.display = 'none';
                document.getElementById('comentarios').style.display = 'block';
            });

            $("#abaArquivos").click(function () {
                $(".aba").removeClass("selecionado");
                $(this).addClass("selecionado");
                var indice = $(this).parent().index();
                indice++;
                $("#infos").hide();
                $("#comentarios").hide();
                $("#comentarios2").show();
                $("#comentarArea").hide();
                $("#arquivos").show();
                document.getElementById('arquivos').style.display = 'block';
            });

            $(".aba").hover(
            function () { $(this).addClass("ativa") },
            	function () { $(this).removeClass("ativa") }
            	);
        });

    </script>

    <script src="/jquery/jquery-3.3.1.min.js"></script>
    <script src="/js/jquery.mask.min.js"></script>
    <script src="/sweetalert/sweetalert.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />

    <section class="masthead text-black text-center" style="background-color: #FFF;">

        <div class="espacoGru"></div>

        <div id="homeTela" class="text-left" title="Voltar para tela inicial">
            <a class="btn btn-outline-light btn-social text-center rounded-circle" href="/paginas/index-Logado.aspx">
                <i class="fa fa-home" style="color: #000000; font-size: 28px; margin-top: 3px;"></i>
            </a>
        </div>

        <!-- CABEÇALHO DO GRUPO -->

        <div class="row">
            <asp:Literal ID="ltrTituloGrupo" runat="server"></asp:Literal>
        </div>

        <div class="text-center col-lg-12 col-md-12 col-sm-12">
            <asp:Literal ID="ltrGif" runat="server"></asp:Literal>
            <asp:Button ID="btnTutorial" CssClass="btn btn-success" runat="server" Text="Tutorial"
                Visible="false" OnClick="btnTutorial_Click" />
        </div>

        <!-- BOTÕES DE AÇÕES DO GRUPO -->

        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
            <asp:Button ID="btnProgresso" CssClass="btn efeitoBotao1 gerenciar btn-preto"
                OnClick="btnProgresso_Click" runat="server" Text="Progresso" />

            <asp:Button ID="btnAdicionarMembro" runat="server" Text="Adicionar Membros"
                CssClass="btn btn-success gerenciar efeitoBotao1 btn-amarelo" OnClick="btnAdicionarMembro_Click" />

            <asp:Button ID="btnRemoverMembro" runat="server" Text="Remover Membros"
                CssClass="btn btn-warning gerenciar efeitoBotao1 btn-preto" OnClick="btnRemoverMembro_Click" />

            <asp:Button ID="btnCriarTarefa" runat="server" Text="Criar Tarefa"
                CssClass="btn btn-success gerenciar efeitoBotao1 btn-amarelo" OnClick="btnCriarTarefa_Click" />

            <asp:Button ID="btnSairDoGrupo" runat="server" Text="Sair do grupo"
                CssClass="btn gerenciar efeitoBotao1 btn-preto" OnClick="btnSairDoGrupo_Click" />
        </div>
        <br />
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
            <asp:LinkButton ID="lkbChatGrupo" runat="server" OnClick="lkbChatGrupo_Click">
                <img src="/img/icones/nickname.png" width="25" height="25"
                    title="Chat do grupo"/>
            </asp:LinkButton>

            <asp:LinkButton ID="lkbConfiguracaoGrupo" runat="server" OnClick="lkbConfiguracaoGrupo_Click">
                <img src="/img/icones/configuracao.png" width="25" height="25"
                    title="Configurações do grupo"/>
            </asp:LinkButton>
        </div>

        <div class="container">
            <br />

            <div class="row">
                <!-- NOME DOS PROFESSORES -->

                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-2 ">
                    <div class="scrollMembros">
                        <b>PROFESSORES</b>
                        <asp:Literal ID="ltrNomesProfessores" runat="server"></asp:Literal>
                    </div>
                </div>

                <!-- PORCENTAGEM DO GRUPO -->
                <asp:Literal ID="ltrProgressoGrupo" runat="server"></asp:Literal>

                <!-- NOME DOS MEMBROS -->
                <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2 ">
                    <div class="scrollMembros">
                        <b>ALUNOS</b>
                        <asp:Literal ID="ltrNomesAlunos" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>

            <a id="encerrarGru"></a>

            <%--<div class="row">
                <div class="col-xs-12 col-sm-12 col-md-7 col-lg-8">
                    <img src="../img/icones/resposta.png" width="60" height="60" style="margin-top: 30px;" />
                </div>
            </div>--%>

            <!-- TAREFAS A FAZER -->
            <h3 class="text-center">A FAZER</h3>

            <div class="row">
                <asp:Repeater ID="rptTarefasAFazer" runat="server" OnItemCommand="rptTarefasAFazer_ItemCommand"
                    OnItemCreated="rptTarefasAFazer_ItemCreated">
                    <ItemTemplate>
                        <div class="box-tarefa col-xs-12 col-sm-12 col-md-4 col-lg-3">
                            <div class="box-tarefa-header">
                                <asp:Label ID="lblNomeTarefa" runat="server"><%#Eval("tar_nome") %></asp:Label>
                                <asp:Label ID="lblCodTarefa" runat="server" Text="" Visible="false"><%#Eval("tar_codigo") %></asp:Label>
                            </div>

                            <img src="../img/icones/data.png" width="20" height="20" />
                            <label>PRAZO: </label>
                            <asp:Label ID="lblPrazo" runat="server"><%#Eval("prazo") %></asp:Label>

                            <br />

                            <div class="espacoLateral">
                                <img src="../img/icones/status.png" width="20" height="20" style="float: left;" class="espacoSuperior" />

                                <label style="float: left;" class="espacoSuperior">STATUS: </label>
                                &nbsp;&nbsp;
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" Style="width: 110px; float: left;"
                                    runat="server" CssClass="form-control espacoDdl" Rel='<%#Eval("tar_codigo")%>'>
                                    <asp:ListItem Selected="True">A fazer</asp:ListItem>
                                    <asp:ListItem>Fazendo</asp:ListItem>
                                    <asp:ListItem>Feito</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <br />

                            <div>
                                <asp:LinkButton ID="btnExcluir" CommandArgument='<%#Eval("tar_codigo") %>'
                                    CommandName="Excluir" runat="server">
                                    <img src="/img/icones/delete.png" class="img-olho" width="20" height="20"
                                        title="Excluir tarefa"/>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnVerTarefa" CommandArgument='<%#Eval("tar_codigo") %>' CommandName="Ver"
                                    runat="server">
                                    <img src="/img/icones/olho.png" class="img-olho" width="30" height="30"
                                        title="Visualizar tarefa"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Literal ID="lblTarefasAFazer" runat="server"></asp:Literal>
            </div>

            <br />

            <!-- TAREFAS FAZENDO -->
            <h3 class="text-center">FAZENDO</h3>

            <div class="row">
                <asp:Repeater ID="rptTarefasFazendo" runat="server" OnItemCommand="rptTarefasFazendo_ItemCommand"
                    OnItemCreated="rptTarefasFazendo_ItemCreated">
                    <ItemTemplate>

                        <div class="box-tarefa col-xs-12 col-sm-12 col-md-6 col-lg-3">
                            <div class="box-tarefa-header">
                                <asp:Label ID="lblNomeTarefa" runat="server"><%#Eval("tar_nome") %></asp:Label>
                                <asp:Label ID="lblCodTarefa" runat="server" Text="" Visible="false"><%#Eval("tar_codigo") %></asp:Label>
                            </div>

                            <img src="../img/icones/data.png" width="20" height="20" />
                            <label>PRAZO: </label>
                            <asp:Label ID="lblPrazo" runat="server"><%#Eval("prazo") %></asp:Label>

                            <br />

                            <div class="espacoLateral">
                                <img src="../img/icones/status.png" width="20" height="20" style="float: left;" class="espacoSuperior" />

                                <label style="float: left;" class="espacoSuperior">STATUS: </label>
                                &nbsp;&nbsp;
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" Style="width: 110px; float: left;"
                                    runat="server" CssClass="form-control espacoDdl" Rel='<%#Eval("tar_codigo")%>'>
                                    <asp:ListItem>A fazer</asp:ListItem>
                                    <asp:ListItem Selected="True">Fazendo</asp:ListItem>
                                    <asp:ListItem>Feito</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <br />

                            <div>
                                <asp:LinkButton ID="btnExcluir" CommandArgument='<%#Eval("tar_codigo") %>'
                                    CommandName="Excluir" runat="server">
                                    <img src="/img/icones/delete.png" class="img-olho" width="20" height="20"
                                        title="Excluir tarefa"/>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnVerTarefa" CommandArgument='<%#Eval("tar_codigo") %>' CommandName="Ver"
                                    runat="server">
                                    <img src="/img/icones/olho.png" class="img-olho" width="30" height="30"
                                        title="Visualizar"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Literal ID="lblTarefasFazendo" runat="server"></asp:Literal>
            </div>

            <br />

            <!-- TAREFAS FEITO -->
            <h3 class="text-center">FEITO</h3>

            <div class="row">
                <asp:Repeater ID="rptTarefasFeito" runat="server" OnItemCommand="rptTarefasFeito_ItemCommand"
                    OnItemCreated="rptTarefasFeito_ItemCreated">
                    <ItemTemplate>
                        <div class="box-tarefa col-xs-12 col-sm-12 col-md-6 col-lg-3">
                            <div class="box-tarefa-header">
                                <asp:Label ID="lblNomeTarefa" runat="server"><%#Eval("tar_nome") %></asp:Label>
                                <asp:Label ID="lblCodTarefa" runat="server" Text="" Visible="false"><%#Eval("tar_codigo") %></asp:Label>
                            </div>

                            <img src="../img/icones/data.png" width="20" height="20" class="espacoSuperior" />
                            <label class="espacoSuperior">PRAZO: </label>
                            <asp:Label ID="lblPrazo" runat="server"><%#Eval("prazo") %></asp:Label>

                            <br />

                            <div class="espacoLateral">
                                <img src="../img/icones/status.png" width="20" height="20" style="float: left;" class="espacoSuperior" />

                                <label style="float: left;" class="espacoSuperior">STATUS: </label>
                                &nbsp;&nbsp;
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" Style="width: 110px; float: left;"
                                    runat="server" CssClass="form-control espacoDdl" Rel='<%#Eval("tar_codigo")%>'>
                                    <asp:ListItem>A fazer</asp:ListItem>
                                    <asp:ListItem>Fazendo</asp:ListItem>
                                    <asp:ListItem Selected="True">Feito</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <br />

                            <div>
                                <asp:LinkButton ID="btnExcluir" CommandArgument='<%#Eval("tar_codigo") %>'
                                    CommandName="Excluir" runat="server">
                                    <img src="/img/icones/delete.png" class="img-olho" width="20" height="20"
                                        title="Excluir tarefa"/>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnVerTarefa" CommandArgument='<%#Eval("tar_codigo") %>' CommandName="Ver"
                                    runat="server">
                                    <img src="/img/icones/olho.png" class="img-olho" width="30" height="30"
                                        title="Visualizar"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Literal ID="lblTarefasFeito" runat="server"></asp:Literal>
            </div>

            <div class="text-right" style="margin-top: 15px">
                <asp:Button ID="btnEncerrarGrupo" CssClass="btn btn-danger gerenciar"
                    runat="server" Text="Encerrar grupo" OnClick="btnEncerrarGrupo_Click" />
            </div>
        </div>
    </section>

    <!-- CRIAR TAREFA -->

    <asp:Panel ID="pnlBtnCriar" DefaultButton="btnCriar" runat="server">
        <div class="popupMaior" id="criarTarefa">
            <br />
            <div class="popup-header">
                <h3 class="text-secondary text-uppercase mb-0">CRIAR TAREFA</h3>
                <a href="javascript: fecharCriarTarefa();" class="btn-fechar">X</a>
            </div>
            <div class="popup-conteudo">

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img class="img-icones" src="/img/icones/resposta.png" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Nome da tarefa: <a style="color: red; font-weight: bolder;">*</a></p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-7">
                        <asp:TextBox ID="txtNomeTarefa" placeholder="Ex: Produzir relatório" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img class="img-icones" src="/img/icones/data.png" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Prazo: <a style="color: red; font-weight: bolder;">*</a></p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-7">
                        <asp:TextBox ID="txtPrazo" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img class="img-icones" src="/img/icones/tema.png" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Tema: <a style="color: red; font-weight: bolder;">*</a></p>
                    </div>

                    <div>
                        <asp:DropDownList ID="ddlTema" CssClass="ddlSelect" data-live-search="true" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img class="img-icones" src="/img/icones/descricao.png" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Descrição:</p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-7">
                        <asp:TextBox ID="txtDescricao" TextMode="MultiLine" CssClass="form-control textarea" runat="server"></asp:TextBox>
                    </div>
                </div>
                <p></p>

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img class="img-icones" src="/img/icones/membros.png" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Adicionar membros: <a style="color: red; font-weight: bolder;">*</a></p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-5">
                        <asp:DropDownList ID="ddlMembrosTarefa"
                            CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                        <asp:Button ID="btnAdd" runat="server" Text="+"
                            CssClass="btn btn-success" OnClick="btnAdd_Click" />
                    </div>
                </div>

                <asp:ListBox ID="ltbCodigos" runat="server" Visible="false"></asp:ListBox>

                <p style="font-size: 14px; margin-top: 5px;"><b>OBS:</b> o botão "Criar" só será habilitado após a inclusão de no mínimo um membro.</p>

                <b>
                    <asp:Label ID="lblMembrosTarefa" runat="server" Text="Membros adicionados: "></asp:Label></b>

                <p></p>
                <a href="javascript: fecharCriarTarefa();" class="btn btn-danger">Cancelar</a>


                <asp:Button ID="btnCriar" runat="server" Enabled="false" OnClick="btnCriar_Click" CssClass="btn btn-success btnEspaco" Text="Criar" />

            </div>
        </div>
    </asp:Panel>

    <!-- POP-UP ADICIONAR MEMBRO -->
    <asp:Panel ID="pnlAddMembros" DefaultButton="btnAdicionar" runat="server">
        <div class="popupMenor" id="adicionarMembro">
            <br />
            <br />
            <div class="popup-header">
                <h3 class="text-icones">ADICIONAR MEMBRO:</h3>
                <a href="javascript: fecharAdicionarMembro();" class="btn-fechar">X</a>
            </div>
            <div class="popup-conteudo">

                <div class="row">
                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-2">
                        <img src="../img/icones/adicionar_membro.png" class="img-icones2" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-4 col-md-4 col-lg-4">
                        <p class="text-icones">Username: <a style="color: red; font-weight: bolder;">*</a></p>
                    </div>

                    <div class="col-xs-10 col-sm-7 col-md-7 col-lg-6">
                        <asp:TextBox ID="txtUsername" CssClass="form-control"
                            placeholder="Digite o username" runat="server" OnTextChanged="txtUsername_TextChanged"></asp:TextBox>
                    </div>
                </div>

                <br />

                <a href="javascript: fecharAdicionarMembro();" class="btn btn-danger">Cancelar</a>

                <asp:Button ID="btnAdicionar" OnClick="btnAdicionar_Click" CssClass="btn btn-success btnEspaco" runat="server" Text="Adicionar" />

            </div>
        </div>
    </asp:Panel>

    <!-- POP-UP VISUALIZAR TAREFA -->

    <div class="popupMaior" id="visualizarTarefa">
        <div class="popup-header">
            <asp:Label ID="lblNomeTarefaVer" runat="server"
                CssClass="text-secondary text-uppercase mb-0" Style="font-size: 25px;" Text="NOME DA TAREFA">
            </asp:Label>

            <asp:Label ID="lblNome" runat="server" Visible="false"
                Style="font-size: 16px;" Text="NOME DA TAREFA">
            </asp:Label>

            <div class="text-center" style="margin-left: 75px; padding-top: 50px; width: 80%;">
                <asp:TextBox ID="txtNomeTarefaVer" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
            </div>

            <%--<a href="javascript: fecharTarefa();" class="btn-fechar">X</a>--%>
            <asp:Button ID="btnFecharTarefa" runat="server" Text="X" OnClick="btnFecharTarefa_Click"
                CssClass="btn-fechar" Style="border-color: #ea0e0e; text-transform: uppercase; font-weight: bold; cursor: pointer; font-size: 17px;" />
        </div>

        <asp:Label ID="lblCodTar" runat="server" Text="" Style="color: #FFF;" Visible="false"></asp:Label>
        <br />

        <div id="header">
            <!-- abas -->
            <ul class="abas">
                <li style="display: inline; float: left;">
                    <div id="abaInfos" class="aba" style="float: left;">
                        <span>Informações</span>
                    </div>
                </li>
                <li style="display: inline; float: left;">
                    <div id="abaComentarios" class="aba" style="float: left;">
                        <span>Comentários</span>
                    </div>
                </li>
                <li style="display: inline; float: left;">
                    <div id="abaArquivos" class="aba" style="float: left;">
                        <span>Arquivos</span>
                    </div>
                </li>
            </ul>
        </div>

        <!-- Informações da tarefa -->
        <div class="content" style="background-color: #ebe8e8;">
            <div class="popup-conteudo" id="infos">

                <h4 class="text-icones">Descrição:</h4>

                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblDescricaoVer" runat="server" Text="Será exibida toda
                                        a descrição cadastrada referente a tarefa."></asp:Label>
                        <asp:TextBox ID="txtDescricaoTarefaVer" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                    </div>
                </div>

                <br />

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img src="../img/icones/tema.png" class="img-icones2 btnEspaco" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Tema da tarefa:</p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-7">
                        <asp:TextBox ID="txtTemaTarefaVer" CssClass="form-control"
                            Enabled="false" Text="" runat="server"></asp:TextBox>

                        <div class="arrumaTemaTarefa" style="float: left; left: 0;">
                            <asp:DropDownList ID="ddlTemaTarefaVer" CssClass="ddlSelect"
                                Visible="false" data-live-search="true" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img src="../img/icones/tipo_tarefa.png" class="img-icones2 btnEspaco" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Tipo da tarefa:</p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-7">
                        <asp:TextBox ID="txtTipoTarefaVer" CssClass="form-control"
                            Enabled="false" Text="" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img src="../img/icones/data.png" class="img-icones2 btnEspaco" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Prazo:</p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-7">
                        <asp:TextBox ID="TxtDataTarefaVer" CssClass="form-control"
                            Enabled="false"
                            runat="server"></asp:TextBox>

                        <asp:TextBox ID="txtPrazoTarefaVer" runat="server" type="date"
                            Visible="false" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="row">

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1">
                        <img src="../img/icones/membros.png" class="img-icones2 btnEspaco" alt="" width="30" height="30" />
                    </div>

                    <div class="col-xs-1 col-sm-2 col-md-2 col-lg-4">
                        <p class="text-icones">Membros:</p>
                    </div>

                    <div class="col-xs-10 col-sm-8 col-md-8 col-lg-7">
                        <asp:ListBox ID="lsbMembros" Enabled="false" CssClass="form-control"
                            runat="server"></asp:ListBox>
                    </div>
                </div>

                <br />
                <asp:Button ID="btnAlterarTarefa" CssClass="btn btn-secondary btnEspaco" runat="server" Text="Alterar Dados"
                    OnClick="btnAlterarTarefa_Click" />
                <asp:Button ID="btnSalvar" Visible="false" CssClass="btn btn-success btnEspaco" runat="server"
                    Text="Salvar" OnClick="btnSalvar_Click" />
            </div>
        </div>

        <!-- Comentários -->
        <div class="content container" style="background-color: #ebe8e8;" id="comentarios2">
            <div class="popup-conteudo" id="comentarios">
                <asp:Label ID="lblCodTarefa" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Repeater ID="rptComentariosTarefa" runat="server" OnItemCommand="rptComentariosTarefa_ItemCommand">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-xs-1 col-sm-2 col-md-2 col-lg-1 text-left">
                                <img src="../img/icones/user.png" class="img-icones2 btnEspaco" alt="" width="20" height="20" />
                            </div>

                            <div class="col-xs-1 col-sm-6 col-md-2 col-lg-3 text-left">
                                <asp:Label ID="lblNomeMembro" runat="server"><%#Eval("usu_nome")%></asp:Label>
                            </div>

                            <div id="conteudoComentario" class="col-xs-11 col-sm-12 col-md-8 col-lg-8 textoCom" style="background-color: <%#Eval("com_cor") %>; border: inset 5px <%#Eval("com_borda") %>; color: <%#Eval("com_cor_texto") %>">
                                <asp:Label ID="lblComentario" runat="server"><%#Eval("com_conteudo")%></asp:Label>
                            </div>

                        </div>

                        <div class="row text-right imgLixeiraCom">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <asp:Label ID="lblData" runat="server" Style="font-size: 14px; font-style: italic;"><%#Eval("dataCom")%></asp:Label>
                                <asp:Label ID="lblHorario" runat="server" Style="font-size: 14px; font-style: italic;"><%#Eval("com_horario")%></asp:Label>
                                <asp:LinkButton ID="btnExcluir" CommandArgument='<%#Eval("com_codigo") %>'
                                    CommandName="Excluir" runat="server">
                                    <img src="/img/icones/delete.png" class="" width="15" height="15"
                                        title="Excluir tarefa"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="ltrLegenda" runat="server"></asp:Literal>
            </div>

            <div class="comentar" id="comentarArea">
                <div class="text-left">
                    <b>Comentar:</b>
                </div>

                <div class="row">
                    <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                        <asp:TextBox ID="txtComentario" TextMode="MultiLine" CssClass="textarea form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                        <asp:Button ID="btnEnviar" OnClick="btnEnviar_Click" CssClass="btn btn-success"
                            runat="server" Text="Enviar" />
                    </div>
                </div>
            </div>
            <br />
        </div>

        <!-- Arquivos -->
        <div class="container" id="arquivos" style="background-color: #ebe8e8; top: 0;">
            <div class="row">
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                    <img src="../img/icones/arquivo.png" width="25" height="25" />
                </div>

                <div class="col-xs-2 col-sm-8 col-md-8 col-lg-8">
                    <asp:FileUpload ID="flpArquivo" runat="server" />
                </div>

                <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2 form-group text-center">
                    <asp:LinkButton ID="btnAnexarArquivo" runat="server" CssClass="btn btn-success"
                        OnClick="btnAnexarArquivo_Click"><i class="glyphicon glyphicon-upload"></i>Subir arquivo</asp:LinkButton>
                </div>
            </div>

            <br />

            <div class="row">
                <asp:Repeater ID="rptArquivosTarefa" runat="server" OnItemCommand="rptArquivosTarefa_ItemCommand">
                    <ItemTemplate>
                        <div class="col-md-4 text-center">
                            <img src="<%#Eval("arq_miniatura") %>" class="img-responsive" width="40" height="40" />

                            <br />
                            <a style="font-size: 15px; font-style: italic;"><%#Eval("titulo") %></a>
                            <br />

                            <a style="font-size: 14px;">
                                <%#Eval("arq_dataEnvio") %>
                                <%#Eval("arq_horario") %></a>

                            <asp:LinkButton ID="btnExcluir"
                                CommandName="Excluir" CommandArgument='<%#Eval("arq_codigo")%>' runat="server">
                                <img src="/img/icones/delete.png" class="img-olho" width="20" height="20"
                                        title="Excluir arquivo"/>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lkbDownload"
                                CommandName="Baixar" CommandArgument='<%#Eval("arq_caminho")%>' runat="server">
                                <i class="fa fa-download" title="Baixar arquivo" 
                                    style="font-size:16px; color: #000000;"></i>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <br />
        </div>
    </div>

    <!-- POP-UP REMOVER MEMBRO -->
    <asp:Panel ID="pnlRemoverMembro" DefaultButton="btnRemover" runat="server">
        <div class="popupMenor2" id="removerMembro">
            <br />
            <div class="popup-header">
                <h3 class="text-icones">REMOVER MEMBRO:</h3>
                <a href="javascript: fecharRemoverMembro();" class="btn-fechar">X</a>
            </div>
            <div class="popup-conteudo">
                <p>
                    Você tem certeza que deseja remover um membro?
                <br />
                    <b>OBS: Além do grupo, ele será retirado de todas as tarefas em que pertence.</b>
                </p>

                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 ddlRemover">
                        <asp:DropDownList ID="ddlMembrosRemover" CssClass="ddlSelect" data-live-search="true" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>

                <br />

                <asp:Button ID="btnCancelar3" href="javascript: fecharRemoverMembro();" CssClass="btn btn-danger espacoRem"
                    runat="server" Text="Cancelar" />
                <asp:Button ID="btnRemover" CssClass="btn btn-warning espacoRem2" runat="server" Text="Remover" OnClick="btnRemover_Click" />
            </div>
        </div>
    </asp:Panel>


    <!-- POP-UP ENCERRAR GRUPO -->
    <asp:Panel ID="pnlEncerrar" DefaultButton="btnSim" runat="server">
        <div class="popupMenor" id="encerrarGrupo">
            <br />
            <br />
            <div class="popup-header">
                <h3 class="text-secondary text-uppercase mb-0">ENCERRAR GRUPO</h3>
                <a href="javascript: fecharEncerrar();" class="btn-fechar">X</a>
            </div>
            <div class="popup-conteudo">

                <p>Você tem certeza que deseja encerrar o grupo? Isso o excluirá permanentemente!</p>

                <br />

                <asp:Button ID="btnSim" CssClass="btn btn-success" OnClick="btnSim_Click"
                    runat="server" Text="Sim" />
                <asp:Button ID="btnNao" href="javascript: fecharEncerrar();" CssClass="btn btn-danger btnEspaco" runat="server" Text="Não" />
            </div>
        </div>
    </asp:Panel>

    <!-- POP-UP DELETAR TAREFA -->
    <asp:Panel ID="pnlDeletar" DefaultButton="btnSimExcluir" runat="server">
        <div class="popupMenor" id="excluirTarefa">
            <br />
            <br />
            <div class="popup-header">
                <h3 class="text-secondary text-uppercase mb-0">EXCLUIR TAREFA</h3>
                <a href="javascript: fecharExcluirTarefa();" class="btn-fechar">X</a>
            </div>
            <div class="popup-conteudo">

                <p>
                    Você tem certeza que deseja excluir a tarefa? Isso a excluirá permanentemente!
                <asp:Label ID="lblCodigoTarefa" runat="server" Text="" Visible="false"></asp:Label>
                </p>
                <br />

                <asp:Button ID="btnSimExcluir" CssClass="btn btn-success" OnClick="btnSimExcluir_Click"
                    runat="server" Text="Sim" />
                <asp:Button ID="btnNaoExcluir" href="javascript: fecharExcluirTarefa();" CssClass="btn btn-danger btnEspaco" runat="server" Text="Não" />
            </div>
        </div>
    </asp:Panel>

    <!-- POP-UP SAIR DO GRUPO -->
    <asp:Panel ID="pnlSair" DefaultButton="btnSimSair" runat="server">
        <div class="popupMenor" id="confirmacaoSair">
            <br />
            <br />
            <div class="popup-header">
                <h3 class="text-secondary text-uppercase mb-0">SAIR DO GRUPO</h3>
                <a href="javascript: fecharSairDoGrupo();" class="btn-fechar">X</a>
            </div>
            <div class="popup-conteudo">

                <p>
                    Você tem certeza que deseja sair do grupo?
                </p>
                <br />

                <asp:Button ID="btnSimSair" CssClass="btn btn-success" OnClick="btnSimSair_Click"
                    runat="server" Text="Sim" />
                <asp:Button ID="btnNaoSair" href="javascript: fecharExcluirTarefa();" CssClass="btn btn-danger btnEspaco"
                    runat="server" Text="Não" />
            </div>
        </div>
    </asp:Panel>

    <!-- TUTORIAL - Adicionar membros -->
    <div id="tutorial6" class="addMembros"></div>

    <div id="msgTutorial6" class="msgAddMembros">
        Aqui você pode <b>adicionar</b> professores e outros integrantes ao seu grupo!
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial6();">Próximo</a>
    </div>

    <!-- TUTORIAL - Remover membros -->
    <div id="tutorial7" class="remMembros"></div>

    <div id="msgTutorial7" class="msgRemMembros">
        Aqui você pode <b>remover</b> professores e outros integrantes do seu grupo!
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial7();">Próximo</a>
    </div>

    <!-- TUTORIAL - Criar tarefas -->
    <div id="tutorial8" class="criarTarefa"></div>

    <div id="msgTutorial8" class="msgCriarTarefa">
        Aqui você pode <b>criar</b> tarefas no seu grupo!
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial8();">Próximo</a>
    </div>

    <!-- TUTORIAL - Ver progresso -->
    <div id="tutorial9" class="progresso"></div>

    <div id="msgTutorial9" class="msgProgresso">
        Aqui você pode acompanhar o <b>progresso</b> desse grupo!
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial9();">Próximo</a>
    </div>

    <!-- TUTORIAL - Encerrar grupo -->
    <div id="tutorial10" class="encerrar"></div>

    <div id="msgTutorial10" class="msgEncerrar">
        Aqui você pode <b>encerrar</b> esse grupo. Ninguém mais terá acesso a ele!
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial10();">Próximo</a>
    </div>

    <!-- TUTORIAL - Home -->
    <div id="tutorial11" class="irHome"></div>

    <div id="msgTutorial11" class="msgIrHome">
        Aqui você pode voltar para a <b>tela inicial</b>, onde ficam todos os seus grupos!
        <a class="btn btn-success" style="color: #FFF; border-radius: 100px; margin-top: 10px;"
            href="javascript: tutorial11();">Pronto!</a>
    </div>
</asp:Content>

