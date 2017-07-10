"use strict";
var MoDate = require("../typedeffs/moment.d.ts");
var TimeManagementContainers;
(function (TimeManagementContainers) {
    var Task = (function () {
        function Task(name, StartDate, EndDate) {
            this._TaskName = name;
            this._StartDate = StartDate;
            this._EndDate = EndDate;
            this._TimeLength = MoDate.duration(EndDate.diff(StartDate));
        }
        Task.prototype.ConstructFromDuration = function (name, StartDate, Duration) {
            return new Task(name, StartDate, StartDate.add(Duration));
        };
        Task.prototype.Render = function (Canvas) {
            this._VirtualRender(Canvas);
        };
        return Task;
    }());
    TimeManagementContainers.Task = Task;
    var Linker = (function () {
        function Linker(StartTask, EndTask) {
            this._StartTask = StartTask;
            this._EndTask = EndTask;
        }
        Linker.prototype.Render = function (Canvas) {
            this._VirtualRender(Canvas);
            this._StartTask.forEach(function (x) {
                x.Render(Canvas);
            });
        };
        return Linker;
    }());
    TimeManagementContainers.Linker = Linker;
})(TimeManagementContainers = exports.TimeManagementContainers || (exports.TimeManagementContainers = {}));
//# sourceMappingURL=TimeManagementContainers.js.map