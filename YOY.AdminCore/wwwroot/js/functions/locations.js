function statesSuccessfullyRetrieved(data, status) {

    if (status == "success") {

        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $.each(data, function (i, item) {
            options += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";
        });

        $("#State").removeAttr('disabled');
        $('#State').html(options);

        $("#City").attr('disabled', 'disabled');
        $('#City').html("");

        $("#District").attr('disabled', 'disabled');
        $('#District').html("");
    }
    else {
        operationFailed();
    }
}

function loadStates() {
    var countryId = $('#Country option:selected').val();

    if (countryId != null && countryId.length > 0 && countryId != "00000000-0000-0000-0000-000000000000") {
        submitDataAjax("POST", "/Locations/States", JSON.stringify({ countryId: countryId }), "json", "application/json; charset=utf-8", statesSuccessfullyRetrieved, operationFailed);
    }
    else {

        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $("#State").attr('disabled', 'disabled');
        $('#State').html(options);

        $("#City").attr('disabled', 'disabled');
        $('#City').html(options);

        $("#District").attr('disabled', 'disabled');
        $('#District').html(options);
    }
}

function commercesSuccessfullyRetrieved(data, status) {

    if (status == "success") {

        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $.each(data, function (i, item) {
            options += "<option value=\"" + item.Key + "\">" + item.Value + "</option>";
        });

        $("#TenantId").removeAttr('disabled');
        $('#TenantId').html(options);
    }
    else {
        operationFailed();
    }
}

function loadCommerces() {
    var countryId = $('#Country option:selected').val();

    if (countryId != null && countryId.length > 0 && countryId != "00000000-0000-0000-0000-000000000000") {

        submitDataAjax("POST", "/Locations/Commerces", JSON.stringify({ countryId: countryId }), "json", "application/json; charset=utf-8", commercesSuccessfullyRetrieved, operationFailed);
    }
    else {
        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $("#TenantId").attr('disabled', 'disabled');
        $('#TenantId').html(options);
    }
}

function citiesSuccessfullyRetrieved(data, status) {

    if (status == "success") {

        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $.each(data, function (i, item) {
            options += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";
        });

        $("#City").removeAttr('disabled');
        $('#City').html(options);

        $("#District").attr('disabled', 'disabled');
        $('#District').html("");
    }
    else {
        operationFailed();
    }
}

function loadCities() {
    var stateId = $('#State option:selected').val();

    if (stateId != null && stateId.length > 0 && stateId != "00000000-0000-0000-0000-000000000000") {
        submitDataAjax("POST", "/Locations/Cities", JSON.stringify({ stateId: stateId }), "json", "application/json; charset=utf-8", citiesSuccessfullyRetrieved, operationFailed);
    }
    else {
        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $("#City").attr('disabled', 'disabled');
        $('#City').html(options);

        $("#District").attr('disabled', 'disabled');
        $('#District').html(options);
    }
}


function districtsSuccessfullyRetrieved(data, status) {

    if (status == "success") {

        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $.each(data, function (i, item) {
            options += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";
        });

        $("#District").removeAttr('disabled');
        $('#District').html(options);

    }
    else {
        operationFailed();
    }
}

function loadDistricts() {
    var cityId = $('#City option:selected').val();

    if (cityId != null && cityId.length > 0 && cityId != "00000000-0000-0000-0000-000000000000") {
        submitDataAjax("POST", "/Locations/Districts", JSON.stringify({ cityId: cityId }), "json", "application/json; charset=utf-8", districtsSuccessfullyRetrieved, operationFailed);
    }
    else {

        var options = "<option value=\"00000000-0000-0000-0000-000000000000\">Seleccionar...</option>";

        $("#District").attr('disabled', 'disabled');
        $('#District').html(options);
    }
}

function buildLocationAddress() {

    var address = "";

    //Sets the country
    var countryId = $('#Country option:selected').val();
    var countryName = $("#Country option:selected").text();

    address += countryName + ":" + countryId + ",";

    //Sets the state
    var stateId = $('#State option:selected').val();
    var stateName = $("#State option:selected").text();

    address += stateName + ":" + stateId + ",";

    //Sets the city
    var cityId = $('#City option:selected').val();
    var cityName = $("#City option:selected").text();

    address += cityName + ":" + cityId + ",";

    //Sets the district
    var districtId = $("#District option:selected").val();
    var district = $("#District option:selected").text();

    address += district + ":" + districtId;

    //Finally sets the complete address
    $('#locationAddressData').val(address);
}