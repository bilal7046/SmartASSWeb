(function () {
    var baseAddressUrl = "https://www.universal-tutorial.com/api/";
    $("#countryCtrl").on('change',
        function() {
            $.ajax({
                url: baseAddressUrl + '/states/' + $(this).val(),
                type: 'post',
                contentType: 'html'
            }).done(function (response) {
                $("#stateCtrl").html(response);
            });
        });

    $("#stateCtrl").on('change',
        function () {
            $.ajax({
                url: baseAddressUrl + '/cities/' + $(this).val(),
                type: 'post',
                contentType: 'html'
            }).done(function (response) {
                $("#cityCtrl").html(response);
            });
})()