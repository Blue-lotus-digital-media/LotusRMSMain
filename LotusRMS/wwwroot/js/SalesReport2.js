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
}
document.addEventListener("DOMContentLoaded", () => {
   
    

    reportTypeChanged($("#reporttype"));
    GetReport();
});
function reportTypeChanged(me) {
    var value = $(me).find(":selected").val();

    $.ajax({
        type: 'GET',
        url: "/admin/salesreport/GetReportType",
        data: "reportType=" + value,
        success: function (data) {
            $("#selectItem").empty();


            data.forEach(function (item) {
                var option = '<option value="' + item.id + '">' + item.name + '</option>';
                $("#selectItem").append(option);


            })

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}
function GetReport() {
    var startDate = $("#startDate").val();
    var startDateAD = NepaliFunctions.BS2AD(startDate);
    var endDate = $("#endDate").val();

    var endDateAD = NepaliFunctions.BS2AD(endDate);
    if (startDate == "" || endDate == "") {
        alert("Please Select start date and end date first");
        return false;
    }
    var reportType = $("#reporttype").find(":selected").val();
    var reportTypeId = $("#selectItem").find(":selected").val();

    $.ajax({
        type: 'GET',
        url: "/admin/salesreport/GetOrder",
        data: " startdate="+startDateAD+"&endDate="+endDateAD+"&reportType="+reportType+"&id="+reportTypeId,
        success: function (data) {
            console.log(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}