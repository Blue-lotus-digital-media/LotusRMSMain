let withDrawAmount = document.getElementById('withDraw');
let closingAmount = document.getElementById('closing');
let remaining = document.getElementById('remaining');



withdrawDisableEnable();
remaining.textContent = closingAmount.textContent - withDrawAmount.value;

document.addEventListener("DOMContentLoaded", () => {
    getTotal();
});
function withdrawDisableEnable() {
    if (withDrawAmount <= 0 || (remaining.textContent) < 0) {
        document.getElementById('btnWithdraw').setAttribute('disabled', '');
    } else {
        document.getElementById('btnWithdraw').removeAttribute('disabled');
    }

}


function changeValue() {
   
    remaining.textContent = closingAmount.textContent - withDrawAmount.value;
    withdrawDisableEnable();
}


function WithDrawGalla() {


    ;
    $.ajax({
        type: 'POST',
        url: "/Galla/WithDrawGalla",
        data: "withDrawAmount=" + withDrawAmount.value,
        success: function (data) {
            document.getElementById("closing").textContent = data.closing_Balance;
            document.getElementById("closingBalance").textContent = data.closing_Balance;
            var table = document.getElementById("dataTable");
            let row = table.insertRow(1);
            let cell0 = row.insertCell(0);
            let cell1 = row.insertCell(1);
            let cell2 = row.insertCell(2);
            let cell3 = row.insertCell(3);
            let cell4 = row.insertCell(4);
            var time = data.galla_Details[0].timeST.split(" ");

            cell0.innerText = time[1] + " " + time[2];
            cell1.innerText = data.galla_Details[0].title;
            cell2.innerText = data.galla_Details[0].deposit;
            cell3.innerText = data.galla_Details[0].withdrawl;
            cell4.innerText = data.galla_Details[0].balance;
            remaining.textContent = data.closing_Balance;
            getTotal();
            withDrawAmount.value = 0;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
    function getTotal() {
        let deposit = 0;
        let withdrawl = 0;
        let balance = document.getElementById("closingBalance").textContent;
        $('#dataTable tbody tr').each(function () {
            deposit += parseFloat($(this).children("td")[2].innerText);
            withdrawl += parseFloat($(this).children("td")[3].innerText);

        });
        var table = document.getElementById("dataTable");

        if (table.getElementsByTagName("tfoot").length > 0) {
            table.getElementsByTagName("tfoot")[0].remove();

        }
        

        let footer = table.createTFoot();
        let row = footer.insertRow(-1);
        let cell0 = row.insertCell(0);
        let cell1 = row.insertCell(1);
        let cell2 = row.insertCell(2);
        let cell3 = row.insertCell(3);
        let cell4 = row.insertCell(4);

        
        cell1.innerText = "Total";
        cell2.innerText = deposit;
        cell3.innerText = withdrawl;
        cell4.innerText = balance;
    }


