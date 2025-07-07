

window.cookieManager = {
    setCookie: function (name, value, days = 7, path = '/', sameSite = 'strict', secure = true) {
        let expires = '';

        if (days) {
            const date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = '; expires=' + date.toUTCString();
        }

        const cookieString = name + '=' + encodeURIComponent(value) +
            expires +
            '; path=' + path +
            '; samesite=' + sameSite +
            (secure ? '; secure' : '');

        document.cookie = cookieString;

        return true;
    },

    getCookie: function (name) {
        const nameEq = name + '=';

        const ca = document.cookie.split(';');

        for (let i = 0; i < ca.length; i++) {

            let c = ca[i];

            while (c.charAt(0) === ' ') {
                c = c.substring(1, c.length);
            }

            if (c.indexOf(nameEq) === 0) {
                return decodeURIComponent(c.substring(nameEq.length, c.length));
            }
        }

        return null;
    },

    deleteCookie: function (name, path = '/') {
        document.cookie = name + '=; path=' + path + '; expires=Thu, 01 Jan 1970 00:00:01 GMT; samesite=strict; secure';

        return true;
    },

    getAllCookies: function () {
        const cookies = {};

        const cookieArray = document.cookie.split(';');

        for (let i = 0; i < cookieArray.length; i++) {

            const cookie = cookieArray[i].trim();

            const eqPos = cookie.indexOf('=');

            if (eqPos > -1) {
                const name = cookie.substring(0, eqPos);
                const value = decodeURIComponent(cookie.substring(eqPos + 1));
                cookies[name] = value;
            }
        }

        return cookies;
    }
};