var mainApp = {};
(function () {
    var firebase = firebaseApp;
    var uid = null;
    firebase.auth().onAuthStateChanged(function (user) {
        if (user) {
            // User is signed in.
            uid = user.uid;

        } else {
            uid = null;
            window.location.href = '/Account/Login';
        }
    });

    function logOut() {
        firebase.auth().signOut();
        window.location.href = '/Account/Login';
    }

    mainApp.logout = logOut;
})()