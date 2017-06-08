"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
/// <reference path="../typedeffs/moment.d.ts" />
var MoDate = require("../typedeffs/moment.d.ts");
var GantChart;
(function (GantChart_1) {
    var Vector2D = (function () {
        function Vector2D(X, Y) {
            this.X = X;
            this.Y = Y;
        }
        Vector2D.prototype.Add = function (Vector) {
            this.X += Vector.X;
            this.Y += Vector.Y;
        };
        Vector2D.prototype.Sub = function (Vector) {
            this.X -= Vector.X;
            this.Y -= Vector.Y;
        };
        Vector2D.prototype.Scale = function (Number) {
            this.X *= Number;
            this.Y *= Number;
        };
        Vector2D.prototype.ToString = function () {
            return this.X.toString() + "," + this.Y.toString() + " ";
        };
        return Vector2D;
    }());
    var Graph = (function () {
        function Graph(Canvas) {
            // to overload it should be set to OverloadRender with a void typodis
            // it should do the main render you could also draw axis here
            // wish I could type define this more but you cant function type or else I dont know how :'0 really should have one if you want to extend
            this._VirtalRender = null;
            this._Canvas = Canvas;
            this._Size = new Vector2D(Canvas.getBoundingClientRect().width, Canvas.getBoundingClientRect().height);
            this._StartPoint = new Vector2D(0, 0);
            //rest per graph after
            this._UnitPxScale = new Vector2D(50, 50);
        }
        Graph.prototype.Render = function () {
            if (this._VirtalRender != null)
                this._VirtalRender();
        };
        return Graph;
    }());
    var GantChart = (function (_super) {
        __extends(GantChart, _super);
        function GantChart(Canvas) {
            _super.call(this, Canvas);
            this._AxisTickScale = new Vector2D(5, 1);
            this._UnitPxScale = new Vector2D(50, 50);
            this._VirtalRender = this.OverloadRender;
        }
        GantChart.prototype.OverloadRender = function () {
            var _this = this;
            this._Chains.forEach(function (x) {
                x.Render(_this._Canvas);
            });
        };
        return GantChart;
    }(Graph));
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
        };
        return Task;
    }());
    var Linker = (function () {
        function Linker(StartTask, EndTask) {
            this._StartTask = StartTask;
            this._EndTask = EndTask;
        }
        Linker.prototype.Render = function (Canvas) {
            this._StartTask.forEach(function (x) {
                x.Render(Canvas);
            });
        };
        return Linker;
    }());
})(GantChart || (GantChart = {}));
//# sourceMappingURL=Gant.js.map