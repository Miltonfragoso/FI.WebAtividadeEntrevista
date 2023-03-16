
$(document).ready(function () {
    $('#formcadastro').submit(function (e) {
        e.preventdefault();
        $.ajax({
            url: urlpost,
            method: "post",
            data: {
                "nome": $(this).find("#nome").val(),
                "cep": $(this).find("#cep").val(),
                "email": $(this).find("#email").val(),
                "sobrenome": $(this).find("#sobrenome").val(),
                "nacionalidade": $(this).find("#nacionalidade").val(),
                "estado": $(this).find("#estado").val(),
                "cidade": $(this).find("#cidade").val(),
                "logradouro": $(this).find("#logradouro").val(),
                "telefone": $(this).find("#telefone").val(),
                "cpf": $(this).find("#cpf").val()
            },
            error:
            function (r) {
                if (r.status == 400)
                    modaldialog("ocorreu um erro", r.responsejson);
                else if (r.status == 500)
                    modaldialog("ocorreu um erro", "ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                modaldialog("sucesso!", r)
                $("#formcadastro")[0].reset();
            }
        });
    })
    
})

function modaldialog(titulo, texto) {
    var random = math.random().tostring().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
