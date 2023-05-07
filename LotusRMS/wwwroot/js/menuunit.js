var dataTable;

$(document).ready(function () {
    loadData();

   

});
function toggleMe(me) {
    var id = $(me).attr("data-id");
    console.log(id);
    $.ajax({
        type: 'GET',
        url: "/admin/menuunit/statuschange",
        data: "id=" + id,
        success: function (data) {

            console.log(data);
            if (data == true) {

                $(me).removeClass("bi-toggle-off").addClass('bi-toggle-on');
            } else {

                $(me).removeClass("bi-toggle-on").addClass('bi-toggle-off');
            }
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });  

   /* if ($(me).hasClass("bi-toggle-on")) {


        $(me).removeClass("bi-toggle-on").addClass('bi-toggle-off');
    } else {
        $(me).removeClass("bi-toggle-off").addClass('bi-toggle-on');
    }*/
}
function loadData() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/menuUnit/GetAll",
           /* "success": function (data) {
                console.log(data);
            }*/
        },
       /* columnDefs: [{
            "defaultContent": "-",
            "targets": "_all"
        }],*/
        "columns": [
            
            {
                data: "unit_Name"
               
            },
            {
                data: "unit_Symbol"
            },
            {
                data: "unit_Description"
            },
            {
                data: "status"
               
            },
            {
                data: "id",

                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/MenuUnit/update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>    
                              </a>
                              <a href="/Admin/MenuUnit/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash"></i>    
                              </a>
                            </div>`;
                }
            }
           
        ],
        searching: false,
        rowCallback: function (row, data) {
            if (data["status"] == false) {
                $('td:eq(3)', row).html(`
<div class="text-center">
<i class= "bi bi-toggle-off statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
</div > `);

            }
            else {
                $('td:eq(3)', row).html(`<div class="text-center">
<i class= "bi bi-toggle-on statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div > `);

            }

        }

    });
}

