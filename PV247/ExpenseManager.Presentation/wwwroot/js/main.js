$(document).ready(function () {
    var menuOpened = false;

    $(".datepicker").datepicker();

    $("#left-menu-opener").click(function () {
        var padding;
        var margin;
        if (menuOpened) {
            padding = 0;
            margin = -202;
            menuOpened = false;
        } else {
            padding = 200;
            margin = -2; // there where problems if this was set to 0
            menuOpened = true;
        }

        $(".container-fluid")
            .animate({
                'padding-left': padding
            }, 500);
        
        $("#left-menu")
            .animate({
                'margin-left': margin
            }, 500);
    });

    //$("#left-menu-opener").click(function () {
    //    $(".container-fluid").toggleClass("menu-opened", 1000);
    //    $("#left-menu").toggleClass("menu-opened", 1000);
    //});
});