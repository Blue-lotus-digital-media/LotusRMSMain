var dataTable;
$(document).ready(function () {
    loadData();
});


function toggleMe(me) {
    var id = $(me).attr("data-id");
    $.ajax({
        type: 'GET',
        url: "/admin/fiscalyear/ActiveChange",
        data: "id=" + id,
        success: function (data) {

            if (data) {
                $(".statusToggle").removeClass('bi-toggle-on').addClass('bi-toggle-off');
                $(me).removeClass("bi-toggle-off").addClass('bi-toggle-on');
            } else {

                alert("Active fiscal year cannot be changed.. ");
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function loadData() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/fiscalyear/GetAll",
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
                data: "name"

            },
            {
                data: "startDateAD",
              
            },
            {
                data: "endDateAD"
            },
            {
                data: "isActive"

            },
            {
                data: "id",

                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/fiscalyear/Update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>    
                              </a>
                              <a href="/Admin/fiscalyear/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash"></i>    
                              </a>
                            </div>`;
                }
            }

        ],
        searching: true,
        rowCallback: function (row, data) {
            $('td:eq(1)', row).html(` BS : ${data['startDateBS']} <br>  AD : ${data['startDateAD']}`);
            $('td:eq(2)', row).html(` BS : ${data['endDateBS']} <br>  AD : ${data['endDateAD']}`);


            if (data["isActive"] == false) {
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
