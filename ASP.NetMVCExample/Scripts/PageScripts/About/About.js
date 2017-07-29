/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To Scroll on load for the about page (also to test bundles)
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: NA
//  Writer/Publisher: NA
//  Link: NA
//  }
*/

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