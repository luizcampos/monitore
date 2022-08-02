<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage-Logado.master" AutoEventWireup="true" CodeFile="comprarPlano.aspx.cs" Inherits="paginas_comprarPlano" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section style="background-color: #FFF;">
        <br />
        <br />
        <br />

        <div class="container">

            <div class="row">
                <div class="container text-center col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <h3>Conheça as vantagens de ser Premium</h3>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-4 offset-lg-1">
                    <div class="tabela-planos">
                        <div class="nome-planos">
                            <h3 class="basic">BASIC</h3>
                            <p class="texto-basic">Muitas funcionalidades grátis</p>
                        </div>
                        <div class="preco-basic">
                            <h2 class="preco-basic1">Grátis</h2>
                        </div>

                        <div class="conteudo-planos">
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Criar grupos</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Criar tarefas</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Visualizar progresso</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Gerenciar membro</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Anexação de até 5MB</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-times icone-times"></i>
                                <p class="nomes-planos">Enviar mensagem privada</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-times icone-times"></i>
                                <p class="nomes-planos">Personalizar perfil</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 offset-lg-2">
                    <div class="tabela-planos">
                        <div class="nome-planos1">
                            <h3 class="basic">Premium</h3>
                            <p class="texto-basic">Muito mais interação para você</p>
                        </div>
                        <div class="preco-basic">
                            <h2 class="preco-basic2">R$ 4,99*</h2>

                        </div>

                        <div class="conteudo-planos">
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Criar grupos</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Criar tarefas</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Visualizar progresso</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Gerenciar membro</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Anexação de até 50MB</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Enviar mensagem privada</p>
                            </div>

                            <hr class="hr-cor" />
                            <div class="posicao-conteudo">
                                <i class="fa fa-check icone-check"></i>
                                <p class="nomes-planos">Personalizar perfil</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
            </div>

            <br />
            <br />

            <div class="row text-center">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <h3>Valor: R$4,99/mês</h3>
                    <img src="../img/icones/pagseguro.png" width="130" height="40" />
                    <asp:Button ID="btnComprarPlano" runat="server" CssClass="btn btn-success" Text="Comprar" />
                </div>
            </div>
        </div>
    </section>

</asp:Content>

