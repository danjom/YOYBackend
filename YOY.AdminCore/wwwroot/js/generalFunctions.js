//These are general functions that all views share

function evaluateRegularExpression(regularExp, val) {
    return regularExp.test(val);
}

//Data format validation
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return evaluateRegularExpression(re, email);
    //return re.test(email);
}

function validateNumericString(data) {
    var re = /^\d+$/;
    return evaluateRegularExpression(re, data)
    //return re.test(data);
}

//Functions to retrieve notifications in real time

//AJAX failed function notification
function operationFailed() {
    alert("Error al recuperar datos");

}

function retrieveNotificationsSuccessful(data, status) {

    if (status == "success") {

        var notificationsHTML = "";

        jQuery.each(data, function (index, value) {
            
            notificationsHTML += '<div class="panel panel-redish"><div class="panel-heading"><h3 class="panel-title">' + this["Title"] + '</h3></div><div class="panel-body"><img style="margin-left: auto !important; margin-right: auto !important; display: block;" src="' + this["ImagePath"] + '" height="150" /><div style="text-align: center;" class="huge">' + this["Description"] + '</div></div></div><br /><br />';
            
        });
        if (notificationsHTML != "" && notificationsHTML.length > 0) {
            $("#notificationHTML").html(notificationsHTML);
            displayModalNotifications();
        }
        
    }
}

function retrieveModalNotifications(isNewUser) {

    submitDataAjaxSimple("GET", "/MyClub/GetDashboardNotifications", { IsNewUser: isNewUser }, retrieveNotificationsSuccessful, operationFailed)
}

function displayModalView() {
    $("#productView").modal('show');
}

function displayPictureInModal(imageId, productName, productDescription) {



    $('#modalTitle').html(productName);
    var content = '';

    content += '<div id="cboxWrapper" style="height: 100%; width: 100%;">';
    content += '<div>';
    content += '<div id="cboxTopLeft" style="float: left;"></div>';
    content += '<div id="cboxTopCenter" style="float: left;"></div>';
    content += '<div id="cboxTopRight" style="float: left;"></div>';
    content += '<div style="clear: left;">';
    content += '<div id="cboxMiddleLeft" style="float: center;"></div>';
    content += '<div id="cboxContent" class="row product-items last"><br/><p style="text-align: center;">' + productDescription + '</p><br/><br/>';
    content += '<div style="height: 100%; width: 100%;" class="image">';
    content += '<img class="img-responsive" style="display:block;margin:auto;height: 70%; width: 70%;" src="https://kirin-admin.azurewebsites.net/Data/Image?Id=' + imageId + '">';
    content += '</div>';
    content += '</div>';
    content += '<div id="cboxMiddleRight" style="float: left; height: 75%;"></div>';
    content += '</div>';
    content += '<div style="clear: left;">';
    content += '<div id="cboxBottomLeft" style="float: left;"></div>';
    content += '<div id="cboxBottomCenter" style="float: left; width: 60%;"></div>';
    content += '<div id="cboxBottomRight" style="float: left;"></div>';
    content += '</div>';
    content += '</div>';

    //Finally displays the modal
    $('#modalBody').html(content);
    displayModalView();
}
