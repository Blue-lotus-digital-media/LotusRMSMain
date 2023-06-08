
var dataTable;

window.onload = function () {
    LoadData();
}

function LoadData() {
    
    dataTable = $("#tblData").DataTable({
        destroy: true,
        "ajax": {
            "url": "/Admin/inventoryReport/GetAll",
            /*   "success": function (data) {
                   console.log(data);
               } */
        },

        "columns": [
            { data: "product_Name",name:"product_Name" },
            { data: "type" },
            { data: "category" },
            { data: "unit" },
            { data: "stock_Quantity",name:"stock_Quantity" },
            { data: "reorderLevel",name:"reorderLevel" }
        ],
        "createdRow": function (row, data, dataIndex) {
            if (data["stock_Quantity"] < data["reorderLevel"]) {
                $(row).addClass("underLevel");
            } else if (data["stock_Quantity"] == data["reorderLevel"]) {
                $(row).addClass("onLevel");
            }

        },
        searching: true,
       

    });
}