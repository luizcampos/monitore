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

/* ADICIONAR MEMBRO */

function abrirAdicionarMembro() {
    document.getElementById('adicionarMembro').style.display = 'block';
}
function fecharAdicionarMembro() {
    document.getElementById('adicionarMembro').style.display = 'none';
}

/* REMOVER MEMBRO */

function abrirRemoverMembro() {
    document.getElementById('removerMembro').style.display = 'block';
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

/* Fazer Login */

function abrirLogin() {
    document.getElementById('login').style.display = 'block';
}
function fecharLogin() {
    document.getElementById('login').style.display = 'none';
}

/* Esqueceu Senha */

function abrirEsqueceuSenha() {
    document.getElementById('esqueceuSenha').style.display = 'block';
}
function fecharEsqueceuSenha() {
    document.getElementById('esqueceuSenha').style.display = 'none';
}

/* PERFIL */

function abrirPerfil() {
    document.getElementById('perfil').style.display = 'block';
}
function fecharPerfil() {
    document.getElementById('txtNomePerfil').disabled = true;
    document.getElementById('ddlSexoPerfil').disabled = true;
    document.getElementById('txtDataPerfil').disabled = true;
    document.getElementById('txtEmailPerfil').disabled = true;
    document.getElementById('txtCPFPerfil').disabled = true;
    document.getElementById('btnOkPerfil').disabled = true;
    document.getElementById('btnAlterarDados').disabled = false;
    document.getElementById('perfil').style.display = 'none';
}

/* CONTATO USUARIO */

function abrirContato() {
    document.getElementById('contatoUser').style.display = 'block';
}
function fecharContato() {
    document.getElementById('contatoUser').style.display = 'none';
}

/* CRIAR GRUPO */

function abrirCriarGrupo() {
    document.getElementById('criarGrupo').style.display = 'block';
}
function fecharCriarGrupo() {
    document.getElementById('criarGrupo').style.display = 'none';
}

/* CONTATO ADM */

function abrirContatoAdm() {
    document.getElementById('contatoAdm').style.display = 'block';
}
function fecharContatoAdm() {
    document.getElementById('contatoAdm').style.display = 'none';
}

/* ERRO */

function abrirErro() {
    document.getElementById('modalValidacaoErro').style.display = 'block';
}
function fecharErro() {
    document.getElementById('modalValidacaoErro').style.display = 'none';
}

//CRIAÇAO DESSA FUNÇAO 30/03/2018
/* PERFIL USUARIO ADM */

function abrirPerfilUA() {
    document.getElementById('perfilUA').style.display = 'block';
}
function fecharPerfilUA() {
    document.getElementById('perfilUA').style.display = 'none';
}

/* ALTERAR DADOS */

function alterarDados() {
    document.getElementById('teste').disabled = true;
} 