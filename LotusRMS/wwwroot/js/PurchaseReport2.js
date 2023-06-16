

function setDate(startDate, endDate) {
    
    
}


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
    var dateFrom =year + "-" + previousMonth + "-" + date.day;
    var dateTo = date.year + "-" + date.month + "-" + date.day;

    console.log(dateFrom);
    console.log(dateTo);
    startDate.value = dateFrom;
    endDate.value = dateTo;

    GetPurchase();

}


function GetPurchase() {
    var dataTable;
    var startDate = $("#startDate").val();
    var startDateAD = NepaliFunctions.BS2AD(startDate);
    var endDate = $("#endDate").val();

    var endDateAD = NepaliFunctions.BS2AD(endDate);
    if (startDate == "" || endDate == "") {
        alert("Please Select startdate and end date first");
        return false;
    }
    console.log(startDateAD);
    console.log(endDateAD);



    dataTable = $("#tblData").DataTable({
        destroy: true,
        "ajax": {
            "url": "/Admin/purchasereport/getbydaterange?startDate="+startDateAD+"&endDate="+endDateAD,
         /*   "success": function (data) {
                console.log(data);
            } */
        },
        
        "columns": [
            {data: "date",},
            {data: "purchase_Date"},
            {data: "supplier_Name"},
            {data: "bill_No"},
            {data: "bill_Amount"},
            {data: "discount" },
            {data:"paid_Amount"},
            {data: "detailCount"},
            {data: "id",
                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/purchaseReport/Viewdetail/${data}"  style="cursor:pointer">
                                  <button class="eye btn btn-secondary dropstart"><i class="fa-solid fa-eye eye"></i></button>   
                              </a>
                            </div>`;
                }
            }

        ],
        searching: false,
        rowCallback: function (row, data) {
            /*var dateAd = NepaliFunctions.AD2BS(data["date"]);
            console.log(dateAd);
            $('td:eq(0)', row).html(`${dateAd}`);*/
            var date = data["date"].split(" ");
            var dateBS = NepaliFunctions.AD2BS(date[0], "MM/DD/YYYY", "YYYY/MM/DD");
            //var convertedDate = NepaliFunctions.ConvertDateFormat({ year: cd.getFullYear, month: cd.getMonth, day: cd.getDate }, "YYYY/MM/DD");

            var purchaseDateAd = NepaliFunctions.AD2BS(data["purchase_Date"], "MM/DD/YYYY", "YYYY/MM/DD");
            $('td:eq(0)', row).html(`${dateBS} ${date[1]}`); 
            $('td:eq(1)', row).html(`${purchaseDateAd}`); 


        }

    });
}