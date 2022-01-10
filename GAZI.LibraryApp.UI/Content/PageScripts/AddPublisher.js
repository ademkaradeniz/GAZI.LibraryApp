$(document).ready(function () {
    var publisherid = "";
    var silinecekpublisherid = "";
    $('#btnsifirla').click(function () {
        $('#publisher_name').val("");
        publisherid = "";
        silinecekpublisherid = "";

    });
    //------------------------YENİ OLUŞTUR----------------------------


    $('#frmaddpublisher').click(function () {

        if ($('#publisher_name').val() == "") {
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
                "ID": publisherid,
                "Name": $('#publisher_name').val()
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

    $('.frm-delete-yayinevi').click(function () {
        silinecekpublisherid = $(this).data("id");
    });

    $('#btn-delete-yayinevi').click(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: deleteUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": silinecekpublisherid }

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

    $('.btn-update-yayinevi').click(function () {
        $('#publisher_name').val($(this).data("name"));
        publisherid = $(this).data("id");
    });




});