// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".center").animate({
        top: "100px"
    }, 500, function () {
        $(this).animate({
            height: "380px"
        }, 1000, function () {
            $(".log").slideDown(500)
        });
    });

    $(".landing").show(1000);
});