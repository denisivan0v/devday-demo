$(function () {
    var salesHubConnection = $.hubConnection("http://localhost:8082");
    var salesHub = salesHubConnection.createHubProxy('salesHub');
    salesHubConnection.start().done(function () {
        console.log("salesHubConnection started");

        $('#createOrder').click(function () {
            var orderNumber = $('#orderNumber').val();
            salesHub.invoke('createOrder', orderNumber).done(function () {
                $('.body-content').prepend(
                    '<div class="alert alert-info alert-dismissable">' +
                        '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                        'Order <strong>' + orderNumber + '</strong> created successfully' +
                    '</div>');
            });
        });
    });
});
