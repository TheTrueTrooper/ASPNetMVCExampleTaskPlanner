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
var Graphing = require("../Graph");
var TestGraph = /** @class */ (function (_super) {
    __extends(TestGraph, _super);
    function TestGraph(finder) {
        var _this = _super.call(this, finder) || this;
        //after a basic build set the overload accordingly
        _this._VirtalRender = _this.OverloadRender;
        _this.Render();
        return _this;
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