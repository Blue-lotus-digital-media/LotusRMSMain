﻿var dataTable;

$(document).ready(function () {
    loadData();

  
});
function toggleMe(me) {
    var id = $(me).attr("data-id");
    $.ajax({
        type: 'GET',
        url: "/admin/menu/statuschange",
        data: "id=" + id,
        success: function (data) {
            
            if (data == true) {

                $(me).removeClass("fa-toggle-off").removeClass("statusToggleOff").addClass('fa-toggle-on').addClass("statusToggleOn");
            } else {

                $(me).removeClass("fa-toggle-on").removeClass("statusToggleOn").addClass('fa-toggle-off').addClass("statusToggleOff");
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
            "url": "/Admin/menu/GetAll",
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
                data: "menu_Image",
                "render": function (data) {
                    if (data == "") {
                        return ``;
                    } else {
                        return `<img src="${data}" width="60px"  height="60px"  style="border-radius:50%; border:0.25rem solid green; margin-right:0"  onclick="showImage()" class="d-flex justify-content-center align-items-center" >`;
                    }
                }


            },
            {
                data: "item_name"
            },
            {
                data: "rate"
            },
            {
                data: "menu_Unit_Name"
            },
            {
                data: "unit_Quantity"
            },
            {
                data: "menu_Type_Name"
            },
            {
                data: "menu_Category_Name"
            },
            {
                data: "orderTo"
            },
            {
                data: "status"
            },
            {
                data: "id",
                render: function (data) {
                    return `<div class="d-flex gap-1">
                               <div class="dropstart">
                                    <button class="eye btn btn-secondary dropstart" data-bs-toggle="dropdown" aria-expanded="false"><i class="fa-solid fa-eye eye"></i></button>

                                            <div class="dropdown-menu">
                                            <div class="container">


                                                  <table class="table table-striped">
                                                 
                                                  <thead>
                                                   <tr>
                                                        
                                                        <th>Qty </th>
                                                        <th>Price</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody>
                                                
                                                   
                                                  </tbody>
                                                  </table>


                                                </div>
                                            </div>
                                </div>
                              <a href="/Admin/menu/update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                 <i class="fa-regular fa-pen-to-square"></i>   
                              </a>
                              <a href="/Admin/menu/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                               <i class="fa-regular fa-trash-can"></i>    
                              </a>
                             
                            </div>`;
                }
            }

        ],
        searching: true,
        rowCallback: function (row, data) {
            if (data["status"] == false) {
                $('td:eq(8)', row).html(`
<div class="text-center">
<i class= "fa-solid fa-toggle-off statusToggle statusToggleOff" onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
</div> `);

            }
            else {
                $('td:eq(8)', row).html(`<div class="text-center">
<i class= "fa-solid fa-toggle-on statusToggle statusToggleOn" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div > `);

            }
            var tdSet = "";
            data.menuDetail.forEach(index => {
                tdSet += " <tr><td>" + index.quantity + "</td ><td>" + index.rate + "</td></tr > ";
            });
            $('td:eq(9)', row).find("table").children("tbody").append(tdSet);

        }

    });
}
function showImage() {



}
