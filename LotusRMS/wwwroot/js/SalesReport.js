function setDate() {


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
    setDate();
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
   
    var endDate = $("#endDate").val();

    if (startDate == "" || endDate == "") {
        alert("Please Select start date and end date first");
        return false;
    }
    var reportType = $("#reporttype").find(":selected").val();
    var reportTypeId = $("#selectItem").find(":selected").val();
    var startDateAD = NepaliFunctions.BS2AD(startDate);
        
    var endDateAD = NepaliFunctions.BS2AD(endDate);

    loading();
    $.ajax({
        type: 'GET',
        url: "/admin/salesreport/GetOrder",
        data: "startDate="+startDateAD+"&endDate="+endDateAD+"&reportType="+reportType+"&id="+reportTypeId,
        success: function (data) {
            $("#reportHere").empty();
            $("#reportHere").html(data);

            loading();
            console.log(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}
function loading() {
    $("#overlay").toggle();
}