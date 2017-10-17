"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Graphing = require("../Graph");
var TestGraph = (function (_super) {
    __extends(TestGraph, _super);
    function TestGraph(finder) {
        _super.call(this, finder);
        //after a basic build set the overload accordingly
        this._VirtalRender = this.OverloadRender;
        this.Render();
    }
    TestGraph.prototype.OverloadRender = function (Canvas) {
        // clear the render 
        this.ClearRender();
        // check the box size
        this._Canvas.append("rect")
            .attr("fill", "green")
            .attr("width", this.GetSize().X)
            .attr("height", this.GetSize().Y);
        //draw a circle in it
        this._Canvas.append("circle")
            .attr("fill", "yellow")
            .attr("cx", 10)
            .attr("cy", 10)
            .attr("r", 10);
    };
    return TestGraph;
}(Graphing.Graphing.Graph));
//# sourceMappingURL=TestGraph.js.map