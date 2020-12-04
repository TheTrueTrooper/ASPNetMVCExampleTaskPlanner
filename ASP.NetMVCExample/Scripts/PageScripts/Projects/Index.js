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

function ToBase64Picture(byteArray) {
    return btoa(String.fromCharCode.apply(null, byteArray));
}


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
    ID = $("[ProjectID]").attr("ProjectID");
})
.config(["$routeProvider", function ($routeProvider)
{

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
            });
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
        GetProjectOverview: function (ID) {
            return $http.get("http://localhost:62740/ProjectAPI/ProjectData?ID=" + ID, { responseType: "json" });
        },
        GetProjectTasks: function (ID) {
            return $http.get("http://localhost:62740/ProjectAPI/ProjectsTaskData?ID=" + ID, { responseType: "json" });
        },
        CreateNewProjectTask(Data) {
            return $http.post("http://localhost:62740/ProjectAPI/ProjectsCreateTask" + ID, Data, { responseType: "json" });
        }
    };
})
.controller("OverViewController", function ($scope, $sce, ProjectsGetterService)
{
    ChangeActiveTab("OverViewTab");

    ProjectsGetterService.GetProjectOverview(ID).then(function (result)
    {
        $scope.project = result.data;

        // avoid the sanitizer messing with data we will cheat here.
        $("#ManagerPicture").attr("src", "data:image;base64, " + ToBase64Picture($scope.project.ManagerPicture));
        //for droping the space away if data is null and format if not null
        $scope.project.ManagerMiddleInitialIsNull = $scope.project.ManagerMiddleInitial !== null;
        $scope.project.ManagerMiddleInitial = $scope.project.ManagerMiddleInitial !== null ? "Middle Name: " + $scope.project.ManagerMiddleInitial : "";

        //$scope.project.ManagerWorkPhoneIsNull = $scope.project.ManagerWorkPhone !== null;
        //$scope.project.ManagerWorkPhone = $scope.project.ManagerWorkPhone !== null ? "Work Phone: " + $scope.project.ManagerWorkPhone.Insert(3, "-").Insert(7, "-") : "";

        //$scope.project.ManagerCellPhoneIsNull = $scope.project.ManagerCellPhone !== null;
        //$scope.project.ManagerCellPhone = $scope.project.ManagerCellPhone !== null ? "Cell Phone: " + $scope.project.ManagerCellPhone.Insert(3, "-").Insert(7, "-") : "";

        //$scope.project.ManagerHomePhoneIsNull = $scope.project.ManagerHomePhone !== null;
        //$scope.project.ManagerHomePhone = $scope.project.ManagerHomePhone !== null ? "Home Phone: " + $scope.project.ManagerHomePhone.Insert(3, "-").Insert(7, "-") : "";
    });

})
.controller("TaskCellViewController", function ($scope, $sce, ProjectsGetterService)
{
    ChangeActiveTab("TaskCellViewTab");

    $scope.PrintListAsOptions = function (AllItems, Task) {
        var str = "";
        AllItems.forEach(function (Item) {
            //if (LinkedItems.includes(Item.TaskID))
            //    str += "<option selected value=\"" + Item.TaskID + "\">" + Item.TaskID + "</option>\n";
            //else
            if (Item.TaskID !== Task.TaskID)
                str += "<option value=\"" + Item.TaskID + "\">" + Item.TaskID + "</option>\n";

        });

        return $sce.trustAsHtml(str);
    };

    ///Takes one array and finds the values unique to the caller vs the input
    Array.prototype.except = function (val, bIgnoreWarrning = true) {
        var Return = [];
        if (!Array.isArray(val))
        {
            val = [val];
            if (!bIgnoreWarrning)
                Console.log("Warrning you have passed a non array to an array except ")
        }
        for (var x = 0; x < this.length; x++) {
            var bIsFound = false;
            for (var y = 0; y < val.length; y++) {
                if (this[x] === val[y]) {
                    bIsFound = true;
                    break;
                }
            }
            if (!bIsFound)
                Return.push(this[x]);
        }
        return Return;
    }; 

    Array.prototype.pushUnique = function (val) {
        var bIsFound = false;
        for (var x = 0; x < this.length; x++) {
            if (this[x] === val) {
                bIsFound = true;
                break;
            }
        }
        if (!bIsFound)
            this.push(val);
    }; 

    Array.prototype.removeValue = function (val) {
        for (var x = 0; x < this.length; x++) {
            if (this[x] === val)
            {
                this.splice(x, 1);
            }
        }
    };

    $scope.ChangeNextTasks = function (Task) {
        //Find all of the changes in the next task
        var Changes = {
            AddedTasks: Task.NextTask.except(Task.LastNextTask),
            RemovedTasks: Task.LastNextTask.except(Task.NextTask)
        };
        //Set our old state so we can find the changes again later
        Task.LastNextTask = Task.NextTask;
        //find and update all of the tasks that need to be updated and up date them
        Changes.AddedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x) - 1].PrevTask.pushUnique(Task.TaskID);
            $scope.Tasks[parseInt(x) - 1].LastPrevTask.pushUnique(Task.TaskID);
        });
        Changes.RemovedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x) - 1].PrevTask.removeValue(Task.TaskID);
            $scope.Tasks[parseInt(x) - 1].LastPrevTask.removeValue(Task.TaskID);
        });
    };

    $scope.ChangePrevTasks = function (Task) {
        // clone of above but with the op Targets
        var Changes = {
            AddedTasks: Task.PrevTask.except(Task.LastPrevTask),
            RemovedTasks: Task.LastPrevTask.except(Task.PrevTask)
        };
        Task.LastPrevTask = Task.PrevTask;
        Changes.AddedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x) - 1].NextTask.pushUnique(Task.TaskID);
            $scope.Tasks[parseInt(x) - 1].LastNextTask.pushUnique(Task.TaskID);
        });
        Changes.RemovedTasks.forEach(function (x) {
            $scope.Tasks[parseInt(x) - 1].NextTask.removeValue(Task.TaskID);
            $scope.Tasks[parseInt(x) - 1].LastNextTask.removeValue(Task.TaskID);
        });

    };

    $scope.PrintList = function (input)
    {
        var OutPut = "";
        input.forEach(function (Item) {
            OutPut += Item + ", ";
        });
        if (OutPut.length > 0)
            OutPut = OutPut.slice(0, OutPut.length -2);
        return OutPut;
    };

    $scope.SubmitData = function ()
    {
        if ($("#CreateTaskForm").hasClass("ng-invalid"))
        {
            alert("invalid Number entered");
            return;
        }

        return;
    };

    $scope.Tasks =
        [{
            TaskID: 1,
            NextTask: [],//[1, 2 , 3, 5, 6],
            LastNextTask: [],//[1, 2, 3, 5, 6],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3, 5, 6],
            LastPrevTask: []//[1, 2, 3, 5, 6]
        },
        {
            TaskID: 2,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1,2,3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 3,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 4,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 5,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 6,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 7,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        }
        ];
})
.controller("GanttChartViewController", function ($scope, ProjectsGetterService)
{
    ChangeActiveTab("GanttChartViewTab");
    //ProjectsGetterService.GetProjectTasks(ID).then(function (result)
    //{ 
    //    $scope.project.tasks = result.data;
    //    $scope.Canvas = new Gantt("#CanvasBox", $scope.project.tasks);
    //});
    $scope.Tasks =
        [{
            TaskID: 1,
            NextTask: [2],//[1, 2 , 3, 5, 6],
            LastNextTask: [],//[1, 2, 3, 5, 6],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3, 5, 6],
            LastPrevTask: []//[1, 2, 3, 5, 6]
        },
        {
            TaskID: 2,
            NextTask: [3],//[1, 2, 3],
            LastNextTask: [],//[1,2,3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 2,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [1],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 3,
            NextTask: [4],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 3,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [2, 7],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 4,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 5,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 1,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 6,
            NextTask: [],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 5,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        },
        {
            TaskID: 7,
            NextTask: [3],//[1, 2, 3],
            LastNextTask: [],//[1, 2, 3],
            Description: "stuff",
            SubContractorID: 1,
            TaskTypeID: 1,
            Duration: 6,
            ActualStartDate: "Jan 6",
            ActualEndDate: "Jan 6",
            ActualDuration: "6 Days",
            TaskCreationDate: "err",
            PrevTask: [],//[1, 2, 3],
            LastPrevTask: []//[1, 2, 3]
        }
        ];
    $scope.Canvas = new Gantt("#CanvasBox", $scope.Tasks);
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

