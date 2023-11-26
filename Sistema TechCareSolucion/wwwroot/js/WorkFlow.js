var tableRepuestos;

$(document).ready(function () {
    GetComentarios();

    $('#Comentario').keydown(function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            enviarComentario();
        }
    });

    tableRepuestos = $("#ReparacionesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": false,
        info: false,
        ordering: false,
        paging: false,
        "pageLength": 100,
        "ajax": {
            url: "/Servicio/GetRepuestosByServicio?idServicio=" + $('#Servicio_Id').val(),
            method: "POST",
            'data': function (data) {

            }
        },
        "columns": [
            {
                "orderable": true,
                "data": "nombre"
            },
            {
                "orderable": true,
                "data": "cantidad"
            },
            {
                "orderable": true,
                "data": "costo"
            },
            {
                "orderable": true,
                "data": "venta"
            }
        ],
        "columnDefs": [
            { "className": "text-center", "targets": "_all" }
        ],
        "language": {


        },

    });
});

function enviarComentario() {
    var comentario = $('#Comentario').val();
    var Servicio = $('#Servicio_Id').val();

    var fecha = new Date();

    $.ajax({
        type: 'POST',
        url: '/Servicio/AgregarComentario',
        data: {
            Comentario: comentario,
            Fecha: fecha.toISOString(),
            Idservicio: Servicio
        },
        success: function (response) {
            $('#Comentario').val("");
            GetComentarios();
        },
        error: function (error) {
            $('#Comentario').val("");
            console.error(error);
        }
    });
}

function GetComentarios() {
    var Servicio = $('#Servicio_Id').val();

    $.ajax({
        type: 'GET',
        url: '/Servicio/GetComentarios',
        data: { idServicio: Servicio },
        success: function (comentarios) {
            $('#chat-messages').empty();

            $.each(comentarios, function (index, comentario) {
                var fecha = new Date(comentario.fechaComentario);
                var formattedFecha = fecha.toLocaleString('es-ES', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
                var message = `<div class="message"><p class="fecha">${formattedFecha} ${comentario.comentario}</p></div>`;
                $('#chat-messages').append(message);
            });
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function enviarReparacion() {
    var nombre = $('#Nombre').val();
    var cantidad = $('#Cantidad').val();
    var costo = $('#Costo').val();
    var venta = $('#Precio').val();

    $.ajax({
        type: 'POST',
        url: '/Servicio/AgregarRepuesto?idServicio=' + $('#Servicio_Id').val(),
        data: {
            Nombre: nombre,
            Cantidad: cantidad,
            Costo: costo,
            Venta: venta
        },
        success: function (response) {
            tableRepuestos.ajax.reload();
            $('#Nombre').val("");
            $('#Cantidad').val("");
            $('#Costo').val("");
            $('#Precio').val("");
        },
        error: function (error) {
            console.error(error);
        }
    });
}
function abrirNuevaVentana() {
    var idServicioPublic = document.getElementById('Servicio_IdServicioPublic').value;

    var url = '/Servicio/Public/' + idServicioPublic;

    window.open(url);
}