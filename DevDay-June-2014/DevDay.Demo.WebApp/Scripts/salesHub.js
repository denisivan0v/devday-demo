$(function () {
    var salesHubConnection = $.hubConnection("http://localhost:8082");
    var salesHub = salesHubConnection.createHubProxy('salesHub');
    salesHubConnection.start().done(function () {
        console.log("salesHubConnection started");

        $('#createOrder').click(function () {
            var orderNumber = $('#orderNumber').val();
            salesHub.invoke('createOrder', orderNumber).done(function () {
                $('.top-right').notify({
                    type: 'info',
                    message: { text: "Order '" + orderNumber + "' created successfully" },
                    fadeOut: { enabled: true, delay: 1000 },
                }).show();
            });
        });
    });
});
