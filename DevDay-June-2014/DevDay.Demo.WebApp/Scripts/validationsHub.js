$(function () {
    var validationsHubConnection = $.hubConnection("http://localhost:8081");
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

    validationsHubConnection.start().done(function () {
        console.log("validationsHubConnection started");
    });
});
