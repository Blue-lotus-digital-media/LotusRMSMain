var datatable1;
var datatable2;
/*$(document).ready(function () {
    loadSupplier();
    loadProduct();
});
*/

function chooseSupplier() {
    $("#ChooseSupplierForm").submit();
}
function supplierLoader() {
    $("#selectionModal").modal("toggle");
    loadSupplier();
}
function CheckPaymentMode() {
    var mode = $("#CheckoutMode option:Selected").text();
    console.log(mode);
    if (mode == "Credit") {
        $("#dueSection").css("display","block");
    } else {
        $("#dueAmount").val("");
        $("#dueSection").css("display", "none");
    }
}

function selectSupplier(me,id) {
    var $tr = $(me).closest("tr");
    var supplierName = $tr.children("td").eq(0).text();
    var address = $tr.children("td").eq(1).text();
    var contact = $tr.children("td").eq(2).text();
    var panOrVat = $tr.children("td").eq(3).text();
    $("#supplierName").text(supplierName);
    $("#supplierId").val(id);
    $("#address").text(address);
    $("#contact").text(contact);
    $("#panOrVat").text(panOrVat);

    $("#selectionModal").modal("toggle");
}

window.onload = function () {
    var startbs = document.getElementById("dateBS");
    startbs.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        ndpYearCount: 10,
        readOnlyInput: true,
        language: "english",
        disableDaysAfter: 0,
        onChange: function () {

            ChangeDate(this);
        }
    });
    function ChangeDate() {
        var date = $("#dateBS").val();

        var adDate = NepaliFunctions.BS2AD(date, "YYYY-MM-DD");

        $("#dateAD").val(adDate);

    }
}


function loadSupplier() {
    dataTable1 = $("#supplierData").DataTable({
        "ajax": {
            "url": "/admin/Purchase/GetSupplier",
          
        },
      
        "columns": [

            {
                data: "fullName"

            },
            {
                data: "address",

            },
            {
                data: "contact"
            },
          
            {
                data: "panOrVat"

            },
           
            {
                data: "id",

                render: function (data) {
                    return `<div class="text-center">
                              <a class="btn btn-success text-white" style="cursor:pointer" onclick="selectSupplier(this,'${data}')">
                               Select
                              </a>
                            </div>`;
                }
            }

        ],
        searching: true,
      



    });

}

function chooseProduct() {
    $("#ChooseProductForm").submit();
}
function productLoader() {
    $("#selectionModal").modal("toggle");
    loadProduct();
}
function CalculatePTotal() {
    var qty = $("#pQuantity").val();
    var rate = $("#pRate").val();
    var total = qty * rate;
    $("#pTotal").val(total);
}
function selectProduct(me, id) {
    var $tr = $(me).closest("tr");
    var itemName = $tr.children("td").eq(0).text();
    var itemUnit = $tr.children("td").eq(1).text();

    $("#selectionModal").modal("toggle");
    $("#pItemName").text(itemName);
    $("#pId").val(id);
    $("#pUnit").val(itemUnit);

    $("#productPurchaseModal").modal("toggle");

}
function RemoveProduct(index) {
    var table = document.getElementById("productList");
    var total = table.tBodies[0].rows[index].cells[4].childNodes[0].value;
    console.log(total);
    var gTotal = document.getElementById("GTotal");
    var sum = parseFloat(gTotal.innerHTML) - parseFloat(total);
    console.log(sum);
    table.tBodies[0].deleteRow(index);
    gTotal.innerHTML = sum;
}
function addProduct() {
    var id = $("#pId").val();
    var itemName = $("#pItemName").text();

    var qty = $("#pQuantity").val();
    var rate = $("#pRate").val();
    var total = $("#pTotal").val();
    var unit = $("#pUnit").val();
    var table = document.getElementById("productList");
    var i = table.tBodies[0].rows.length;
    var row = table.tBodies[0].insertRow(i);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
        var cell5 = row.insertCell(4);
        var cell6 = row.insertCell(5);
       
    cell1.innerHTML = "<input type='text' class='form-control-plaintext' name='ProductList[" + i + "].Product_Name' readonly value='" + itemName + "'><input type='text' name='ProductList[" + i +"].Product_Id' hidden readonly value ='" + id + "'>" ;
    cell2.innerHTML = "<input type='text' class='form-control-plaintext' name='ProductList[" + i +"].Product_Unit' readonly value='" + unit + "'>";
    cell3.innerHTML = "<input type='text' class='form-control-plaintext' name='ProductList[" + i +"].Product_Quantity' readonly value='" + qty + "'>";
    cell4.innerHTML = "<input type='text' class='form-control-plaintext' name='ProductList[" + i +"].Product_Rate' readonly value='" +rate+ "'>";
    cell5.innerHTML = "<input type='text' class='form-control-plaintext' readonly value='" +total+ "'>";
    cell6.innerHTML = "<button type='button' class='btn btn-danger' onclick='RemoveProduct("+i+")'><i class='bi bi-trash'></i></button>";
        var gTotal = document.getElementById("GTotal");
    var sum = parseFloat(gTotal.innerHTML) + parseFloat(total);
        console.log(sum);
        gTotal.innerHTML = sum;
    

    $("#productPurchaseModal").modal("toggle");
}

function loadProduct() {
    dataTable2 = $("#productData").DataTable({
        "ajax": {
            "url": "/admin/Purchase/GetProduct",
          },
        "columns": [

            {
                data: "product_Name"
            },
            {
                data: "product_Unit"
            },
            {
                data: "product_Type"
            },
            {
                data: "product_Category"
            },
            {
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                              <a class="btn btn-success text-white" style="cursor:pointer" onclick="selectProduct(this,'${data}')">
                               Select  
                              </a>
                            </div>`;
                }
            }

        ],
        searching: true,

    });
}