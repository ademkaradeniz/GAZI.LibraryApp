
//Basım Yılı Sadece Sayı Girişi Yapablimesini Sağlar
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

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

    $('#select_user').change(function () {

        //--------- JSON Verisini Bu Arada Oluşturdum--------------------
        $.ajax({
            url: selectUrl,
            type: 'POST',
            dataType: 'json',
            data: { "Id": $('#select_user').val() }

        }).done(function (data) {
            $('#e_mail').val(data.veri["Email"]);
            $('#user_id').val(data.veri["ID"]);
            $('#telephone').val(data.veri["Telephone"]);
            $.alert(data.message, {
                title: 'Kullanıcı Bulma İşlemi',
                closeTime: 3000,
                autoClose: true,
                withTime: false,
                type: data.result,
                isOnly: true,
                onClose: function () {
                }
            });

        });

    });

    $('.frm-lend-book').click(function () {
        //Önceki kullanıcı bilgilerini siler
        $('#select_user').val("");
        $('#e_mail').val("");
        $('#user_id').val("");
        $('#telephone').val("");
        
        $('#book_name').val($(this).data("bookname"));
        $('#publisher_name').val($(this).data("publishername"));
        $('#number_of_page').val($(this).data("numberofpage"));
        $('#author_name').val($(this).data("authorname"));
        $('#type_name').val($(this).data("typename"));
        $('#year_of_public').val($(this).data("yearofpublic"));
        kitapID = $(this).data("id");

        
    });


});