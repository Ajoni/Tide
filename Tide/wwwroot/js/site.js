function checkStatus(response) {
    if (response.status >= 200 && response.status < 300) {
        return response
    } else {
        var error = new Error(response.statusText)
        error.response = response
        throw error
    }
}

function _fetch(url, body) {
    const headers = {
        'Content-Type': 'application/json'
    }
    return fetch(url, {
            method: "POST",
            headers: headers,
            body: body
    })
        .then(checkStatus)
}
