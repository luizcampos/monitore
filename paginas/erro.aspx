<%@ Page Language="C#" AutoEventWireup="true" CodeFile="erro.aspx.cs" Inherits="paginas_erro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Monitore | Monitorar para avaliar</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Sistema web para monitoramento do andamento de grupos acadêmicos" />
    <meta name="author" content="Monitore" />
    <link rel="icon" href="../img/logotipo/icone.png" type="image/x-icon" />

    <link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/font-awesome/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="/magnific-popup/magnific-popup.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="fundo">

            <div style="margin-top: 15%; text-align: center;">
                <img src="../img/icones/triste.png" width="120" height="120" />

                <h3>Opa, ocorreu algum erro!</h3>
                <h6>Pedimos desculpa pelo transtorno. Agora você pode logar novamente:</h6>
                <br />
                <asp:LinkButton ID="lkbVoltar" OnClick="lkbVoltar_Click" runat="server">
                    Voltar para tela inicial
                </asp:LinkButton>
            </div>
        </section>
    </form>
</body>
</html>
