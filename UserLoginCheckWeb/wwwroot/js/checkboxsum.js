$(document).ready(function () {
    $('input[name="selectedBills"]').change(function () {
        var totalAmount = 0;
        $('input[name="selectedBills"]:checked').each(function () {
            var amount = parseFloat($(this).closest('tr').find('.due-amount').text());
            totalAmount += amount;
        });

        $('#payButton').val('Pay Selected Bills ($' + totalAmount.toFixed(2) + ')');
    });
});