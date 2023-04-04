"use strict";

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

client.on("OrderComplete", newCall => {
    console.log(newCall);
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

document.addEventListener("DOMContentLoaded", () => {
    client.start();
   

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