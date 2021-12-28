// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.existing_image a').click(function (e) {
    e.preventDefault();
    var lastItemId = $('.existing_image:last').attr("id");
    if ($('.existing_image').length == 1 || $(this).parent().attr("id") == lastItemId) {
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

if ($("#no_pictures").length > 0)
{
    if ($("#no_pictures").hasClass("field-validation-error"))
    {
        $('.no_existing_images').removeClass('d-none');
    }
}

if ($("#ddlYearsValue").length > 0)
{
    $("select[name='Pet.Years']").val($("#ddlYearsValue").val());
}

if ($("#ddlMonthsValue").length > 0) {
    $("select[name='Pet.Months']").val($("#ddlMonthsValue").val());
}

if ($("#ddlWeeksValue").length > 0) {
    $("select[name='Pet.Weeks']").val($("#ddlWeeksValue").val());
}