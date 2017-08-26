angular.module("NGDashBoard", [])
.service("ProjectsService", function ($http)
{
    return {
        GetProjects: function()
        {
            return $http.get("/UtilitiesAPI/GetUsersProjectData", { responseType: "json" })
        }
    }
})
.controller("ProjectList", function ($scope, ProjectsService)
{
    ProjectsService.GetProjects().then(function (result)
    {
        $scope.projects = result.data;
        $scope.projects.forEach(function (Item)
        {
            Item.StartDate = moment(Item.StartDate);
            if (Item.EndDate)
            Item.EndDate = moment(Item.EndDate);
        })
    });
})