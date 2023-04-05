


function loadData(t) {
   
   
        $.ajax({
            type: "GET",
            url: '/admin/menu/GetCategory/' + $(t).val(),
            success: function (data) {
                $("#mCategory").empty();
                $("#mCategory").append(`<option>--Select a ` + $("#mType option:selected").text()+`'s category -- </option>`);
                $.each(data, function (index, value) {
                    var text1 = '<option value=' + value.value + '>' +
                        value.text + '</option>';
                    $("#mCategory").append(text1);
                });
                $("#mCategory").removeAttr("disabled");
            },
            error: function (result) {
                alert("error occured");

                $("#mCategory").attr("disabled", "true");
            }
        });
    

}

/*ChooseProduct*/

function chooseProduct() {
    $("#ChooseProductForm").submit();
}
function productLoader() {
    $("#selectionModal").modal("toggle");
    loadProduct();
}

function selectProduct(me, id) {
    var $tr = $(me).closest("tr");
    var itemName = $tr.children("td").eq(0).text();
    var itemUnit = $tr.children("td").eq(1).text();

    $("#selectionModal").modal("toggle");
    $("#pItemName").text(itemName);
    $("#pId").val(id);


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
   
    var unit = $("#pUnit").val();
    var table = document.getElementById("productList");
    var i = table.tBodies[0].rows.length;
    var row = table.tBodies[0].insertRow(i);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
  

    cell1.innerHTML = "<input type='text' class='form-control-plaintext' name='ProductList[" + i + "].Product_Name' readonly value='" + itemName + "'><input type='text' name='ProductList[" + i + "].Product_Id' hidden readonly value ='" + id + "'>";
    cell2.innerHTML = "<input type='text' class='form-control-plaintext' name='ProductList[" + i + "].Product_Unit' readonly value='" + unit + "'>";
    cell3.innerHTML = "<input type='text' class='form-control-plaintext' name='ProductList[" + i + "].Product_Quantity' readonly value='" + qty + "'>";
    

    cell4.innerHTML = "<button type='button' class='btn btn-danger' onclick='RemoveProduct(" + i + ")'><i class='bi bi-trash'></i></button>";
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