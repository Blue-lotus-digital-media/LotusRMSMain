function getUnitDivision(t) {
    var unit = document.getElementById("mUnit");
    var x = unit.value;
    var text = unit.options[unit.selectedIndex].text;
    
     $.ajax({
        type: "GET",
         url: '/admin/menu/getunitdivision?UnitId=' + x,
        success: function (data) {
           
            var quantity = document.getElementById('unitDivision_0');
            quantity.innerHTML="";
           
            data.unitDivision.forEach((item) =>{
                var option = document.createElement("option");
                option.text = item.title + "(" + item.value + " " + text + ") ";
                console.log(item.id);
                option.value = item.id;
                quantity.append(option);
            });
            var fieldset = document.getElementById('fieldset_0');



            $(".wrappers").empty();
            $(".wrappers").append(fieldset);
        }
    });
}


function loadData(t) {


    $.ajax({
        type: "GET",
        url: '/admin/menu/GetCategory/' + $(t).val(),
        success: function (data) {
            $("#mCategory").empty();
            $("#mCategory").append(`<option>--Select a ` + $("#mType option:selected").text() + `'s category -- </option>`);
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

    var itemUnitDiv = $tr.children("td").eq(1).children("div");
    var itemUnit = itemUnitDiv.text().trim();
    console.log(itemUnit);
    var itemUnitId = itemUnitDiv.attr("data-unitId");

     $("#selectionModal").modal("toggle");
    $("#pItemName").text(itemName);
    $("#pUnitId").val(itemUnitId);
    $("#pUnit").val(itemUnit);
    $("#pId").val(id);



    $("#productPurchaseModal").modal("toggle");

}
function RemoveProduct(index) {
    var table = document.getElementById("productList");

    table.tBodies[0].deleteRow(index);
    
    var rowLength = table.tBodies[0].rows.length;
    for (i = 0; i < rowLength; i++) {
        var row = table.tBodies[0].rows[i];

        var cell0 = row.cells[0];
        var cell1 = row.cells[1];
        var cell2 = row.cells[2];
        var cell3 = row.cells[3];

        cell0.childNodes[0].setAttribute("name", "Menu_Incredian[" + i + "].Product_Name");
        cell0.childNodes[1].setAttribute("name", "Menu_Incredian[" + i + "].Id");
        cell1.childNodes[0].setAttribute("name", "Menu_Incredian[" + i + "].Product_Unit_Id");
        cell1.childNodes[1].setAttribute("name", "Menu_Incredian[" + i + "].Product_Unit");
        cell2.childNodes[0].setAttribute("name", "Menu_Incredian[" + i + "].Quantity");
        cell3.childNodes[0].setAttribute("onclick", "RemoveProduct(" + i + ")");

      

    }

    
}
function addProduct() {
    var id = $("#pId").val();
    var itemName = $("#pItemName").text();

    var qty = $("#pQuantity").val();

    var unit = $("#pUnit").val();
    var unitId = $("#pUnitId").val();
    var table = document.getElementById("productList");
    var i = table.tBodies[0].rows.length;
    var row = table.tBodies[0].insertRow(i);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);


    cell1.innerHTML = "<input type='text' class='form-control-plaintext' name='Menu_Incredian[" + i + "].Product_Name' readonly value='" + itemName + "'><input type='text' name='Menu_Incredian[" + i + "].Product_Id' hidden readonly value ='" + id + "'>";
    cell2.innerHTML = "<input type='text' class='form-control-plaintext' name='Menu_Incredian[" + i + "].Product_Unit_Id' readonly value='" + unitId + "' hidden><input type='text' class='form-control-plaintext' name='Menu_Incredian[" + i + "].Product_Unit' readonly value='" + unit + "'>";
    cell3.innerHTML = "<input type='text' class='form-control-plaintext' name='Menu_Incredian[" + i + "].Quantity' readonly value='" + qty + "'>";


    cell4.innerHTML = "<button type='button' class='btn btn-danger' onclick='RemoveProduct(" + i + ")'><i class='bi bi-trash'></i></button>";
 


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
        searching: false,
        rowCallback: function (row, data) {
            $('td:eq(1)', row).html(`
            <div class="text-center" data-unitId='${data['product_Unit_Id']}'>

            ${data['product_Unit']}
            </div>
            `);

        }

    });
}



$(document).ready(function () {
    $("#mCategory").attr("disabled", "true");

  
   
    $(document).on('change',' .unitDefault', function () {
        $('.unitDefault').not(this).prop('checked', false);
        $('.unitDefault').not(this).prop('value', false);
        $(this).prop("value", true);


    });

    $(document).on('click', '.remove_button', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        $('#fieldset_' + id).remove();


        var fieldset = document.getElementsByClassName("fieldset");
        
        for (var i = 0; i < fieldset.length; i++) {
            var field = fieldset[i];

            field.setAttribute("id", "fieldset_" + i);
            var div = field.children[0].children[0].children;

            var quantity = div[0];
            var rate = div[1];
            var defaults = div[2];
            var button = div[3];
            quantity.setAttribute("id", "unitDivision_" + i);
            quantity.setAttribute("name", "MenuUnit["+i+"].Quantity");
            rate.setAttribute("name", "MenuUnit["+i+"].Rate");
            defaults.setAttribute("name", "MenuUnit[" + i + "].IsDefault");
            button.setAttribute("data-id", i);



            console.log(field);
        }

    });
});
function addDivision(e) {

    var wrapper = $(".wrappers");
    var x = document.getElementsByClassName("fieldset").length;
    var max_fields = document.getElementById('unitDivision_0').length;
    /*e.preventDefault();*/
    if (x < max_fields) {
       
        $(wrapper).append(
            '<div class="fieldset" id="fieldset_' + x + '">' +
            '  <div class="fields">' +
            '    <div class="d-flex mb-2">' +
            '       <select name="MenuDetail[' + x + '].Quantity" class="unitDivision form-control" id="unitDivision_'+x+'">'+
                                       ' </select >'+
            '      <input type="number" class="form-control me-2" placeholder="rate" name="MenuDetail[' + x + '].Rate" />' +
            '      <input type="radio" class="me-2 unitDefault" name="MenuDetail[' + x + '].IsDefault" />' +
            '      <button type="button" class="remove_button btn btn-danger" data-id="' + x + '"><i class="fa-solid fa-minus"></i></button>' +
            '    </div>' +
            '  </div>' +
            '</div>');

        var quantity = document.getElementById('unitDivision_0');
        document.getElementById("unitDivision_" + x).innerHTML=quantity.innerHTML;
    }
}
            


