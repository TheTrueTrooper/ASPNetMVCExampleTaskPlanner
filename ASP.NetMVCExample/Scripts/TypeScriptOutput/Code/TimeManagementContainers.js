"use strict";
/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management
//File Goal: To make some reuseable java containers to use in graphing time objects
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources:
//  {
//  Name: TypeScript
//  Writer/Publisher: Microsoft
//  Link: https://www.typescriptlang.org/
//  }
*/
var MoDate = require("../typedeffs/moment");
//A class that encapulates a Task
var Task = (function () {
    //constructor used to make a task from a set of moments and a name
    function Task(name, StartDate, EndDate) {
        this._TaskName = name;
        this._StartDate = StartDate;
        this._EndDate = EndDate;
        this._TimeLength = MoDate.duration(EndDate.diff(StartDate));
    }
    // a Extra static funtion that is used to make a Task from a Duration
    Task.ConstructFromDuration = function (name, StartDate, Duration) {
        return new Task(name, StartDate, StartDate.add(Duration));
    };
    //called to call the virtual Render or a Non-virtual interface pattern INV call system for use in rendering
    Task.prototype.Render = function (Canvas) {
        this._VirtualRender(Canvas);
    };
    return Task;
}());
exports.Task = Task;
//a class that encaplates a Realational connection between tasks
var Linker = (function () {
    //a simple constructor used to make taskLinkers from a set of tasks
    function Linker(StartTask, EndTask) {
        this._StartTask = StartTask;
        this._EndTask = EndTask;
    }
    //called to call the virtual Render or a Non-virtual interface pattern INV call system for use in rendering
    Linker.prototype.Render = function (Canvas) {
        this._VirtualRender(Canvas);
        this._StartTask.forEach(function (x) {
            x.Render(Canvas);
        });
    };
    return Linker;
}());
exports.Linker = Linker;
//# sourceMappingURL=TimeManagementContainers.js.map