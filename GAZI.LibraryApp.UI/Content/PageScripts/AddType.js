$(document).ready(function () {
    var typeid = "";
    var silinecektypeid = "";
    $('#btnsifirla').click(function () {
        $('#type_name').val("");
        typeid = "";
        silinecektypeid = "";

    });
    //------------------------YENİ OLUŞTUR----------------------------


    $('#btn-add-type').click(function () {

        if ($('#type_name').val() == "") {
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
                "ID": typeid,
                "Name": $('#type_name').val()
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

    $('.frm-delete-type').click(function () {
        silinecektypeid = $(this).data("id");
    });

    $('#btn-delete-type').click(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: deleteUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": silinecektypeid }

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

    $('.frm-update-type').click(function () {
        $('#type_name').val($(this).data("name"));
        typeid = $(this).data("id");
    });




});