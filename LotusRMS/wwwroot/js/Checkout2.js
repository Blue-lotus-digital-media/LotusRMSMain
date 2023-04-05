"use strict";

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

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
    playBeepSound();
});

client.on("CheckoutComplete", newCall => {

    var span = document.querySelector(".TypeForm[typeId='" + newCall.type_Id + "'] > div > span");

    span.textContent = newCall.bookCount;
    if (newCall.bookCount == 0) {
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

$(document).ready(function () {
    client.start();
});


function CheckPaymentMode() {
    var mode = $("#CheckoutMode option:Selected").text();
    console.log(mode);
    if (mode == "Credit") {
        $("#CustomerDueForm").submit();
    } else {
        $("#customer_Id").val("");
        $("#customerDue").empty();
    }
}
function calculateDiscount() {
    var total = $("#GTotal").val();
    var discount = $("#DiscountAmount").val();
    var discountType = $("#discountType option:selected").text();
    console.log(discountType);
    var gt = 0;
    if (discountType != "Rs.") {

        gt = total - discount * total / 100;
    } else {
        gt = total - discount;
    }

    $("#GrandAmount").val(gt);

    calculateRefund();

}
function calculateRefund() {
    var tanderAmount = $("#TenderAmount").val();
    var gt = $("#GrandAmount").val();
    var refund = tanderAmount - gt;
    $("#refundAmount").text(refund);
    $("#refAmt").val(refund);
}
function validateCheckout() {
    console.log($("#CheckoutMode option:selected").text());
    if ($("#CheckoutMode option:selected").text() == "Cash" && parseInt($("#TenderAmount").val()) < parseInt($("#GrandAmount").val())) {
        $("#TenderAmount").css("border", "1px solid red");
        $("#TenderAmount").attr("focused", "true");
        $(".invalid-tooltip").css("display", "block");

        return false;
    } else if ($("#CheckoutMode option:selected").text() == "Credit" && $("#customer_Id").val() == 0) {
        alert("Please select customer before checkout or change payment mode");
    }

    else {
        $("#checkoutSubmitBtn").attr("disabled", "true");
        $("#checkoutForm").submit();
    }
}
function checkValid() {
    if (document.getElementsByClassName("unserved").length > 0) {
        $("#mainBtn").children("button").attr("disabled", "true");
    } else {
        $("#mainBtn").children("button").removeAttr("disabled");
    }
}

function selectCustomer(me) {

    var tr = $(me).closest("tr");
    $(".selectCustomerBtn").removeClass("selectedCustomer");
    $(me).addClass("selectedCustomer");
    var id = $(me).attr("id");
    $("#customer_Id").val(id);
    var name = tr.children("td").children(".name").text().trim();
    $("#customer_Name").val(name);
    var address = tr.children("td").children(".address").text().trim();
    $("#customer_Address").val(address);
    var contact = tr.children("td").children(".contact").text().trim();
    $("#customer_Contact").val(contact);

}
function SwitchTable(me) {
    let btnClicked = me.textContent;
    document.querySelector(".selected-table").textContent = btnClicked;

    let value = me.getAttribute("tableId");
    document.querySelector(".selectedInput").value = value;

}

