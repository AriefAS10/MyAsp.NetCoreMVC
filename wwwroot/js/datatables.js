$(document).ready(function () {
    $('#table_id').DataTable({
        columnDefs: [{
            orderable: false, targets: 6,
        }],
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        searching: true
    });
});