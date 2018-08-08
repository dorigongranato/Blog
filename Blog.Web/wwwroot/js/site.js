
function MensagemBloqueio(Mensagem) {
    $.blockUI({
        message: '<h3 style="color:#ffffff;">' + Mensagem + '</h3>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#ffffff'
        }
    });
}

function IniciarElementos() {

    $('[rel="tooltip"]').tooltip();
}


function DesbloquearPagina() {
    $.unblockUI();
}

function ValidarForm() {

    var forms = document.getElementsByClassName('needs-validation');

    var validation = Array.prototype.filter.call(forms, function(form) {
        if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
        }
        form.classList.add('was-validated');
    });
}

function ExibirMensagen(Mensagem) {

    $("#PartMsg").html(Mensagem);
    $("#PartMsg").show();

}

function LimparMensagem() {
    $("#PartMsg").html("");
}


//Reinicia o formulário
function LimparCampos() {

    $(':input').not(':button, :submit, :reset, :hidden, :checkbox, :radio').val('');
    $(':checkbox, :radio').prop('checked', false);

}