"use strict";

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

client.on("OrderReceived", newCall => {
    loadData();
    playOrderSound();
});

document.addEventListener("DOMContentLoaded", () => {
    console.log("here");
    var body = document.querySelector("body");
    body.classList.add("toggle-sidebar");
    loadData();
    client.start();
  /*  if ('speechSynthesis' in window) {
        // Speech Synthesis supported 🎉
        var msg = new SpeechSynthesisUtterance();
        msg.text = "Good Morning";
        window.speechSynthesis.speak(msg);

    } else {
        // Speech Synthesis Not Supported 😣
        alert("Sorry, your browser doesn't support text to speech!");
    }*/
});
function loadData() {
    console.log("here");
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
function CompleteOrder(orderNo, orderDetailId) {



    $.ajax({
        type: "get",
        url: "/kitchen/home/CompleteKitchen",

        contentType: 'application/json; charset=utf-8',
        data: { orderNo: orderNo, orderDetailId: orderDetailId },
        success: function (data) {
            loadData();
        }
    });
}