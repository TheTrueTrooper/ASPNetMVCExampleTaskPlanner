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

angular.module("NGProjectsIndex", ["ngRoute", "ngSanitize"])
.value("$", $)
.run(function ()
{
    ID = $();
})
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
.factory("socketService", function ($, $rootScope)
{
    //var hub = null
    //return {
    //    initialize: function ()
    //    {
    //        this.connection = $.connection.hubConnection();
    //        this.connection.qs = sessionStorage;
    //        this.hub = this.connection.createHubProxy("ganttEditHub");
    //        this.hub.on("onInItDone", function (Data) {
    //            $rootScope.Data = Data
    //        });
    //        this.connection.start()
    //            .done(function () { console.log('Now connected, connection ID=' + this.connection.id); })
    //            .fail(function () { console.log('Could not connect'); });
    //    },
    //    ServerCall: function (MethodName, Object)
    //    {
    //        this.hub.invoke(MethodName, Object).done(function () {
    //            console.log('Invocation of ' + MethodName + ' succeeded');
    //        }).fail(function (error) {
    //            console.log('Invocation of  ' + MethodName + '  failed. Error: ' + error);
    //        });
    //    }
    //}
})
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
.controller("TaskCellViewController", function ($scope, $sce, ProjectsGetterService)
{
    ChangeActiveTab("TaskCellViewTab");

    $scope.PrintListAsOptions = function (AllItems, LinkedItems) {
        var str = "";
        AllItems.forEach(function (Item) {
            //if (LinkedItems.includes(Item.TaskID))
            //    str += "<option selected value=\"" + Item.TaskID + "\">" + Item.TaskID + "</option>\n";
            //else
            str += "<option value=\"" + Item.TaskID + "\">" + Item.TaskID + "</option>\n";
            
        });

        return $sce.trustAsHtml(str);
    }

    Array.prototype.except = function (val) {
        var Return = [];
        if (!Array.isArray(val))
            throw new Error('Value passed to Except must be an array.');
        for (var x = 0; x < this.length; x++) {
            var bIsFound = false
            for (var y = 0; y < val.length; y++) {
                if (this[x] == val[y]) {
                    bIsFound = true;
                    break;
                }
            }
            if (!bIsFound)
                Return.push(this[x])
        }
        return Return;
    }; 

    Array.prototype.RemoveValue = function (val) {
        var Return = [];
        for (var x = 0; x < this.length; x++) {

            if (this[x] != val)
                Return.push(this[x])
        }
        return Return;
    };

    $scope.ChangeNextTasks = function (Task) {
        var Changes = {
            AddedTasks: Task.NextTask.except(Task.LastNextTask),
            RemovedTasks: Task.LastNextTask.except(Task.NextTask)
        };

        Changes.AddedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x)].PrevTask.push(x)
            $scope.Tasks[parseInt(x)].PrevTask.push(x)
        });
        Changes.AddedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x)].LastNextTask = $scope.Tasks[parseInt(x)].PrevTask.RemoveValue(x)
            $scope.Tasks[parseInt(x)].LastNextTask = $scope.Tasks[parseInt(x)].PrevTask.RemoveValue(x)
        });
    };

    $scope.ChangePrevTasks = function (Task) {
        var Changes = {
            AddedTasks: Task.NextTask.except(Task.LastNextTask),
            RemovedTasks: Task.LastNextTask.except(Task.NextTask)
        };

        Changes.AddedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x)].LastNextTask.push(x)
            $scope.Tasks[parseInt(x)].NextTask.push(x)
        });
        Changes.AddedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x)].LastNextTask = $scope.Tasks[parseInt(x)].LastNextTask.RemoveValue(x)
            $scope.Tasks[parseInt(x)].LastNextTask = $scope.Tasks[parseInt(x)].NextTask.RemoveValue(x)
        });

    };

    $scope.PrintList = function (input)
    {
        var OutPut = "";
        input.forEach(function (Item) {
            OutPut += Item + ", "
        });
        if (OutPut.length > 0)
            OutPut = OutPut.slice(0, OutPut.length -2);
        return OutPut;
    };

    $scope.Tasks =
        [{
            TaskID: 1,
            NextTask: [1, 2 , 3, 5, 6],
            LastNextTask: [1, 2, 3, 5, 6],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1, 2, 3, 5, 6],
            LastPrevTask: [1, 2, 3, 5, 6]
        },
        {
            TaskID: 2,
            NextTask: [1, 2, 3],
            LastNextTask: [1,2,3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1, 2, 3],
            LastPrevTask: [1, 2, 3]
        },
        {
            TaskID: 3,
            NextTask: [1, 2, 3],
            LastNextTask: [1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1, 2, 3],
            LastPrevTask: [1, 2, 3]
        },
        {
            TaskID: 4,
            NextTask: [1, 2, 3],
            LastNextTask: [1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1, 2, 3],
            LastPrevTask: [1, 2, 3]
        },
        {
            TaskID: 5,
            NextTask: [1, 2, 3],
            LastNextTask: [1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1, 2, 3],
            LastPrevTask: [1, 2, 3]
        },
        {
            TaskID: 6,
            NextTask: [1, 2, 3],
            LastNextTask: [1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1, 2, 3],
            LastPrevTask: [1, 2, 3]
        },
        {
            TaskID: 7,
            NextTask: [1, 2, 3],
            LastNextTask: [1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1, 2, 3],
            LastPrevTask: [1, 2, 3]
        }
        ];
})
.controller("GanttChartViewController", function ($scope, ProjectsGetterService, socketService)
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

