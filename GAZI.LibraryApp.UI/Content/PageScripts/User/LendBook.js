
$(document).ready(function () {
    var kitapID = "";

    $('#btn-add-borrowbook').click(function () {
        if ($('#user_id').val() == "" || $('#select_user').val() == "" || kitapID == "" || $('#alis_tarih').val() == "" || $('#verilecek_tarih').val() == "") {
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
            var BorrowBook = {
                "UserID": $('#user_id').val(),
                "BookID": kitapID,
                "PurchaseDate": $('#alis_tarih').val(),
                "IssueDate": $('#verilecek_tarih').val()
            }
            //--------- JSON Verisini Bu Arada Oluşturdum--------------------

            $.ajax({
                url: submitUrl,
                type: 'POST',
                dataType: 'json',
                data: BorrowBook,

            }).done(function (data) {
                $.alert(data.message, {
                    title: 'Ödünç İşlemi Sonucu',
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

    })

    $('.frm-lend-book').click(function () {
        //Önceki kullanıcı bilgilerini siler
        $('#book_name').val($(this).data("bookname"));
        $('#publisher_name').val($(this).data("publishername"));
        $('#number_of_page').val($(this).data("numberofpage"));
        $('#author_name').val($(this).data("authorname"));
        $('#type_name').val($(this).data("typename"));
        $('#year_of_public').val($(this).data("yearofpublic"));
        kitapID = $(this).data("id");

        if ($(this).data("image") == null || $(this).data("image") == "") {
            $('#bookimage').attr('src', '/Content/Gentelella/production/images/lend-return-book.png');
        }
        else {
            $('#bookimage').attr('src', $(this).data("image"));
        }

    });


});