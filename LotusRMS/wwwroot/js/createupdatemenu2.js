


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