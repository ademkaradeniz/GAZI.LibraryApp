
//Basım Yılı Sadece Sayı Girişi Yapablimesini Sağlar
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

$(document).ready(function () {
    var bookid = "";
    var silinecekbookid = "";
    $('#btnsifirla').click(function () {
        $('#book_name').val("");
        $('#publisher_id').val("");
        $('#number_of_page').val("");
        $('#author_id').val("");
        $('#type_id').val("");
        $('#year_of_public').val("");
        bookid = "";
        silinecekbookid = "";

    });
    //------------------------YENİ OLUŞTUR----------------------------


    $('#btn-add-book').click(function () {
        if ($('#book_name').val() == "" || $('#publisher_id').val() == "" || $('#number_of_page').val() == "" || $('#author_id').val() == "" || $('#type_id').val() == "" || $('#year_of_public').val() == "") {
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
            var Book = {
                "ID": bookid,
                "Name": $('#book_name').val(),
                "AuthorID": $('#author_id').val(),
                "PublisherID": $('#publisher_id').val(),
                "TypeID": $('#type_id').val(),
                "YearOfPublic": $('#year_of_public').val(),
                "NumberOfPage": $('#number_of_page').val(),
                "Image": $('#image').val()
            }
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------

            $.ajax({
                url: submitUrl,
                type: 'POST',
                dataType: 'json',
                data: Book,

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

    $('.frm-delete-book').click(function () {
        silinecekbookid = $(this).data("id");
    });

    $('#btn-delete-book').click(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: deleteUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": silinecekbookid }

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

    $('.frm-update-book').click(function () {
        $('#book_name').val($(this).data("name"));
        $('#publisher_id').val($(this).data("publisherid"));
        $('#number_of_page').val($(this).data("numberofpage"));
        $('#author_id').val($(this).data("authorid"));
        $('#type_id').val($(this).data("typeid"));
        $('#year_of_public').val($(this).data("yearofpublic"));
        alert($(this).data("image"));
        $('#bookimage').attr('src', $(this).data("image"));
        bookid = $(this).data("id");
    });




});