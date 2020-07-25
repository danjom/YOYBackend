function getCantones(prov) {
    $.ajax({
        url: "@Url.Action("Cantones", "Locations")",
        data: {provincia: prov},
    dataType: "json",
    type: "POST",
    error: function() {
        alert("No fue posible obtener la lista de cantones");
    },
    success: function(data) {
        var items = "";
        $.each(data, function(i, item) {
            items += "<option value=\"" + item.Value + "\">" + item.Text + "</option>";
        });

        $("#Canton").html(items);
    }
});
}


function getDistritos(cant) {
    $.ajax({
        url: "@Url.Action("Distritos", "Locations")",
        data: {canton: cant},
    dataType: "json",
    type: "POST",
    error: function() {
        alert("no fue posible obtener la lista de distritos");
    },
    success: function(data) {
        var items = "";
        $.each(data, function(i, item) {
            items += "<option value=\"" + item.Value + "\">" + item.Text + "</option>";
        });

        $("#Distrito").html(items);
    }
});
}

$(document).ready(function(){
    $("#Provicia").change(function() {
        var prov = $("#Provincia").val();
        getCantones(prov);
    });

    $("#Canton").change(function () {
        var cant = $("#Canton").val();
        getDistritos(cant);
    });
});