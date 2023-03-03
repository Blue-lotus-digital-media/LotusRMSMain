$(document).ready(function () {
    $("#CategoryId").attr("disabled", "true");
})


function loadData(t) {
   
   
        $.ajax({
            type: "GET",
            url: '/admin/product/GetCategory/' + $(t).val(),
            success: function (data) {
                $("#CategoryId").empty();
                $("#CategoryId").append(`<option>--Select a ` + $("#typeId option:selected").text()+`'s category -- </option>`);
                $.each(data, function (index, value) {
                    var text1 = '<option value=' + value.value + '>' +
                        value.text + '</option>';
                    $("#CategoryId").append(text1);
                });
                $("#CategoryId").removeAttr("disabled");
            },
            error: function (result) {
                alert("error occured");

                $("#CategoryId").attr("disabled", "true");
            }
        });
    

}