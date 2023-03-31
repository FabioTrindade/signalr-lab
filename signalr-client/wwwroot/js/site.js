// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function SendRequest(method, url, responseType, params, callbackSuccess, callbackError) {
    if (!url || url.length == 0)
        return;

    axios({
        url: url,
        method: (!method ? 'GET' : method),
        responseType: (!responseType ? 'JSON' : responseType),
        params: (!params ? '' : params)
    }).then(function (response) {
        if (callbackSuccess)
            return callbackSuccess(response)
    }).catch(function (error) {
        if (callbackError)
            return callbackError(error.data, error.status)
    })
}