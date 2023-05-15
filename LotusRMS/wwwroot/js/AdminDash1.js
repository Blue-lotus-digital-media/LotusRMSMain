

function GetTableBooked(me) {
    var type = $(me).attr("type");
    var text = $(me).text();
    console.log(text);

    $.ajax({
        type: 'GET',
        url: "/Admin/Home/GetTableBooked",
        data: "type=" + type,
        success: function (data) {
            var span = document.querySelector("#TableBookedDataType");
            span.innerHTML = text;
            var h6 = document.querySelector("#TableBookedData");
            h6.innerHTML=data.data;

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
document.addEventListener("DOMContentLoaded", () => {
    $("#tableBookedFirst").click();
});