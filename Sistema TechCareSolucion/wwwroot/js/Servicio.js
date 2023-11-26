var tableMantenimiento;

$(document).ready(function () {


    tableMantenimiento = $("#Serviciotable").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "pageLength": 10,
        "ajax": {
            url: "/Servicio/GetServicios/",
            method: "POST",
            'data': function (data) {

            }
        },
        "columns": [
            {
                "orderable": true,
                "data": "id"
            },
            {
                "orderable": true,
                "data": "tipo"
            },
            {
                "orderable": true,
                "data": "tecnico" 
            },
            {
                "orderable": true,
                "data": "cliente" 
            },
            {
                "orderable": true,
                "data": "estado" 
            },
            {
                "orderable": true,
                "data": "inicio" 
            },
            {
                "orderable": true,
                "data": "fin" 
            },
            {
                "orderable": true,
                "data": "total" 
            },
            {
                "orderable": false,
                "render": function (data, type, row) {
                    var id = row.idCostoServicio;
                    var buttons = `<a class="btn btn-light" href="/Servicio/WorkFlow/${row.id}">Ver servicio</a>`;

                    return buttons;
                }
            }
        ],
        "columnDefs": [
            { "className": "text-center", "targets": "_all" }
        ],
        "language": {


        },

    });

    $("#BuscarServicio").keyup(function () {
        var texto = $(this).val();
        tableMantenimiento.search(texto).draw();
    });
});