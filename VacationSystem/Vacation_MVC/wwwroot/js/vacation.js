//var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/vacation/getall' },
        "columns": [
            { data: 'employee.name', "width": "25%" },
            { data: 'period', "width": "15%" },
            { data: 'occupancy', "width": "15%" },
            { data: 'startVacationDate', "width": "10%" },
            { data: 'finishVacationDate', "width": "15%" },
            { data: 'days', "width": "15%" },
            { data: 'observacoes', "width": "15%" },
            { data: 'status', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/vacation/details/${data}" class="btn btn-primary mx-2"> <i class="bi bi-check2-square"></i>Detalhes</a>               
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Tem certeza?',
        text: "Essa ação é irreversível!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Rejeitar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}
