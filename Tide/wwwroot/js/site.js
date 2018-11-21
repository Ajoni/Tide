const accessTokenName = "acces_token";
const refreshTokenName = "refresh_token";

function checkStatus(response) {
    if (response.status >= 200 && response.status < 300) {
        return response
    } else {
        var error = new Error(response.statusText)
        error.response = response
        throw error
    }
}

function _fetch(url, options) {
    const headers = {
        'Content-Type': 'application/json'
    }
    return fetch(url, {
            headers: headers,
            ...options
    })
        .then(checkStatus)
}

function loggedInWithRefresh() {
        let token = getToken("acces_token")
        if (!!token && isTokenExpired(token)) {
            refresh()
        }

        token = getToken("acces_token")
        return !!token && !isTokenExpired(token)
    }

function loggedIn() {
        const token = getToken("acces_token")
        return !!token && !isTokenExpired(token)
    }

function isTokenExpired(token) {
        try {
            const decoded = decode(token);
            if (decoded.exp < Date.now() / 1000)
                return true;
            else
                return false;
        }
        catch (err) {
            return false;
        }
    }

function refresh(domain) {
    const accessToken = getToken("acces_token");
    const refreshToken = getToken("refresh_token")
    const body = JSON.stringify({
            AccessToken: accessToken,
            RefreshToken: refreshToken
    });
    const url = `https://${domain}/token/refresh`;

    return _fetch(url, {
            method: 'POST',
            body: body
        }).then(res => res.json()).then( res => {
            setToken(res.token, "acces_token");
            setToken(res.refreshToken, "refresh_token");
            return res;
        })
    }

function setToken(idToken, name) {
        localStorage.setItem(name, idToken)
    }

function getToken(name) {
        return localStorage.getItem(name)
    }

function logout(domain) {
    return fetch(`https//${domain}/token/revoke`, {
            method: 'POST',
        }).then(() => {
            localStorage.removeItem("acces_token");
            localStorage.removeItem("refresh_token");
        }).catch(() => {
            localStorage.removeItem("acces_token");
            localStorage.removeItem("refresh_token");
        })
}

function decode(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(window.atob(base64));
};