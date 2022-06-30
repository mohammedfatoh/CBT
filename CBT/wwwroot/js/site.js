// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".center").animate({
        top: "50px"
    }, 500, function () {
        $(this).animate({
            Height: "400px"
        }, 1000, function () {
            $(".log").slideDown(500)
        });
    });

    $("#asd").click(function () {
       $(".asd2").remove();
    });
    $(".center2").animate({
        top: "50px"
    }, 500, function () {
        $(this).animate({
        }, 1000, function () {
            $(".log").slideDown(500)
        });
    });
});