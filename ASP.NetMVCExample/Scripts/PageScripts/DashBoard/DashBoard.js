/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: Augest 28,2017
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
angular.module("NGDashBoard", [])
    //make a service that can be used to get the stuff
.service("ProjectsService", function ($http)
{
    return {
        GetProjects: function ()
        {
            return $http.get("/UtilitiesAPI/GetUsersProjectData", { responseType: "json" })
        }
    }
})
.controller("ProjectList", function ($scope, $interval , ProjectsService)
{
    ProjectsService.GetProjects().then(function (result)
    {
        $scope.projects = result.data;
        $scope.projects.forEach(function (Item)
        {
            Item.StartDate = moment(Item.StartDate);
            if (Item.EndDate)
                Item.EndDate = moment(Item.EndDate);
            else
                Item.EndDate = "N/A"
        });
    });
    $scope.wait = $interval(function ()
    {
        //do for projects.
            var SetAtSize = 0;
            var ElementArray = $.makeArray($("#Projects").children())
            ElementArray.forEach(function (Item)
            {
                var Height = $(Item.childNodes[1].childNodes[1]).height();
                if (Height > SetAtSize)
                    SetAtSize = Height;
            });
            ElementArray.forEach(function (Item)
            {
                $(Item.childNodes[1].childNodes[1]).height(SetAtSize);
            });

            SetAtSize = 0;
            ElementArray = $.makeArray($("#TopInfo").children())
            ElementArray.forEach(function (Item)
            {
                var Height = $(Item.childNodes[3]).height();
                if (Height > SetAtSize)
                    SetAtSize = Height;
            });
            ElementArray.forEach(function (Item)
            {
                $(Item.childNodes[3]).height(SetAtSize);
            });

       }, 200);
})




