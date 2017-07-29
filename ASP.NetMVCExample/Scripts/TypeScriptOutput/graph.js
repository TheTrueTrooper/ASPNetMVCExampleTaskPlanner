"use strict";
/// <reference path="../typedeffs/moment.d.ts" />
var BasicVectors = require("../Code/BasicVectors.ts");
var Graphing;
(function (Graphing) {
    var Graph = (function () {
        function Graph(Canvas) {
            // to overload it should be set to OverloadRender with a void typodis
            // it should do the main render you could also draw axis here
            // wish I could type define this more but you cant function type or else I dont know how :'0 really should have one if you want to extend
            this._VirtalRender = null;
            this._Canvas = Canvas;
            //get the size of the graph
            this._Size = new BasicVectors.Vector2D(Canvas.getBoundingClientRect().width, Canvas.getBoundingClientRect().height);
            //set the basic start
            this._StartPoint = new BasicVectors.Vector2D(0, 0);
            //rest per graph after
            this._UnitPxScale = new BasicVectors.Vector2D(50, 50);
        }
        //called to call the virtual Render or a Non-virtual interface pattern INV call system for use in rendering
        Graph.prototype.Render = function () {
            this._VirtalRender();
        };
        return Graph;
    }());
    Graphing.Graph = Graph;
})(Graphing = exports.Graphing || (exports.Graphing = {}));
//# sourceMappingURL=graph.js.map