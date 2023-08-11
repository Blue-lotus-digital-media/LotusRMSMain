var dataTable;

$(document).ready(function () {
    loadData();



});
function toggleMe(me) {
    var id = $(me).attr("data-id");
    console.log(id);
    $.ajax({
        type: 'GET',
        url: "/admin/table/statuschange",
        data: "id=" + id,
        success: function (data) {

            console.log(data);
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
        responsive: true,
        rowReorder: {
            selector: 'td:nth-child(2)'
        },
        "ajax": {
            "url": "/Admin/table/GetAll",
             /*"success": function (data) {
                 console.log(data);
             }*/
        },
        /* columnDefs: [{
             "defaultContent": "-",
             "targets": "_all"
         }],*/
        "columns": [

            {
                data: "table_Name"

            },
            {
                data: "table_No"
            },
            {
                data: "no_Of_Chair"
            },
            {
                data: "table_Type_Name"
            },
            {
                data: "isReserved"
            },
            {
                data: "status"

            },
            {
                data: "id",

                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/table/Update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fa-regular fa-pen-to-square"></i>
                              </a>
                              <a href="/Admin/table/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fa-regular fa-trash-can"></i>   
                              </a>
                            </div>`;
                }
            },
            {
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                            <a onclick="downloadQr($(this),event); " data-id="${data}" href="" >Download Qr</a>
<div class="spinner" style="display:none">Downloading                            
<div class="spinner-border text-info" role="status"  >
                <span class="visually-hidden">Loading...</span>
              </div>
              </div>
                            </div>`;
                }
            }

        ],
        searching: true,
        rowCallback: function (row, data) {
            if (data["status"] == false) {
                $('td:eq(5)', row).html(`
<div class="text-center">
<i class= "fa-solid fa-toggle-off statusToggle statusToggleOff"  onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
 </div>`);

            }
            else {
                $('td:eq(5)', row).html(`
<div class="text-center">
<i class= "fa-solid fa-toggle-on statusToggle text-center statusToggleOn" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div> `);

            }

            if (data["isReserved"] == false) {
                $('td:eq(4)', row).html(`

<div class="text-primary text-center">Free</div>
 `);

            }
            else {
                $('td:eq(4)', row).html(`<div class="text-center">

<p class="text-danger ">Reserved</p></div > `);

            }

        }

    });
}

function downloadQr(me, e) {
    var spinner = $(me).siblings(".spinner");
    spinner.css("display", "block");
    $(me).css("display", "none");


    e = e || window.event;
    e.preventDefault();
    var id = $(me).attr("data-id");
    $.ajax({
        type: 'GET',
        url: "/admin/table/downloadQr",
        data: "id=" + id,
        success: function (data) {

            const pageImage = new Image();
            pageImage.src = data.stringImage;//'data:image/png;base64,' + base64string;
            console.log(pageImage.naturalHeight+100);
            pageImage.onload = function () {
                const canvas = document.createElement('canvas');
                canvas.width = pageImage.naturalWidth ;
                canvas.height = pageImage.naturalHeight + 300;
                canvas.fillStyle = "white";

                const ctx = canvas.getContext('2d');
                ctx.fillStyle = "#000ff0";

                ctx.fillRect(0, 0, 1180, 1180);

                ctx.fillStyle = "#0ff000";
                ctx.imageSmoothingEnabled = false;
                ctx.drawImage(pageImage, 0, 0);

               
                ctx.font = "80px Calibri";
                ctx.fillStyle = "#0ff000";
                ctx.textBaseline = 'middle';
                ctx.textAlign = "center";

                ctx.fillText(data.tableName, 590, 1120);
                


               
                saveScreenshot(canvas,data.tableName);
            }
            $(me).css("display", "block");

            spinner.css("display", "none");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(me).css("display", "block");

            spinner.css("display", "none");
            alert(errorThrown);
        }
    });

}
function saveScreenshot(canvas,fileName) {
    const link = document.createElement('a');
    link.download = fileName + '.png';
    console.log(canvas)
    canvas.toBlob(function (blob) {
       
        link.href = URL.createObjectURL(blob);
        link.click();
    });
};
