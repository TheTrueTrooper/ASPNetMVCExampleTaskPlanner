/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: Sep 12,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: to create a angularjs Contoller system to template my work and streamline with APIs
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: NA
//  Writer/Publisher: NA
//  Link: NA
//  }
*/
function OverViewTabClick()
{
    $(".Active").removeAttr("Class");
    $("#OverViewTab").addClass("Active");
    $(".TabBodies >").attr("hidden","true");
}

angular.module("NGProjectsIndex", [])