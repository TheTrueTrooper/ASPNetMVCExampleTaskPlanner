/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: Sep 17,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: to create a module for reusable code.
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: NA
//  Writer/Publisher: NA
//  Link: NA
//  }
*/
angular.module("ngAngelModule", [])
//{{picture | ByteToBase64}}
//used to Convert a Byte arrat to a Base 64 string probably for pictures
.filter('ByteToBase64', function ()
{//{{picture | ByteToBase64}}
    return function (buffer)
    {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++)
        {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    };
})