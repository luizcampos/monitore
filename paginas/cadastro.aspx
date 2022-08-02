<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cadastro.aspx.cs" Inherits="paginas_cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/modal.css" rel="stylesheet" />
    <script src="/js/jquery.mask.min.js"></script>

    <script type="text/javascript">

        function abrirErro() {
            document.getElementById('modalValidacaoErro').style.display = 'block';
        }
        function fecharErro() {
            document.getElementById('modalValidacaoErro').style.display = 'none';
        }

        function fecharConfirmacao() {
            document.getElementById('confirmacaoCadastro').style.display = 'none';
        }

        function abrirTermosUso() {
            document.getElementById('termosdeuso').style.display = 'block';
        }

        function fecharTermosUso() {
            document.getElementById('termosdeuso').style.display = 'none';
        }

        $(document).ready(function () {
            $('.data').mask('00/00/0000');
            $('.cpf').mask('000.000.000-00');
        });

        var dataNasci = $('#data').val();
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="cadastro" id="cadastro">
        <div class="container">
            <div class=" text-center">

                <br />
                <h3 class="text-secondary text-uppercase mb-0">CADASTRO</h3>
                <br />

                <div class="tooltip">
                    Hover over me
                    <span class="tooltiptext">Tooltip text</span>
                </div>

                <div class="row">

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/user.png" title="Digite seu nome completo" width="30" height="30" />
                        <p class="text-icones">Nome completo: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtNome" MaxLength="50" placeholder="Ex: João Carlos da Silva" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/data.png" title="Digite a sua data de nascimento" width="30" height="30" />
                        <p class="text-icones">Data de Nascimento: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtData" CssClass="form-control" placeholder="00/00/0000" type="date" runat="server"></asp:TextBox>
                        <%--<asp:TextBox ID="txtData" CssClass="form-control data" placeholder="00/00/0000" runat="server"></asp:TextBox>--%>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/nickname.png" title="Digite um nome de usuário para entrar no sistema" width="30" height="30" />
                        <p class="text-icones">Login: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtLogin" MaxLength="20" placeholder="Ex: joao2234" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/sexo.png" title="Selecione seu sexo" width="30" height="30" />
                        <p class="text-icones">Sexo: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:DropDownList ID="ddlSexo" CssClass="form-control" runat="server">
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Selected="True" Value="F">Feminino</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/email2.png" title="Digite seu email atual e ativo" width="30" height="30" />
                        <p class="text-icones">Email: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtEmail" MaxLength="50" placeholder="Ex: joao@hotmail.com" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/cpf.png" title="Digite seu CPF" width="30" height="30" />
                        <p class="text-icones">CPF: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtCpf" placeholder="000.000.000-00" CssClass="cpf form-control" MaxLength="11" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/senha.png" title="Digite uma senha para entrar no sistema" width="30" height="30" />
                        <p class="text-icones">Senha: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtSenha" MaxLength="30" placeholder="Ex: joao22@86" type="password" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Literal ID="ltrSenha" runat="server"></asp:Literal>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/senha2.png" title="Digite a senha novamente" width="30" height="30" />
                        <p class="text-icones">Repetir senha: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtRepetirSenha" MaxLength="30" placeholder="Repita a senha anterior" type="password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/tipo.png" title="Selecione o seu tipo de usuário no sistema" width="30" height="30" />
                        <p class="text-icones">Tipo de usuário: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:DropDownList ID="ddlTipo" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/pergunta.png" title="Selecione uma pergunta de segurança" width="30" height="30" />
                        <p class="text-icones">Pergunta: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:DropDownList ID="ddlPergunta" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/resposta.png" title="Digite uma resposta para a pergunta de segurança" width="30" height="30" />
                        <p class="text-icones">Resposta: <a style="color:red;font-weight:bolder;">*</a></p>
                        <asp:TextBox ID="txtResposta" MaxLength="20" placeholder="Digite a resposta" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                        <img class="img-icones" src="/img/icones/documento.png" title="É necessário a aceitação dos termos de uso para efetuar o cadastro" width="30" height="30" />
                        <p class="text-icones">Termos de uso: <a style="color:red;font-weight:bolder;">*</a></p>
                        <i><a href="javascript: abrirTermosUso();" style="color: black; font-weight: bold;">Visualizar</a></i><br />
                        <asp:CheckBox ID="ckbTermosUso" AutoPostBack="true"
                            OnCheckedChanged="ckbTermosUso_CheckedChanged" runat="server" Text="Aceito" />
                    </div>
                </div>
                <br />
                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-danger portfolio-modal-dismiss" runat="server" Text="Cancelar" />
                <asp:Button ID="btnCadastrar" OnClick="btnCadastrar_Click" CssClass="btn btn-success" runat="server" Text="Cadastrar" Enabled="false" title="É necessário aceitar os termos de uso para habilitar o botão" />

                <div class="row text-center">
                    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2"></div>
                    <asp:Literal ID="ltrMensagem" runat="server"></asp:Literal>
                    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2"></div>
                </div>
            </div>
        </div>
    </section>

    <!-- CONFIRMAÇÃO CADASTRO -->

    <div class="popupMaior" id="confirmacaoCadastro">
        <div class="popup-conteudo">
            <h3 class="text-secondary text-uppercase mb-0">MENSAGEM DE CONFIRMAÇÃO</h3>
            <br />
            <a href="javascript: fecharConfirmacao();" class="btn-fechar">X</a>

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <img src="/img/icones/descricao.png" class="img-icones2" alt="" width="30" height="30" />
                    <p class="text-icones" style="font-size: 20px;">Os dados estão corretos?</p>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left;">
                    <asp:Label ID="lblDadosCadastro" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <br />

            <asp:Button ID="btnNão" href="javascript: fecharConfirmacao();" CssClass="btn btn-danger"
                runat="server" Text="Não" />
            <asp:Button ID="btnSim" OnClick="btnSim_Click" CssClass="btn btn-success" runat="server" Text="Sim" />

        </div>
    </div>

    <!-- TERMOS DE USO -->

    <div class="popupMaiorTermos" id="termosdeuso">
        <div class="popup-conteudo">
            <h3 class="text-secondary text-uppercase mb-0">TERMOS DE USO</h3>
            <br />
            <a href="javascript: fecharTermosUso();" class="btn-fechar">X</a>

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                    <b>I – ACEITAÇÃO DOS TERMOS:</b>
                    <div style="text-align: justify;">
                        <p>Estes Termos e Usos (doravante denominados “Termos e Usos”) regulamenta o uso do serviço do portal de Internet www.monitoreweb.com.br (doravante, “Sistema web”) que MONITORE oferece aos seus USUÁRIOS.</p>
                        <p>1) Qualquer pessoa, física ou jurídica, doravante nominada USUÁRIO, que pretenda utilizar os serviços da monitoreweb.com.br, deverá aceitar as Cláusulas de Uso e todas as demais políticas e princípios que as regem.</p>
                        <p>2) A ACEITAÇÃO DESTES TERMOS E CONDIÇÕES GERAIS É INDISPENSÁVEL À UTILIZAÇÃO DOS SITES E SERVIÇOS PRESTADOS PELA monitoreweb.com.br. O USUÁRIO deverá ler, certificar-se de haver entendido e aceitar todas as disposições estabelecidas nos Termos e Condições e na Política de Privacidade, para que então seja efetuado com sucesso seu cadastro como USUÁRIO da monitoreweb.com.br.</p>
                    </div>

                    <b>II – DO USUÁRIO:</b>
                    <div style="text-align: justify;">
                        <p>3) O usuário se compromete a fornecer seus dados de forma verdadeira para o seu próprio cadastro.</p>
                        <p>4) O usuário permite que os seus dados sejam cadastrados no banco de dados do sistema “MONITORE”, provido pelo servidor.</p>
                        <p>5) Os administradores do sistema terão acesso a todos os seus dados cadastrados no banco de dados.</p>
                        <p>6) Os administradores só poderão utilizar seus dados a fim de manter o controle de administração do sistema.</p>
                        <p>7) Os administradores em hipótese alguma poderão divulgar, usar ou compartilhar seus dados pessoais dentro do sistema ou para qualquer outra organização sem autorização.</p>
                        <p>8) O MONITORE não se responsabiliza pelo conteúdo e objetivo das informações cadastradas pelos usuários, sendo deles toda a responsabilidade pelo conteúdo.</p>
                        <p>9) O usuário acessará o sistema através de um login e senha, comprometendo-se a não informar a terceiros esses dados, responsabilizando-se integralmente pelo uso que deles seja feito.</p>
                        <p>10) Caso o usuário adquira um plano pago disponibilizado pelo MONITORE, será de sua responsabilidade efetuar o pagamento contínuo.</p>
                        <p>11) Caso o usuário não efetue o pagamento de seu plano no tempo previsto, sua conta será suspensa até o pagamento de seus atrasados.</p>
                        <p>12) É expressamente proibida a tentativa de invasão, cópia ou qualquer outro meio utilizado por hackers, a fim de violar, prejudicar ou comprometer o sistema MONITOREWEB.COM.BR.</p>
                        <p>13) O MONITOREWEB.COM.BR preserva a privacidade dos dados dos USUÁRIOS, e se compromete a revelar os dados pessoais do Usuário apenas devido a um dos seguintes motivos:</p>
                        <p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp• por lei;</p>
                        <p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp•	por meio de uma ordem ou intimação de um órgão, autoridade ou tribunal com poderes para tanto e de jurisdição competente;</p>
                        <p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp• para garantir a segurança dos sistemas, resguardar direitos e prevenir responsabilidades da Abril.</p>

                        <p>14) O MONITOREWEB.COM.BR não se responsabiliza por quaisquer danos sofridos a integridade física ou psicológica dos USUÁRIOS, por meio de qualquer tipo de informação compartilhada pelos mesmos.</p>
                    </div>

                    <b>III – DO SERVIÇO PRESTADO:</b>
                    <div style="text-align: justify;">
                        <p>15) O MONITORE não se compromete em responder mensagens agressivas, invasivas, preconceituosas, religiosas ou políticas enviadas pelos usuários.</p>
                        <p>16) Os usuários não possuem vinculo permanente com algum plano pago oferecido pelo MONITORE, podendo a qualquer momento solicitar o cancelamento.</p>
                        <p>17) Em razão da cláusula 13, os usuários só poderão cancelar o plano adquirido após um pagamento do valor mensal contratado.</p>
                        <p>18) A MONITOREWEB.COM.BR poderá mudar os valores citados neste contrato ou modificar quaisquer de suas clausulas, mediante notificação previa enviada aos USUÁRIOS.</p>
                        <p>19) Caso sejam detectados acessos robóticos e/ou ilegais no sistema, o MONITOREWEB.COM.BR poderá bloquear automaticamente o usuário por tempo indeterminado por medidas de segurança.</p>
                        <p>20) O MONITORE não possui qualquer vínculo com os grupos, tarefas e qualquer outra informação cadastrada pelos seus usuários.</p>
                        <p>21) O MONITORE não altera nenhuma informação pessoal cadastrada pelo usuário, cabendo toda a responsabilidade no próprio usuário pelas suas alterações.</p>
                    </div>

                    <b>IV – DAS DISPOSIÇÕES FINAIS:</b>
                    <div style="text-align: justify;">
                        <p>22) Estes Termos e Condições Gerais não geram nenhum contrato de sociedade, de mandato, franquia ou relação de trabalho entre o monitoreweb.com.br e o USUÁRIO.</p>
                        <p>23) O conteúdo deste documento poderá ser alterado livremente pelo MONITOREWEB.COM.BR, desde que seus usuários recebem uma notificação com a nova versão.</p>
                        <p>24) É vedado ao USUÁRIO a venda de qualquer tipo de informação cadastrada no MONITOREWEB.COM.BR ou até mesmo sua utilização.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

