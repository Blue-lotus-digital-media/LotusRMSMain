"use strict";

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();


client.on("OrderComplete", newCall => {
    if ('speechSynthesis' in window) {
        // Speech Synthesis supported 🎉
        var msg = new SpeechSynthesisUtterance();
        msg.text = "Order for "+newCall[1]+" complete for table "+newCall[0];
        window.speechSynthesis.speak(msg);

    } else {
        // Speech Synthesis Not Supported 😣
        alert("Sorry, your browser doesn't support text to speech!");
    }

    
});

client.on("OrderReceived", newCall => {
    var span = document.querySelector(".TypeForm[typeId='" + newCall.type_Id + "'] > div > span");
    span.textContent = newCall.bookCount;
    if (span.classList.contains("d-none")) {
        span.classList.remove("d-none");
    }
    var tableForm = document.querySelector(".TableForm[tableId='" + newCall.table_Id + "']");
    if (tableForm.length > 0) {
        const tableFormBtn = document.querySelector(".TableForm[tableId='" + newCall.table_Id + "']>button");
        console.log(tableFormBtn);
        if (!tableFormBtn.classList.contains("booked")) {
            tableFormBtn.classList.add("booked");
        }
        if (tableFormBtn.classList.contains("tableSelected")) {
            tableFormBtn.click();
        }
    }
});


client.on("CheckoutComplete", newCall => {

    var span = document.querySelector(".TypeForm[typeId='" + newCall.type_Id + "'] > div > span");
    
    span.textContent = newCall.bookCount;
    if (newCall.bookCount==0) {
        span.classList.add("d-none");
    }
    var tableForm = document.querySelector(".TableForm[tableId='" + newCall.table_Id + "']");
    if (tableForm.length > 0) {
        const tableFormBtn = document.querySelector(".TableForm[tableId='" + newCall.table_Id + "']>button");
        if (tableFormBtn.classList.contains("booked")) {
            tableFormBtn.classList.remove("booked");
            tableFormBtn.classList.add("free-table");

        }
        if (tableFormBtn.classList.contains("tableSelected")) {
            tableFormBtn.click();
        }
    }


});

document.addEventListener("DOMContentLoaded", () => {
    client.start();
    console.log(client);

});

function mySearchFunction(input) {
    /*Declare variables*/
    var input, filter, table, tr, td, i, txtValue;
    filter = input.value.toUpperCase();
    table = input.closest("div").nextElementSibling;
    tr = table.getElementsByTagName("tr");


    {/*  // Loop through all table rows, and hide those who don't match the search query*/ }
    for (i = 1; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];


        if (td) {
            txtValue = td.textContent || td.innerText;

            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
function openMenu() {
    var menu = $("#menuDiv").html();
    $("#menuModal").modal("toggle");
    document.getElementById("modalMenuDiv").innerHTML = menu;

}

function SelectQuantity(me) {
    var rate = $(me).find(":selected").attr("Rate");
    $("#menuRate").val(rate);

    console.log(rate);
}