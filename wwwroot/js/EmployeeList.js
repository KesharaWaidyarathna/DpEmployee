var dataTable;

$(document).ready(function () {
    loadDataTable();
    datePickerId.max = new Date().toISOString().split("T")[0];
    getAge();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/employees/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "code", "width": "20%" },
            { "data": "firstName", "width": "20%" },
            { "data": "address1", "width": "20%" },
            {
                "data": "code",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Employees/Upsert?code=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a href="/Employees/View?code=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            View
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/Employees/Delete?code='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

function getAge() {
    var today = new Date();
    var birthDate = new Date($('#datePickerId').val());
    console.log(birthDate);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
   document.getElementById("age").value = age;
}