$(document).ready(function () {
    $("#CategoryId").attr("disabled", "true");
})


function loadData(t) {
   
   
        $.ajax({
            type: "GET",
            url: '/admin/product/GetCategory/' + $(t).val(),
            success: function (data) {
                $("#pCategory").empty();
                $("#pCategory").append(`<option>--Select a ` + $("#typeId option:selected").text()+`'s category -- </option>`);
                $.each(data, function (index, value) {
                    var text1 = '<option value=' + value.value + '>' +
                        value.text + '</option>';
                    $("#pCategory").append(text1);
                });
                $("#pCategory").removeAttr("disabled");
            },
            error: function (result) {
                alert("error occured");

                $("#pCategory").attr("disabled", "true");
            }
        });
    

}