$(document).ready(function () {
    $(document).on('click', '.remove_button', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        $('#fieldset_' + id).remove();


        var fieldset = document.getElementsByClassName("fieldset");
        for (var i = 0; i < fieldset.length; i++) {
            var field = fieldset[i];

            field.setAttribute("id", "fieldset_" + i);
            var div = field.children[0].children[0].children;
            console.log(div);
            var title = div[0];
            var value = div[1];
            var button = div[2];
            title.setAttribute("name", "Unit_Division[" + i + "].Title");
            value.setAttribute("name", "Unit_Division[" + i + "].Value");
            button.setAttribute("data-id", i);
        }

    });
})
function addDivision() {

    var wrapper = $(".wrappers");
    var x = document.getElementsByClassName("fieldset").length;
    var max_fields = 5;
    console.log();
    /*e.preventDefault();*/
    if (x < max_fields) {

        $(wrapper).append(
            '<div class="fieldset" id="fieldset_' + x + '">' +
            '  <div class="fields">' +
            '    <div class="d-flex mb-2">' +
            '       <input name="Unit_Division[' + x + '].Title" class="unitDivision form-control" id="unitDivision_' + x + '" placeholder="Title eg. Half">' +
           
            '      <input type="number" class="form-control me-2" placeholder="Value eg. 0.5" name="Unit_Division[' + x + '].Value" />' +
            '      <button type="button" class="remove_button btn btn-danger" data-id="' + x + '"><i class="fa-solid fa-minus"></i></button>' +
            '    </div>' +
            '  </div>' +
            '</div>');

        var quantity = document.getElementById('unitDivision_0');
        document.getElementById("unitDivision_" + x).innerHTML = quantity.innerHTML;
    }
}
