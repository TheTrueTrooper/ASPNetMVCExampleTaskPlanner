"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var Graphing = require("../Code/Graph");
var TimeManagementContainers = require("../Code/TimeManagementContainers");
var BasicVectors = require("../Code/BasicVectors");
var GantGraphing;
(function (GantGraphing) {
    // an extention of the task to allow specfic gant rendering of the data
    var GantTask = /** @class */ (function (_super) {
        __extends(GantTask, _super);
        function GantTask(name, StartDate, EndDate) {
            var _this = _super.call(this, name, StartDate, EndDate) || this;
            //after a basic build set the overload accordingly
            _this._VirtualRender = _this.OverloadRender;
            return _this;
        }
        GantTask.prototype.OverloadRender = function (Canvas) {
        };
        return GantTask;
    }(TimeManagementContainers.Task));
    // an extention of the linker to allow specfic gant rendering of the data
    var GantLinker = /** @class */ (function (_super) {
        __extends(GantLinker, _super);
        function GantLinker(StartTask, EndTask) {
            var _this = _super.call(this, StartTask, EndTask) || this;
            //after a basic build set the overload accordingly
            _this._VirtualRender = _this.OverloadRender;
            return _this;
        }
        GantLinker.prototype.OverloadRender = function (Canvas) {
            //Render all lines
            //then render all tasks accordingly
            this._StartTask.forEach(function (x) { return x.Render(Canvas); });
            this._EndTask[this._EndTask.length - 1].Render(Canvas);
        };
        return GantLinker;
    }(TimeManagementContainers.Linker));
    //a class for drawing the gant chart using the Graphing.Graph base
    var GantChart = /** @class */ (function (_super) {
        __extends(GantChart, _super);
        function GantChart(Canvas) {
            var _this = _super.call(this, Canvas) || this;
            //set some draw values
            _this._AxisTickScale = new BasicVectors.Vector2D(5, 1);
            _this._UnitPxScale = new BasicVectors.Vector2D(50, 50);
            //after a basic build set the overload accordingly
            _this._VirtalRender = _this.OverloadRender;
            return _this;
        }
        //the Over load render
        GantChart.prototype.OverloadRender = function () {
            var _this = this;
            this._Chains.forEach(function (x) {
                x.Render(_this._Canvas);
            });
        };
        return GantChart;
    }(Graphing.Graphing.Graph));
})(GantGraphing = exports.GantGraphing || (exports.GantGraphing = {}));
//# sourceMappingURL=Gant.js.map