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
angular.module("NGProjectsIndex", ["ngRoute"])
.config(function ($routeProvider, $rootScope)
{
    $rootScope.RouteURL = $("#RootURLData").val;
    var AngularViewVar = $rootScope.RouteURL + "?AngularView=";
    $routeProvider
        .when($rootScope.RouteURL + "/OverView",
        {
            templateUrl: AngularViewVar + "OverView",
            controller: "OverViewController"
        })
        .when($rootScope.RouteURL + "/TaskCellView",
        {
            templateUrl: AngularViewVar + "TaskCellView",
            controller: "TaskCellViewController"
        })
        ($rootScope.RouteURL + "/GanttChartView",
        {
            templateUrl: AngularViewVar + "GanttChartView",
            controller: "GanttChartViewController"
        })
        .when($rootScope.RouteURL + "/PerkChartView",
        {
            templateUrl: AngularViewVar + "PerkChartView",
            controller: "PerkChartViewController"
        });
})
.service("ProjectsGetterService", function ($http)
{
    return {
        GetProjects: function ()
        {
            return $http.get("/UtilitiesAPI/ProjectData", { responseType: "json" })
        },
        GetTasks: function ()
        {
            return $http.get("/UtilitiesAPI/ProjectTasksData", { responseType: "json" })
        }
    }
})
.controller("OverViewController", function ()
{

})
.controller("TaskCellViewController", function ()
{

})
.controller("GanttChartViewController", function ()
{

})
.controller("PerkChartViewController", function ()
{

});