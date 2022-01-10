$(document).ready(function () {

    //------------------------KURSİYER OLUŞTUR----------------------------

    $('#frmmailsender').click(function () {
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------
            var Id = $('#username').data("id");
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------

            $.ajax({
                url: mailSenderUrl,
                type: 'POST',
                dataType: 'json',
                data: { "Id": Id }

            }).done(function (data) {

                $.alert(data.message, {
                    title: 'Mail İşlemi Sonucu',
                    closeTime: 3000,
                    autoClose: true,
                    withTime: false,
                    type: data.result,
                    isOnly: true,
                    onClose: function () {
                        location.reload();
                    }
                });

            });
        

    });
});