$(document).ready(function () {

    //------------------------KURSİYER OLUŞTUR----------------------------

    $('#btn-add-user').click(function () {
        if ($('#username').val() == "" || $('#name').val() == "" || $('#surname').val() == "" || $('#email').val() == "" || $('#telephone').val() == "" || $('#password').val() == "" || $('#password_2').val() == "") {
            $.alert("Kayıt Bilgileri Boş Olamaz", {
                title: 'Kayıt İşlemi Sonucu',
                closeTime: 3000,
                autoClose: true,
                withTime: false,
                type: "danger",
                isOnly: true,
                onClose: function () {

                }
            });
        }
        else if ($('#password').val() != $('#password_2').val()) {
            $.alert("Şifreler Aynı Olmalı!", {
                title: 'Kayıt İşlemi Sonucu',
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
            var User = {
                "UserName": $('#username').val(),
                "Name": $('#name').val(),
                "SurName": $('#surname').val(),
                "Soyadi": $('#soyadi').val(),
                "Email": $('#email').val(),
                "Telephone": $('#telephone').val(),
                "Password": $('#password').val()
            }
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------

            $.ajax({
                url: submitUrl,
                type: 'POST',
                dataType: 'json',
                data: User,

            }).done(function (data) {

                $.alert(data.message, {
                    title: 'Kayıt İşlemi Sonucu',
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