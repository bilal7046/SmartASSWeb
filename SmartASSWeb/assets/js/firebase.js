var firebaseApp = {};
var firebaseStorage = {};
(function () {
    //import firebase from 'firebase';
    //import 'firebase/storage';  // <----

    // Your web app's Firebase configuration
    var firebaseConfig = {
        apiKey: "AIzaSyCO9Y6xIrvrcCvEo5EBitTwcfOhWE2h-BE",
        authDomain: "smartass-28ae3.firebaseapp.com",
        databaseURL: "https://smartass-28ae3.firebaseio.com",
        projectId: "smartass-28ae3",
        storageBucket: "smartass-28ae3.appspot.com",
        messagingSenderId: "700426758001",
        appId: "1:700426758001:web:a1a9fe4b634271390e38d0",
        measurementId: "G-WYLR4YHV1L"
    };
    // Initialize Firebase
    firebase.initializeApp(firebaseConfig);
    //TODO: firebase.analytics();

    firebaseApp = firebase;
    firebaseStorage = firebase.storage();
})()