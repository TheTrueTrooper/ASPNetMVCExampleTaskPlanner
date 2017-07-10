/// <reference path="../typedeffs/moment.d.ts" />
"use strict";
var Graphing;
(function (Graphing) {
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
    Graphing.Vector2D = Vector2D;
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
            this._VirtalRender();
        };
        return Graph;
    }());
    Graphing.Graph = Graph;
})(Graphing = exports.Graphing || (exports.Graphing = {}));
//# sourceMappingURL=graph.js.map