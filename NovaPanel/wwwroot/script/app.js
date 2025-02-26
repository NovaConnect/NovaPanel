function WideOnly() {
    const screenWidth = window.innerWidth;
    const screenHeight = window.innerHeight;

    const wideOnlyElements = document.querySelectorAll('[tag="WideOnly"]');

    wideOnlyElements.forEach(element => {
        if (screenHeight > screenWidth) {
            element.style.display = 'none';
        } else {
            element.style.display = '';
        }
    });
}
function csc() {
    const cookie = document.cookie
        .split('; ')
        .find(row => row.startsWith('session='))
        ?.split('=')[1];
    return cookie;
}
function ssc(value, daysToExpire) {
    const date = new Date();
    date.setTime(date.getTime() + (daysToExpire * 24 * 60 * 60 * 1000));
    const expires = "expires=" + date.toUTCString();
    const path = "path=/";
    const sameSite = "SameSite=Strict";
    document.cookie = `session=${value}; ${expires}; ${path}; ${sameSite}`;
}
function dsc() {
    document.cookie = "session=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
    window.location.reload();
}
WideOnly();

window.addEventListener('resize', WideOnly);