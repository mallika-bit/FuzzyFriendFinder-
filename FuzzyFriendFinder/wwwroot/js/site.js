// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.existing_image a').click(function (e) {
    e.preventDefault();
    if ($('.existing_image').length == 1) {
        $('#existingImageUrls').val($('#existingImageUrls').val().replace($(this).parent().attr("id"), ""));
    }
    else {
        $('#existingImageUrls').val($('#existingImageUrls').val().replace($(this).parent().attr("id") + ", ", ""));
    }
    
    $(this).parent().remove();
    if ($('.existing_image').length == 0) {
        $('.no_existing_images').removeClass('d-none');
    }
});