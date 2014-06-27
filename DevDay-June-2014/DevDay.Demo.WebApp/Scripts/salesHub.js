$(function () {
    var salesHubConnection = $.hubConnection("http://uk-rnd-168.2gis.local:8082");
    var salesHub = salesHubConnection.createHubProxy('salesHub');
    salesHubConnection.start()
        .done(function() {
            console.log("salesHubConnection started");

            $('#createOrder').click(function() {
                var orderNumber = $('#orderNumber').val();
                salesHub.invoke('createOrder', orderNumber)
                    .done(function() {
                        $('.top-right').notify({
                            type: 'info',
                            message: { text: "Order '" + orderNumber + "' created successfully" },
                            fadeOut: { enabled: true, delay: 1000 },
                        }).show();
                    });
            });
        })
        .fail(function() {
            $('#createOrder').prop('disabled', true);
            $('.top-right').notify({
                type: 'danger',
                message: { text: 'Could not connect to sales hub' },
                fadeOut: { enabled: true, delay: 3000 },
            }).show();
        });
});
