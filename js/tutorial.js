/* Tutorial Index */

function tutorial2() {
    document.getElementById('tutorial1').style.display = 'none';
    document.getElementById('msgTutorial1').style.display = 'none';
    document.getElementById('tutorial2').style.display = 'block';
    document.getElementById('msgTutorial2').style.display = 'block';
    document.getElementById('botaoMenu').click();
}

function tutorial3() {
    document.getElementById('tutorial2').style.display = 'none';
    document.getElementById('msgTutorial2').style.display = 'none';
    document.getElementById('tutorial3').style.display = 'block';
    document.getElementById('msgTutorial3').style.display = 'block';
}

function tutorial4() {
    document.getElementById('tutorial3').style.display = 'none';
    document.getElementById('msgTutorial3').style.display = 'none';
    document.getElementById('tutorial4').style.display = 'block';
    document.getElementById('msgTutorial4').style.display = 'block';
}

function tutorial5() {
    document.getElementById('tutorial4').style.display = 'none';
    document.getElementById('msgTutorial4').style.display = 'none';
    document.getElementById('botaoMenu').click();
}

/* Tutorial Grupo */

function tutorial6() {
    document.getElementById('tutorial6').style.display = 'none';
    document.getElementById('msgTutorial6').style.display = 'none';
    document.getElementById('tutorial7').style.display = 'block';
    document.getElementById('msgTutorial7').style.display = 'block';
}

function tutorial7() {
    document.getElementById('tutorial7').style.display = 'none';
    document.getElementById('msgTutorial7').style.display = 'none';
    document.getElementById('tutorial8').style.display = 'block';
    document.getElementById('msgTutorial8').style.display = 'block';
}

function tutorial8() {
    document.getElementById('tutorial8').style.display = 'none';
    document.getElementById('msgTutorial8').style.display = 'none';
    document.getElementById('tutorial9').style.display = 'block';
    document.getElementById('msgTutorial9').style.display = 'block';
}

function tutorial9() {
    document.getElementById('tutorial9').style.display = 'none';
    document.getElementById('msgTutorial9').style.display = 'none';

    window.location.href = '#encerrarGru';

    document.getElementById('tutorial10').style.display = 'block';
    document.getElementById('msgTutorial10').style.display = 'block';
}

function tutorial10() {
    document.getElementById('tutorial10').style.display = 'none';
    document.getElementById('msgTutorial10').style.display = 'none';
    document.getElementById('tutorial11').style.display = 'block';
    document.getElementById('msgTutorial11').style.display = 'block';
    jQuery('body,html').animate({
        scrollTop: 0
    }, 800);
}

function tutorial11() {
    document.getElementById('tutorial11').style.display = 'none';
    document.getElementById('msgTutorial11').style.display = 'none';
}