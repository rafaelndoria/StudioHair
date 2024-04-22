setTimeout(function () {
    var mensagemSucesso = document.getElementById('mensagem-sucesso');
    if (mensagemSucesso) {
        mensagemSucesso.style.display = 'none';
    }

    var mensagemErro = document.getElementById('mensagem-erro');
    if (mensagemErro) {
        mensagemErro.style.display = 'none';
    }
}, 5000);
