$(function () {
    var validationsHubConnection = $.hubConnection("http://uk-rnd-168.2gis.local:8081");
    var validationsHub = validationsHubConnection.createHubProxy('validationsHub');
    validationsHub.on('sendStatus', function (validationResult, message) {
        var alertType;
        if (!validationResult || validationResult == 'error') {
            alertType = 'danger';
        } else {
            alertType = 'info';
        }

        $('.top-right').notify({
            type: alertType,
            message: { text: message },
            fadeOut: { enabled: true, delay: 10000 },
        }).show();
    });

    validationsHubConnection.start()
        .done(function() {
            console.log("validationsHubConnection started");
        })
        .fail(function() {
            $('.top-right').notify({
                type: 'danger',
                message: { text: 'Could not connect to validations hub' },
                fadeOut: { enabled: true, delay: 3000 },
            }).show();
        });
});
