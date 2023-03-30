"use strict";

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

client.on("OrderReceived", newCall => {
    loadData();
    playOrderSound();
});

document.addEventListener("DOMContentLoaded", () => {
    var body = document.querySelector("body");
    body.classList.add("toggle-sidebar");
    loadData();
    client.start();
});
function loadData() {

    $.ajax({
        url: "/kitchen/home/getData",
        success: function (data) {
            var orderSection = $(".row#orderSection");
            orderSection[0].innerHTML = data;

        },
        error: function (res) {

        }
    })
} 
