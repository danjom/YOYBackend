function childCategoriesSuccessfullyRetrieved(data, status) {

    if (status == "success") {

        var options = "<option>Seleccionar...</option>";

        $.each(data, function (i, item) {
            options += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";
        });

        $("#CategoryId").prop("disabled", false);
        $('#CategoryId').html(options);

    }
    else {
        operationFailed();
    }
}

function loadChildCategories() {
    var parentCategoryId = $('#ParentCategoryId option:selected').val();

    if (parentCategoryId != null && parentCategoryId.length > 0) {
        submitDataAjax("POST", "/Category/ChildCategories", JSON.stringify({ parentCategoryId: parentCategoryId }), "json", "application/json; charset=utf-8", childCategoriesSuccessfullyRetrieved, operationFailed);
    }
}


function classificationsSuccessfullyRetrieved(data, status) {

    if (status == "success") {

        var options = "<option>Seleccionar...</option>";

        $.each(data, function (i, item) {
            options += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";
        });

        $("#ClassificationId").prop("disabled", false);
        $('#ClassificationId').html(options);

    }
    else {
        operationFailed();
    }
}

function loadClassifications() {
    var parentCategoryId = $('#PreferenceId option:selected').val();

    if (parentCategoryId != null && parentCategoryId.length > 0) {
        submitDataAjax("POST", "/Category/ChildCategories", JSON.stringify({ parentCategoryId: parentCategoryId }), "json", "application/json; charset=utf-8", classificationsSuccessfullyRetrieved, operationFailed);
    }
}

function categoriesSuccessfullyRetrieved(data, status) {

    if (status == "success") {

        var options = "<option>Seleccionar...</option>";

        $.each(data, function (i, item) {
            options += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";
        });

        $("#ParentCategoryId").prop("disabled", false);
        $('#ParentCategoryId').html(options);

    }
    else {
        operationFailed();
    }
}

function loadCategories() {
    var parentCategoryId = $('#ClassificationId option:selected').val();

    if (parentCategoryId != null && parentCategoryId.length > 0) {
        submitDataAjax("POST", "/Category/ChildCategories", JSON.stringify({ parentCategoryId: parentCategoryId }), "json", "application/json; charset=utf-8", categoriesSuccessfullyRetrieved, operationFailed);
    }
}
