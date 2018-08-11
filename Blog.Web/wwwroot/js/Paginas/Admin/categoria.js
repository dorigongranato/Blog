var IdAcao = 0;
var btnExluirRefer = null;

//Inicia a tela
$(document).ready(function () {

    //Lista os dados encontrados
    Listar();

    //Ação do botão Novo
    $("#btnNovo").click(function () {

        IdAcao = 0;
        $("#PartForm").slideDown("slow");
        $("#btnVoltar").show();

        //LimparMensagem();

    });

    //Ação do botão Voltar
    $("#btnVoltar").click(function () {

        $("#PartForm").slideUp("slow");

        $("#btnVoltar").hide();
        LimparMensagem();

        LimparCampos();

    });

    //Ação do botão Salvar
    $("body").on('click', '#btnSalvar', function () {

        Salvar();
        
        LimparMensagem();

    });

    //Ação do botão Editar
    $("body").on('click', '#btnEditar', function () {

        $("#btnVoltar").show();

        var id = $(this).parent().parent().attr("data-id");

        Buscar(id);

        IdAcao = id;

    });

    //Ação do botão Excluir
    $("body").on('click', '#btnExcluir', function () {

        var id = $(this).parent().parent().attr("data-id");
  
        $(this).confirmation(
            { 
                onConfirm: function() {
                    Excluir(id);
                  },
                title:"Confirmar Exlusão?" , 
                btnOkLabel: "&nbsp;Sim", 
                btnCancelLabel: "&nbsp;Não"
            }
        ).click();

    });

});


//Lista os dados da base
function Listar(pagina) {
    
   // MensagemBloqueio("Carregando...");
    $.ajax({
        url: '/Admin/Categoria/Listar',
        data: {
            numPagina : pagina
        },
        statusCode: {
            200: function (content) {

                $("#PartList").html(content);
                $("#PartList").show("slow");
                
               // DesbloquearPagina();

                IniciarElementos();

            },
            99: function (content) {
                DesbloquearPagina();
                ExibirMensagen(content.responseText);
            }
        },
        error: function (req, status, errorObj) {
                DesbloquearPagina();
                ExibirMensagen(errorObj.toString())
        }
    });

}

function Salvar() {

    if(!ValidarFormulario()){
        return;
    }

    MensagemBloqueio("Carregando...");

    $.ajax({
        url: '/Admin/Categoria/Salvar',
        type: "POST",
        data: {
            Nome:      $("#Nome").val(),
            Id:        IdAcao
        },
        statusCode: {
            200: function (content) {

                DesbloquearPagina();
                ExibirMensagen(content);
                $("#PartForm").slideUp("slow");

                LimparCampos();
            
                Listar();

            },
            99: function (content) {
                DesbloquearPagina();
                ExibirMensagen(content.responseText);
            }
        },
        error: function (req, status, errorObj) {
                DesbloquearPagina();
                ExibirMensagen(errorObj.toString())
        }
    });

}

//Busca o dado de acordo com o Id
function Buscar(id) {
    
    MensagemBloqueio("Carregando...");

    $.ajax({
        url: '/Admin/Categoria/Buscar',
        data: { Id: id },
        statusCode: {
            200: function (content) {

                DesbloquearPagina();

                $("#PartForm").slideUp("slow");
                $("#PartForm").html(content);
                $("#PartForm").slideDown("slow");

            },
            99: function (content) {
                DesbloquearPagina();
                ExibirMensagen(content.responseText);
            }
        },
        error: function (req, status, errorObj) {
                DesbloquearPagina();
                ExibirMensagen(errorObj.toString())
        }
    });
}

//Excluir o dado informado
function Excluir(id) {

    $.ajax({
        url: '/Admin/Categoria/Excluir',
        data: {
            Id : id
        },
        statusCode: {
            200: function (content) {
                DesbloquearPagina();
                ExibirMensagen(content);
                Listar();
            },
            99: function (content) {
                DesbloquearPagina();
                ExibirMensagen(content.responseText);
            }
        },
        error: function (req, status, errorObj) {
                DesbloquearPagina();
                ExibirMensagen(errorObj.toString())
        }
    });
            
}


function ValidarFormulario(){

   $( "#frmPagina1" ).validate( {
        rules: {
            Nome: "required"
        },
        messages: {
            Nome: "Informe uma categoria"
        },
        errorElement: "em",
        errorPlacement: function ( error, element ) {
            // Add the `help-block` class to the error element
            error.addClass( "invalid-feedback d-block" );

            // Add `has-feedback` class to the parent div.form-group
            // in order to add icons to inputs

            element.addClass("is-invalid");

            if ( element.prop( "type" ) === "checkbox" ) {
                error.insertAfter( element.parent( "label" ) );
            } else {
                error.insertAfter( element );
            }

            // Add the span element, if doesn't exists, and apply the icon classes to it.
            if ( !element.next( "span" )[ 0 ] ) {
                //$( "<i class='fa fa-edit fa-lg'></i>" ).insertAfter( element );
                //$( "<span class='input-group-append'><div class='input-group-text bg-transparent'><i class='fa fa-search'></i></div></span>" ).insertAfter( $( element ) );
            }
        },
        success: function ( label, element ) {
            // Add the span element, if doesn't exists, and apply the icon classes to it.
            if ( !$( element ).next( "span" )[ 0 ] ) {
                //$( "<span class='b glyphicon glyphicon-ok form-control-feedback'></span>" ).insertAfter( $( element ) );
            }
        },
        highlight: function ( element, errorClass, validClass ) {
            $( element ).addClass( "is-invalid" ).removeClass( "is-valid" );
           // $( element ).next( "span" ).addClass( "c glyphicon-remove" ).removeClass( "glyphicon-ok" );
        },
        unhighlight: function ( element, errorClass, validClass ) {
            $( element ).addClass( "is-valid" ).removeClass( "is-invalid" );
           // $( element ).next( "span" ).addClass( "d glyphicon-ok" ).removeClass( "glyphicon-remove" );
        }
    } );


    return $("#frmPagina1").valid();

}