/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 28,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To make a easy to read ajax call
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: NA
//  Writer/Publisher: NA
//  Link: NA
//  }
*/

//ajax lib
//jquerie loaded

//Args
//tags : One of GET , POST, DELETE, PUT
//url : where is it going
//sendData : js object with data to send
//ResposeType : one of : text, html, json, jsonp, xml
//Target function : object Target to invoke to handle data
//Fnc Success and fncFail
function AjaxRequest(type, url, sendData, responseType, fncSuccess, fncFail)
{
    var options = {};
    options["type"] = type;
    options["url"] = url;
    options["data"] = sendData;
    options["dataType"] = responseType;
    options["success"] = fncSuccess;
    options["error"] = fncFail;
    $.ajax(options);
}