$(document).ready(function () {
    var roleid = "";
    var silinecekroleid = "";
    $('#btnsifirla').click(function () {
        $('#role_name').val("");
        roleid = "";
        silinecekroleid = "";

    });
    //------------------------YENİ OLUŞTUR----------------------------


    $('#btn-add-role').click(function () {

        if ($('#role_name').val() == "") {
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
        else {
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------
            var Author = {
                "ID": roleid,
                "Name": $('#role_name').val()
            }
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------

            $.ajax({
                url: submitUrl,
                type: 'POST',
                dataType: 'json',
                data: Author,

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

    $('.frm-delete-role').click(function () {
        silinecekroleid = $(this).data("id");
    });

    $('#btn-delete-role').click(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: deleteUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": silinecekroleid }

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

    $('.frm-update-role').click(function () {
        $('#role_name').val($(this).data("name"));
        roleid = $(this).data("id");
    });




});