<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ajuda.aspx.cs" Inherits="paginas_ajuda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/js/ajuda.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />
    <section style="background-color: #FFFFFF;">
        <div class="container">

            <div class="row">
                <div class="text-center col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <iframe width="560" height="315" src="https://www.youtube.com/embed/pr1YByfts58" 
                        frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="text-center col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <h3>PERGUNTAS FREQUENTES</h3>
                </div>
            </div>
            <br />

            <!-- AJUDA 1 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda1();" id="up1" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda1();" id="down1">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como realizar o cadastro?
            </div>

            <div class="row ajuda" id="ajuda1" style="padding: 20px; border-color: #000000;">
                No menu superior, há a opção "Registre-se":
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoRegistre.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, você será redirecionado para uma tela com todos os campos necessários para o seu cadastro no sistema.
                <br />
                <b>IMPORTANTE: </b>você deve aceitar o termos de uso, selecionado a caixa de seleção, para que o botão "Cadastre" seja habilitado.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoAceito.png" class="text-center" />
                </div>
            </div>
            <br />

            <!-- AJUDA 2 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda2();" id="up2" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda2();" id="down2">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como entrar na minha conta?
            </div>

            <div class="row ajuda" id="ajuda2" style="padding: 20px; border-color: #000000;">
                No menu superior, há a opção "Login":
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoLogin.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up será aberta para a inserção de seus username e senha, já cadastrados.
            </div>
            <br />

            <!-- AJUDA 3 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda3();" id="up3" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda3();" id="down3">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como criar um grupo?
            </div>

            <div class="row ajuda" id="ajuda3" style="padding: 20px; border-color: #000000;">
                Ao logar no sistema, é exibido na tela inicial um botão "+".
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoCriarGrupo.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up será aberta para a escolha do nome e cor do grupo.
            </div>

            <br />

            <!-- AJUDA 4 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda4();" id="up4" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda4();" id="down4">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como criar tarefas?
            </div>

            <div class="row ajuda" id="ajuda4" style="padding: 20px; border-color: #000000;">
                Ao entrar em grupo, é possível encontrar o botão "Criar Tarefa" habilitado, caso você seja um <b>Aluno-líder</b>.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoCriarTarefa.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up será aberta para a inserção das informações da tarefa.
            </div>
            <br />

            <!-- AJUDA 5 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda5();" id="up5" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda5();" id="down5">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como adicionar membros?
            </div>

            <div class="row ajuda" id="ajuda5" style="padding: 20px; border-color: #000000;">
                Ao entrar em grupo, é possível encontrar o botão "Adicionar Membros" habilitado, caso você seja um <b>Aluno-líder</b>.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoAddMembros.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up será aberta para a inserção do username de um usuário desejado.
            </div>
            <br />

            <!-- AJUDA 6 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda6();" id="up6" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda6();" id="down6">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como remover membros?
            </div>

            <div class="row ajuda" id="ajuda6" style="padding: 20px; border-color: #000000;">
                Ao entrar em grupo, é possível encontrar o botão "Remover Membros" habilitado, caso você seja um <b>Aluno-líder</b>.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoRemoverMembros.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up será aberta para a escolha de um membro desejado, que poderá ser removido.
            </div>
            <br />

            <!-- AJUDA 7 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda7();" id="up7" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda7();" id="down7">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como sair de um grupo?
            </div>

            <div class="row ajuda" id="ajuda7" style="padding: 20px; border-color: #000000;">
                Ao entrar em grupo, é possível encontrar o botão "Sair do Grupo" habilitado, caso você seja um <b>Aluno-integrante</b>.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoSairDoGrupo.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up será aberta para confirmação da ação.<br />
                Um <b>Aluno-líder</b> não poderá sair do grupo, apenas encerrá-lo.
            </div>
            <br />

            <!-- AJUDA 8 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda8();" id="up8" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda8();" id="down8">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Onde vejo o progresso do grupo?
            </div>

            <div class="row ajuda" id="ajuda8" style="padding: 20px; border-color: #000000;">
                Ao entrar em grupo, é possível encontrar o botão "Progresso" habilitado.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoProgresso.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, você será redirecionado para uma nova tela, que contém as informações do andamento de determinado grupo.
            </div>
            <br />

            <!-- AJUDA 9 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda9();" id="up9" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda9();" id="down9">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como visualizar o perfil?
            </div>

            <div class="row ajuda" id="ajuda9" style="padding: 20px; border-color: #000000;">
                No menu superior, ao logar, será exibido seu nome de usuário.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoPerfil.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up contendo seus dados principais será exibida.
            </div>
            <br />

            <!-- AJUDA 10 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda10();" id="up10" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda10();" id="down10">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como alterar a senha?
            </div>

            <div class="row ajuda" id="ajuda10" style="padding: 20px; border-color: #000000;">
                No menu superior, ao logar, será exibido seu nome de usuário.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoPerfil.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up contendo seus dados principais será exibida.<br />
                Um link "Alterar senha" será exibido também, caso selecionado uma outra pop-up destinada para a alteração da senha será exibida.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoAlterarSenha.png" class="text-center" />
                </div>
            </div>
            <br />

            <!-- AJUDA 11 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda11();" id="up11" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda11();" id="down11">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como alterar dados do perfil?
            </div>

            <div class="row ajuda" id="ajuda11" style="padding: 20px; border-color: #000000;">
                No menu superior, ao logar, será exibido seu nome de usuário.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoPerfil.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up contendo seus dados principais será exibida.<br />
                O botão "Alterar dados" poderá ser selecionado para habilitar todos os campos para alteração.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoAlterarDados.png" class="text-center" />
                </div>

                Para salvar essas alterações, será necessário que você clique no botão "Ok", que estará habilitado.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoOkPerfil.png" class="text-center" />
                </div>
            </div>
            <br />

            <!-- AJUDA 12 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda12();" id="up12" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda12();" id="down12">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como ver uma tarefa?
            </div>

            <div class="row ajuda" id="ajuda12" style="padding: 20px; border-color: #000000;">
                Ao entrar em um grupo, você poderá ver as tarefas separadas pelo seus status.
                <br />
                Cada tarefa possui um botão para visualização, representado por uma imagem de um olho.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoVerTarefa.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up contendo as informações principais da tarefa será exibida.
            </div>
            <br />

            <!-- AJUDA 13 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda13();" id="up13" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda13();" id="down13">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como excluir uma tarefa?
            </div>

            <div class="row ajuda" id="ajuda13" style="padding: 20px; border-color: #000000;">
                Ao entrar em um grupo, você poderá ver as tarefas separadas pelo seus status.
                <br />
                Cada tarefa possui um botão para exclusão, representado por uma imagem de uma lixeira.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoExcluirTarefa.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up será exibida para confirmação dessa tarefa.<br />
                <b>Lembre-se: </b>apenas o Aluno-líder poderá excluir tarefas.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoSimExcluirTarefa.png" class="text-center" />
                </div>
            </div>
            <br />

            <!-- AJUDA 14 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda14();" id="up14" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda14();" id="down14">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como contatar os administradores?
            </div>

            <div class="row ajuda" id="ajuda14" style="padding: 20px; border-color: #000000;">
                Ao logar no sistema, a opção "Contato" poderá ser exibida no menu superior.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoContatoAdm.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up contendo os campos necessários para a mensagem será exibida.
                <br />
                Para enviar, é só clicar no botão "Enviar".
                <br />
                <b>OBS: </b>sua resposta virá via e-mail.
            </div>
            <br />

            <!-- AJUDA 15 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda15();" id="up15" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda15();" id="down15">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Como adquirir o plano premium?
            </div>

            <div class="row ajuda" id="ajuda15" style="padding: 20px; border-color: #000000;">
                No menu superior, ao logar, será exibido seu nome de usuário.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoPerfil.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, uma pop-up contendo seus dados principais será exibida.<br />
                O botão "Adquirir Premium" poderá ser selecionado.
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoAdquirirPremium.png" class="text-center" />
                </div>

                Caso seja selecionado, você será redirecionado para uma outra tela responsável pelo processo de compra.
            </div>
            <br />

            <!-- AJUDA 16 -->
            <div class="form-control">
                <a href="javascript: fecharAjuda16();" id="up16" class="ajuda">
                    <i style="float: right; color: black;" class="fa fa-sort-up"></i>
                </a>

                <a href="javascript: abrirAjuda16();" id="down16">
                    <i style="float: right; color: black;" class="fa fa-sort-down"></i>
                </a>
                Esqueci a minha senha. Como recuperar?
            </div>

            <div class="row ajuda" id="ajuda16" style="padding: 20px; border-color: #000000;">
                No menu superior, há a opção "Login":
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoLogin.png" class="text-center" />
                </div>

                <br />
                Quando selecionado, existe a opção "Esqueceu a senha?", que ao ser selecionada abrirá outra pop-up para 
                que você responda corretamente a pergunta secreta, escolhida e respondida no seu cadastro.<br />
                <br />
                <br />

                <div class="text-center" style="border-color: #000000; border: 6px solid;">
                    <img src="/img/ajuda/botaoEsqueceuSenha.png" class="text-center" />
                </div>
            </div>
            <br />
        </div>
    </section>
</asp:Content>

