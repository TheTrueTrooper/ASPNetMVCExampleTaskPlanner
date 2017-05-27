"use strict";
/// <reference path="../typedeffs/moment.d.ts" />
var MoDate = require("../typedeffs/moment.d.ts");
var Task = (function () {
    function Task(name, StartDate, EndDate) {
        this.Name = name;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.TimeLength = MoDate.duration(EndDate.diff(StartDate));
    }
    return Task;
}());
//# sourceMappingURL=Gant.js.map