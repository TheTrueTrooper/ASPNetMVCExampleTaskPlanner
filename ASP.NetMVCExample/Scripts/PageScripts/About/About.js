//2017-04-24 The Trooper
// basic page script

//Assume jQuery Loaded

var ScrollID = $("#ScrollId").valueOf()

function ReadyScroll()
{
    if ($("#ScrollId").valueOf() != null && $("#ScrollId").valueOf() != "")
    {
        var ScrollID = $("#ScrollId").val();

        $('html, body').animate({
            scrollTop: $("#" + ScrollID.toString()).offset().top
        }, 2000);
    }
}

$(document).ready(function () {
    ReadyScroll();
})