
$(document).ready(function () {
    var borrowBookID = "";

    $('#btn-return-book').click(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: submitUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": borrowBookID }

        }).done(function (data) {

            $.alert(data.message, {
                title: 'Teslim İşlemi Sonucu',
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


    $('.frm-return-book').click(function () {
        $('#book_name').val($(this).data("bookname"));
        $('#publisher_name').val($(this).data("publishername"));
        $('#number_of_page').val($(this).data("numberofpage"));
        $('#author_name').val($(this).data("authorname"));
        $('#type_name').val($(this).data("typename"));
        $('#year_of_public').val($(this).data("yearofpublic"));
        $('#name').val($(this).data("name"));
        $('#surname').val($(this).data("surname"));
        $('#e_mail').val($(this).data("email"));
        $('#telephone').val($(this).data("telephone"));
        $('#alis_tarih').val($(this).data("purchasedate"));
        $('#verilecek_tarih').val($(this).data("issuedate"));
        borrowBookID = $(this).data("id");

        if ($(this).data("image") == null || $(this).data("image") == "") {
            $('#bookimage').attr('src', '/Content/Gentelella/production/images/lend-return-book.png');
        }
        else {
            $('#bookimage').attr('src', $(this).data("image"));
        }

    });


});