(function() {
    // Initialize the FirebaseUI Widget using Firebase.
    var ui = new firebaseui.auth.AuthUI(firebase.auth());

    var uiConfig = {
        callbacks: {
            signInSuccessWithAuthResult: function (authResult, redirectUrl) {
                // User successfully signed in.
                // Return type determines whether we continue the redirect automatically
                // or whether we leave that to developer to handle.
                $.ajax({
                    type: 'POST',
                    url: "/Account/LoginFromFirebase?uid=" + authResult.user.uid,
                    async: false,
                    cache: false,
                    processData: false,
                    contentType: false,
                    success: (function (data) {
                        console.log("success");
                    }),
                    error: function (request, status, error) {
                        console.log("error " + error);
                    }
                });
                return true;
            },
            uiShown: function () {
                // The widget is rendered.
                // Hide the loader.
                document.getElementById('loader').style.display = 'none';
            }
        },
        // Will use popup for IDP Providers sign-in flow instead of the default, redirect.
        signInFlow: 'popup',
        signInSuccessUrl: '/Home/Index',
        signInOptions: [
            // Leave the lines as is for the providers you want to offer your users.
            firebase.auth.EmailAuthProvider.PROVIDER_ID,
            firebase.auth.GoogleAuthProvider.PROVIDER_ID,
            firebase.auth.FacebookAuthProvider.PROVIDER_ID
        ]
        //,
        // Terms of service url.
        //tosUrl: function () {
        //    window.location.href = 'https://www.smartassdigitalbusinesscard.com/terms-and-conditions/';
        //},
        // Privacy policy url.
        //privacyPolicyUrl: function () {
        //    window.location.href = "https://www.smartassdigitalbusinesscard.com/privacy-policy/";
        //}
    };

    // The start method will wait until the DOM is loaded.
    ui.start('#firebaseui-auth-container', uiConfig);
})()