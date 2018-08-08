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

        LimparMensagem();

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


        //$("#PartForm").slideUp("slow");
        //$("#PartForm").html(content);
        //$("#PartForm").slideDown("slow");
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
    
    MensagemBloqueio("Carregando...");
    $.ajax({
        url: '/Admin/Categoria/Listar',
        data: {
            numPagina : pagina
        },
        statusCode: {
            200: function (content) {

                $("#PartList").html(content);
                $("#PartList").show("slow");
                
                DesbloquearPagina();

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

    ValidarForm();

    MensagemBloqueio("Carregando...");

    var Form = new FormData();

    Form.append("Nome",      $("#Nome").val());
    Form.append("Id",        IdAcao);

    $.ajax({
        url: '/Admin/Categoria/Salvar',
        type: "POST",
        contentType: false,
        processData: false,
        data: Form,
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