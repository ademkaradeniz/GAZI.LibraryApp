
$(document).ready(function () {
    var bookID = "";

    $('#btn-mail-send').click(function () {

        var ViewBorrowBook = {
            "UserID": $('#user_id').val(),
            "BookID": bookID,
            "BookName": $('#book_name').val(),
            "AuthorName": $('#author_name').val(),
            "PublisherName": $('#publisher_name').val(),
            "TypeName": $('#type_name').val(),
            "NumberOfPage": $('#number_of_page').val(),
            "YearOfPublic": $('#year_of_public').val(),
            "Name": $('#name').val(),
            "SurName": $('#surname').val(),
            "Email": $('#e_mail').val(),
            "Telephone": $('#telephone').val(),
            "AlisTarih": $('#alis_tarih').val(),
            "VerilecekTarih": $('#verilecek_tarih').val()
        }
        //--------- JSON Verisini Bu Arada Oluşturdum--------------------

        $.ajax({
            url: mailSendUrl,
            type: 'POST',
            dataType: 'json',
            data: ViewBorrowBook,

        }).done(function (data) {

            $.alert(data.message, {
                title: 'Hatırlatma İşlemi Sonucu',
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


    $('.frm-mail-send').click(function () {
        bookID = $(this).data("bookid");
        $('#book_name').val($(this).data("bookname"));
        $('#publisher_name').val($(this).data("publishername"));
        $('#number_of_page').val($(this).data("numberofpage"));
        $('#author_name').val($(this).data("authorname"));
        $('#type_name').val($(this).data("typename"));
        $('#year_of_public').val($(this).data("yearofpublic"));
        $('#user_id').val($(this).data("userid"));
        $('#name').val($(this).data("name"));
        $('#surname').val($(this).data("surname"));
        $('#e_mail').val($(this).data("email"));
        $('#telephone').val($(this).data("telephone"));
        $('#alis_tarih').val($(this).data("purchasedate"));
        $('#verilecek_tarih').val($(this).data("issuedate"));
        borrowBookID = $(this).data("id");


    });


});