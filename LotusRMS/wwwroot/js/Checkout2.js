"use strict";

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

client.on("OrderReceived", newCall => {
    var form = $(".TableForm[tableid='" + newCall + "']");
    if (form.length > 0) {
        form[0].children[0].classList.remove("tableUnBooked");
        form[0].children[0].classList.add("tableBooked");
    } else {
        checkTableType(newCall)
    }
    playBeepSound();
});
function checkTableType(tableId) {
    $.ajax({
      
            type: 'GET',
        url: "/order/home/ReturnTableType",
        data:"id="+tableId,
        success: function (data) {
            var form = $(".TypeForm[typeId='" + data.typeId + "']");
            if (form.length > 0) {
                form[0].children[0].classList.remove("typeUnBooked");
                form[0].children[0].classList.add("typeBooked");
                form[0].children[1].innerHTML = data.count;
            }
        },
        error: function (res) {
            console.log(res);
        }
    });
}

$(document).ready(function () {
    client.start();
});


