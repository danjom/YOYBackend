//=======================================================================================================================

//AJAX
//This function is for submit forms, including the antiforgery token
function submitFormAjax(urlAction, data, sendType, successFunction, errorFunction) {


    $.ajax({
        url: urlAction,
        type: sendType,
        data: data,
        cache: false,
        contentType: false,
        processData: false,
        success: successFunction,
        error: errorFunction
    });
}

//This is for sending sensible data, mostly on POST requests
function submitDataAjax(type, urlAction, data, dataType, contentType, successFunction, errorFunction) {

    $.ajax({
        type: type,
        url: urlAction,
        data: param = data,
        contentType: contentType,
        dataType: dataType,
        success: successFunction,
        error: errorFunction
    });
}

///This is for sending simple request that sends non-sensible data, as GET requests
function submitDataAjaxSimple(type, urlAction, data, successFunction, errorFunction) {

    $.ajax({
        type: type,
        url: urlAction,
        data: data,
        success: successFunction,
        error: errorFunction
    });
}