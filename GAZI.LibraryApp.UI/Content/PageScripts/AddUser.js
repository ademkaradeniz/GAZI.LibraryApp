
//Telefon Sadece Sayı Girişi Yapablimesini Sağlar
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

$(document).ready(function () {
    var userid = "";
    var silinecekuserid = "";
    $('#btnsifirla').click(function () {
        $('#user_name').val("");
        $('#role_id').val("");
        $('#ad').val("");
        $('#soyad').val("");
        $('#user_email').val("");
        $('#user_telephone').val("");
        $('#user_password').val("");
        userid = "";
        silinecekuserid = "";

    });
    //------------------------YENİ OLUŞTUR----------------------------


    $('#btn-add-user').click(function () {
        if ($('#user_name').val() == "" || $('#role_id').val() == "" || $('#ad').val() == "" || $('#soyad').val() == "" || $('#user_email').val() == "" || $('#user_password').val() == "" || $('#user_telephone').val() == ""){
            $.alert("Bilgiler Boş Olamaz", {
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
        else if ($('#user_password').val() != $('#user_password_2').val()) {
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
                "ID": userid,
                "RoleID": $('#role_id').val(),
                "UserName": $('#user_name').val(),
                "Name": $('#ad').val(),
                "SurName": $('#soyad').val(),
                "Email": $('#user_email').val(),
                "Password": $('#user_password').val(),
                "Telephone": $('#user_telephone').val()
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
                        location.reload();
                    }
                });

            });
        }

    });

    //--------------------------SİL-----------------------------

    $('.frm-delete-user').click(function () {
        silinecekuserid = $(this).data("id");
    });

    $('#btn-delete-user').click(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: deleteUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": silinecekuserid }

        }).done(function (data) {

            $.alert(data.message, {
                title: 'Silme İşlemi Sonucu',
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

    ////-------------------------GUNCELLE-------------------------

    $('.frm-update-user').click(function () {
        $('#role_id').val($(this).data("roleid"));
        $('#user_name').val($(this).data("username"));
        $('#ad').val($(this).data("ad"));
        $('#soyad').val($(this).data("soyad"));
        $('#user_email').val($(this).data("email"));
        $('#user_telephone').val($(this).data("telephone"));
        $('#user_password').val($(this).data("password"));
        $('#user_password_2').val($(this).data("password"));
        userid = $(this).data("id");
    });




});