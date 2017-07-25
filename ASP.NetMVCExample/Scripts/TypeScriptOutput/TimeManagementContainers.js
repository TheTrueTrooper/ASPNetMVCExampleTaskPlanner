"use strict";
/// <reference path="basicvectors.ts" />
var MoDate = require("../typedeffs/moment.d.ts");
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