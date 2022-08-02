<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="paginas_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="/jquery-easing/jquery.easing.min.js"></script>
    <!-- PARA EFEITO DE ROLAGEM -->

    <style type="text/css">
        #planos {
            height: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div id="inicio"></div>

    <br />

    <section id="cabecalho" class="masthead bg-primary text-black">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 align-items-center">
                    <img src="/img/fundo/grupo2.1.png" class="img-fluid fundo1" />
                </div>
            </div>

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="row">
                <div id="sobre"></div>
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <h1 class="text-uppercase mb-0 text-center">SOBRE</h1>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-12 col-sm-12 col-md-6 col-lg-4">
                    <h3 class="text-left">Quem Somos?</h3>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-8">
                    <p class="text-left">
                        Monitore é um sistema web para monitoramento de grupos acadêmicos.
                               Surgimos a partir de um problema que há em muitas faculdades,
                               <strong>a falta de comunicação e atenção entre alunos e professores</strong>. Nesse
                               caso, a empresa soluciona o problema a partir de estudos que agregam a base dessa falha.
                               <br />
                        <br />
                    </p>
                    <p class="frase-sobre text-center">
                        "Os alunos merecem todo os recursos da instituição acadêmia e
                                   os professores também, de mais rapidez e eficiência na qual o sistema oferece."
                    </p>
                </div>
                <div class="col-12 col-sm-8 col-md-7 col-lg-4 index-imagem">
                    <img class="imagem-produtividade " src="/img/fundo/produtividade.png" />
                </div>
            </div>
            <br />
            <br />

            <div class="row">
                <div class="col-lg-4 offset-lg-6">
                    <h3>Como Funciona?</h3>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-12 col-sm-8 col-md-7 col-lg-4 index-imagem">
                    <img class="imagem-produtividade img-fluid" src="/img/fundo/engrenagem.png" />
                </div>
                <div class="col-lg-8">
                    <p class="text-left ">
                        O sistema funciona a partir de um processo que o usuário ou professor
                                realizar. Após todo o processo, o usuário poderá se beneficiar no sistema.
                    </p>

                    <p class="text-center" style="font-size: 22px;">As funcionalidades do sistema são:</p>
                    <div class="row">
                        <div class="col-lg-5 offset-lg-4">
                            <ul>
                                <li><strong>Criação de grupos;</strong></li>
                                <li><strong>Criação de tarefas;</strong></li>
                                <li><strong>Visualização de progresso;</strong></li>
                                <li><strong>Definição de prazos;</strong></li>
                                <li><strong>Estipulação das notas;</strong></li>
                                <li><strong>E muito mais!</strong></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <br />
            <br />


            <%--CRIAÇAO DO PLANO 30/03/2018--%>
            <div id="planos"></div>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="text-uppercase mb-0 text-center">PLANOS</h1>
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
                                <p class="nomes-planos">Comentários priorizados</p>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-lg-4 offset-lg-2">
                    <div class="tabela-planos">
                        <div class="nome-planos1">
                            <h3 class="basic">PREMIUM</h3>
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
                                <p class="nomes-planos">Gerenciar membros</p>
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
                                <p class="nomes-planos">Comentários priorizados</p>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <br />
            <br />

        </div>
    </section>

</asp:Content>
