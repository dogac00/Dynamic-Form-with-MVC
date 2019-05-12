// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Contains(first, second) {
    if (first.indexOf(second) != -1) {
        return true;
    }
    else {
        return false;
    }
}

$('.search-bar').keyup(function () {
    var textToSearch = $('.search-bar').val().toLowerCase();
    $('.search-table tr').each(function () {
        var formName = $(this).children('td:first').text().trim().toLowerCase();
        if (Contains(formName, textToSearch)) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
});

$($('.add')).click(function () {
    var isRequired = $('#fieldIsRequired').is(':checked');
    var dataType = $('#fieldDataType').find(':selected').val();
    var localFieldName = $('#fieldName').val();

    if (localFieldName == "") {
        alert("Field name can not be null.");
        return;
    }

    var fieldName = '<input type="hidden" name="fieldNames" class="form-control" value="' + localFieldName + '">' + localFieldName + '';
    var fieldIsRequired, fieldDataType;
    if (isRequired) {
        fieldIsRequired = '<input type="hidden" name="fieldIsRequireds" class="form-check-input" value="true" checked>Required';
    }
    else {
        fieldIsRequired = '<input type="hidden" name="fieldIsRequireds" class="form-check-input" value="false">Not Required';
    }

    if (dataType == "String") {
        fieldDataType = '<input type="hidden" name="fieldDataTypes" class="form-check-input" value="string">String';
    }
    else {
        fieldDataType = '<input type="hidden" name="fieldDataTypes" class="form-check-input" value="number">Number';
    }

    $($('.form-elements')).append('<tr><td>' + fieldName + '</td><td>' + fieldIsRequired + '</td><td>' + fieldDataType + '</td><td><a class="btn btn-danger text-white remove">Remove</a></td></tr>');
});

$($('.form-elements')).on('click', '.remove', function () {
    $(this).parent().parent().remove();
});