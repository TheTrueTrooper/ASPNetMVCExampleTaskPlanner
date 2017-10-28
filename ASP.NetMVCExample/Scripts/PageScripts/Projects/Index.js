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

function ChangeActiveTab(Tab)
{
    $(".active").removeClass("active");
    $("#" + Tab).addClass("active");
}

var ID;

angular.module("NGProjectsIndex", ["ngRoute"])
.config(["$routeProvider", function ($routeProvider)
{
    ID = $("[ProjectID]").attr("ProjectID");

    $routeProvider
        .when("/OverView",
        {
            templateUrl: "http://localhost:62740/Project/IndexSubPage?AngularView=OverView",
            controller: "OverViewController"
        })
        .when("/TaskCellView",
        {
            templateUrl: "http://localhost:62740/Project/IndexSubPage?AngularView=TaskCellView",
            controller: "TaskCellViewController"
        })
        .when("/GanttChartView",
        {
            templateUrl: "http://localhost:62740/Project/IndexSubPage?AngularView=GanttChartView",
            controller: "GanttChartViewController"
        })
        .when("/PerkChartView",
        {
            templateUrl: "http://localhost:62740/Project/IndexSubPage?AngularView=PerkChartView",
            controller: "PerkChartViewController"
        })
}])
//.run(function()
//{
//})
.service("ProjectsGetterService", function ($http)
{
    return {
        GetProjectOverview: function (ID)
        {
            return $http.get("http://localhost:62740/UtilitiesAPI/ProjectData?ID=" + ID, { responseType: "json" });
        },
        GetProjectTasks: function (ID)
        {
            return $http.get("http://localhost:62740/UtilitiesAPI/ProjectsTaskData?ID=" + ID, { responseType: "json" });
        }
    }
})
.controller("OverViewController", function ($scope, $sce, ProjectsGetterService)
{
    ChangeActiveTab("OverViewTab");

    ProjectsGetterService.GetProjectOverview(ID).then(function (result)
    {
        $scope.project = result.data;
        // avoid the sanitizer messing with data we will cheat here.
        $("#ManagerPicture").attr("src", "data:image;base64," + $scope.project.ManagerPicture)
        //for droping the space away if data is null and format if not null
        $scope.project.ManagerMiddleInitialIsNull = $scope.project.ManagerMiddleInitial !== null;
        $scope.project.ManagerMiddleInitial = $scope.project.ManagerMiddleInitial !== null ? "Middle Name: " + $scope.project.ManagerMiddleInitial : "";

        $scope.project.ManagerWorkPhoneIsNull = $scope.project.ManagerWorkPhone !== null;
        $scope.project.ManagerWorkPhone = $scope.project.ManagerWorkPhone !== null ? "Work Phone: " + $scope.project.ManagerWorkPhone.Insert(3, "-").Insert(7, "-") : "";

        $scope.project.ManagerCellPhoneIsNull = $scope.project.ManagerCellPhone !== null;
        $scope.project.ManagerCellPhone = $scope.project.ManagerCellPhone !== null ? "Cell Phone: " + $scope.project.ManagerCellPhone.Insert(3, "-").Insert(7, "-") : "";

        $scope.project.ManagerHomePhoneIsNull = $scope.project.ManagerHomePhone !== null;
        $scope.project.ManagerHomePhone = $scope.project.ManagerHomePhone !== null ? "Home Phone: " + $scope.project.ManagerHomePhone.Insert(3, "-").Insert(7, "-") : "";
    });

})
.controller("TaskCellViewController", function ($scope, ProjectsGetterService)
{
    ChangeActiveTab("TaskCellViewTab");

    //ProjectsGetterService.GetProjectTasks(ID).then(function (result){});
})
.controller("GanttChartViewController", function ($scope, ProjectsGetterService)
{
    ChangeActiveTab("GanttChartViewTab");
    ProjectsGetterService.GetProjectTasks(ID).then(function (result)
    { 
        $scope.project.tasks = result.data;
        $scope.Canvas = new Gantt("#CanvasBox", $scope.project.tasks);
    });
})
.controller("PerkChartViewController", function ($scope, ProjectsGetterService)
{
    ChangeActiveTab("PerkChartViewTab");

    //ProjectsGetterService.GetProjectTasks(ID).then(function (result){});
})
.filter('ArrayToBase64String', function ()
{
    return function (buffer)
    {

    };
});

