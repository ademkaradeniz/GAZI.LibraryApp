
$(document).ready(function () {

    $('.frm-library-book').click(function () {
        $('#book_name').val($(this).data("bookname"));
        $('#publisher_name').val($(this).data("publishername"));
        $('#number_of_page').val($(this).data("numberofpage"));
        $('#author_name').val($(this).data("authorname"));
        $('#type_name').val($(this).data("typename"));
        $('#year_of_public').val($(this).data("yearofpublic"));
        $('#bookimage').val($(this).data("image"));

    });


});