var dataTable;

$(document).ready(function () {
    loadData();
});
function toggleMe(me) {
    var id = $(me).attr("data-id");
    $.ajax({
        type: 'GET',
        url: "/admin/billSetting/Activechange",
        data: "id=" + id,
        success: function (data) {
            if (data) {
                $(".statusToggle").removeClass('bi-toggle-on').addClass('bi-toggle-off');
                $(me).removeClass("bi-toggle-off").addClass('bi-toggle-on');
            } else {

                alert("Active bill setting cannot be changed.. ");
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
            "url": "/Admin/BillSetting/GetAll",
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
                data: "billTitle"

            },
            {
                data: "billPrefix"
            },
            {
                data: "billAddress"
            },
            {
                data: "billNote"

            },
            {
                data: "isPanOrVat"

            },{
                data: "isPhone"

            },{
                data: "isFiscalYear"

            },
            {
                data: "isActive"
            },
            {
                data: "id",

                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/billSetting/Update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>    
                              </a>
                              <a href="/Admin/billSetting/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash"></i>    
                              </a>
                            </div>`;
                }
            }

        ],
        searching: false,
        rowCallback: function (row, data) {


            if (data["isPhone"]) {
                $('td:eq(4)', row).html(`

                  <i class= "bi bi-eye-fill">

                `);
            }
            else {
                $('td:eq(4)', row).html(`

                  <i class= "bi bi-eye-slash-fill">

                `);
            }
            if (data["isPanOrVat"]) {
                $('td:eq(5)', row).html(`

                  <i class= "bi bi-eye-fill">

                `);
            }
            else {
                $('td:eq(5)', row).html(`

                  <i class= "bi bi-eye-slash-fill">

                `);
            }
             if (data["isFiscalYear"]) {
                $('td:eq(6)', row).html(`

                  <i class= "bi bi-eye-fill">

                `);
             }
             else {
                $('td:eq(6)', row).html(`

                  <i class= "bi bi-eye-slash-fill">

                `);
            }


            if (!data["isActive"]) {

                $('td:eq(7)', row).html(`
<div class="text-center">
<i class= "bi bi-toggle-off statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
</div > `);

            }
            else {
                $('td:eq(7)', row).html(`<div class="text-center">
<i class= "bi bi-toggle-on statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div > `);

            }

        }

    });
}
