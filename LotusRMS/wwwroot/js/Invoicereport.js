
document.addEventListener("DOMContentLoaded", () => {


});

window.onload = function () {


    var startDate = document.getElementById("startDate");

    startDate.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        ndpYearCount: 10,
        readOnlyInput: true,
        language: "english",
        disableDaysAfter: 0,

    });


    var endDate = document.getElementById("endDate");
    endDate.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        ndpYearCount: 10,
        readOnlyInput: true,
        language: "english",
        disableDaysAfter: 0,

    });

    var date = NepaliFunctions.GetCurrentBsDate();
    var year = date.year;
    var previousMonth = date.month - 1;
    if (previousMonth == 0) {
        previousMonth = 12;
        year = year - 1;
    }
    var dateFrom = year + "-" + previousMonth + "-" + date.day;
    var dateTo = date.year + "-" + date.month + "-" + date.day;

  
    startDate.value = dateFrom;
    endDate.value = dateTo;

    GetInvoice();

}


function GetInvoice() {
    var dataTable;
    var startDate = $("#startDate").val();
    var startDateAD = NepaliFunctions.BS2AD(startDate);
    var endDate = $("#endDate").val();

    var endDateAD = NepaliFunctions.BS2AD(endDate);
    if (startDate == "" || endDate == "") {
        alert("Please Select startdate and end date first");
        return false;
    }
 


    dataTable = $("#tblData").DataTable({
        destroy: true,
        "ajax": {
            "url": "/Admin/InvoiceReport/getbydaterange?startDate=" + startDateAD + "&endDate=" + endDateAD,
              /* "success": function (data) {
                   console.log(data);
               } */
        },

        "columns": [
            { data: "date", },
            { data: "invoice_No" },
            { data: "customerName" },
            { data: "total" },
            { data: "discount" },
            { data: "discountType" },
            { data: "paid_Amount" },
            { data: "paymentMode" },
            {
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/invoiceReport/Viewdetail/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fa fa-eye"></i>    
                              </a>
                            </div>`;
                }
            }

        ],
        searching: true,
        rowCallback: function (row, data) {
            /*//*var dateAd = NepaliFunctions.AD2BS(data["date"]);
            console.log(dateAd);
            $('td:eq(0)', row).html(`${dateAd}`);/*/
            var date = data["date"].split(" ");
            var dateBS = NepaliFunctions.AD2BS(date[0], "MM/DD/YYYY", "YYYY/MM/DD");
            //var convertedDate = NepaliFunctions.ConvertDateFormat({ year: cd.getFullYear, month: cd.getMonth, day: cd.getDate }, "YYYY/MM/DD");

            //var purchaseDateAd = NepaliFunctions.AD2BS(data["purchase_Date"], "MM/DD/YYYY", "YYYY/MM/DD");
            $('td:eq(0)', row).html(`${dateBS} ${date[1]} ${date[2]}`);
           // $('td:eq(1)', row).html(`${purchaseDateAd}`);


        }

    });
}