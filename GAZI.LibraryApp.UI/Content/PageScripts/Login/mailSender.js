$(document).ready(function () {

    //------------------------KURSİYER OLUŞTUR----------------------------

    $('#frmmailsender').click(function () {
        if ($('#emailsender').val() == "") {
            $.alert("Email Bilgileri Boş Olamaz", {
                title: 'Mail İşlemi Sonucu',
                closeTime: 3000,
                autoClose: true,
                withTime: false,
                type: "danger",
                isOnly: true,
                onClose: function () {

                }
            });
        }
        else {
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------
            var Email = {
                "email": $('#emailsender').val()

            }
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------

            $.ajax({
                url: mailSenderUrl,
                type: 'POST',
                dataType: 'json',
                data: Email,

            }).done(function (data) {

                $.alert(data.message, {
                    title: 'Mail İşlemi Sonucu',
                    closeTime: 3000,
                    autoClose: true,
                    withTime: false,
                    type: data.result,
                    isOnly: true,
                    onClose: function () {
                        $(location).attr('href', '\\Login\\Index')
                    }
                });

            });
        }

    });
});