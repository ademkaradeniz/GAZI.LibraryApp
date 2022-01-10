$(document).ready(function () {
    var authorid = "";
    var silinecekauthorid = "";
    $('#btnsifirla').click(function () {
        $('#author_name').val("");
        authorid = "";
        silinecekauthorid = "";

    });
    //------------------------YENİ OLUŞTUR----------------------------


    $('#frmaddauthor').click(function () {
        
        if ($('#author_name').val() == "") {
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
                "ID": authorid,
                "Name": $('#author_name').val()
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

    $('.frm-delete-yazar').click(function () {
        silinecekauthorid = $(this).data("id");
    });

    $('#btn-delete-yazar').click(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: deleteUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": silinecekauthorid }

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

    $('.btn-update-yazar').click(function () {
        $('#author_name').val($(this).data("name"));
        authorid = $(this).data("id");
    });




});