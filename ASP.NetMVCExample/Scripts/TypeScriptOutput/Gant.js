"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Graphing = require("../Code/Graph.ts");
var TimeManagementContainers = require("../Code/TimeManagementContainers.ts");
var BasicVectors = require("../Code/BasicVectors.ts");
var GantGraphing;
(function (GantGraphing) {
    var GantTask = (function (_super) {
        __extends(GantTask, _super);
        function GantTask(name, StartDate, EndDate) {
            _super.call(this, name, StartDate, EndDate);
            this._VirtualRender = this.OverloadRender;
        }
        GantTask.prototype.OverloadRender = function (Canvas) {
        };
        return GantTask;
    }(TimeManagementContainers.Task));
    GantGraphing.GantTask = GantTask;
    var GantLinker = (function (_super) {
        __extends(GantLinker, _super);
        function GantLinker(StartTask, EndTask) {
            _super.call(this, StartTask, EndTask);
            this._VirtualRender = this.OverloadRender;
        }
        GantLinker.prototype.OverloadRender = function (Canvas) {
            //Render all lines
            //then render all tasks accordingly
            this._StartTask.forEach(function (x) { return x.Render(Canvas); });
            this._EndTask[this._EndTask.length - 1].Render(Canvas);
        };
        return GantLinker;
    }(TimeManagementContainers.Linker));
    GantGraphing.GantLinker = GantLinker;
    var GantChart = (function (_super) {
        __extends(GantChart, _super);
        function GantChart(Canvas) {
            _super.call(this, Canvas);
            this._AxisTickScale = new BasicVectors.Vector2D(5, 1);
            this._UnitPxScale = new BasicVectors.Vector2D(50, 50);
            this._VirtalRender = this.OverloadRender;
        }
        GantChart.prototype.OverloadRender = function () {
            var _this = this;
            this._Chains.forEach(function (x) {
                x.Render(_this._Canvas);
            });
        };
        return GantChart;
    }(Graphing.Graphing.Graph));
    GantGraphing.GantChart = GantChart;
})(GantGraphing = exports.GantGraphing || (exports.GantGraphing = {}));
//# sourceMappingURL=Gant.js.map