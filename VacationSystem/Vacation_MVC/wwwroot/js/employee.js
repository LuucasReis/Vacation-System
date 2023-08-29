//var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/employees/getall' },
        "columns": [
            { data: 'name', "width": "25%" },
            { data: 'email', "width": "15%" },
            { data: 'salary', "width": "10%" },
            { data: 'period', "width": "15%" },
            { data: 'occupancy', "width": "15%" }
        ]
    });
}
