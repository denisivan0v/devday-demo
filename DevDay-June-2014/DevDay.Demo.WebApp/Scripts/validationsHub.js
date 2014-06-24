$(function () {
    var validationsHubConnection = $.hubConnection("http://localhost:8081");
    var validationsHub = validationsHubConnection.createHubProxy('validationsHub');
    validationsHub.on('sendStatus', function (validationResult, message) {
        var alertClass;
        if (!validationResult || validationResult == 'error') {
            alertClass = 'alert-danger';
        } else {
            alertClass = 'alert-info';
        }

        $('.body-content').prepend(
            '<div class="alert ' + alertClass + ' alert-dismissable">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                '<strong>' + message + '</strong>' +
            '</div>');
    });

    validationsHubConnection.start().done(function () {
        console.log("validationsHubConnection started");
    });
});
