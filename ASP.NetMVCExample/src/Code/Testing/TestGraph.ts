import * as Graphing from "../Graph";
import * as BasicVectors from "../BasicVectors";
import * as js3D from "../../TypeDeffs/3Djs";


class TestGraph extends Graphing.Graphing.Graph
{
    constructor(finder: string)
    {
        super(finder);

        //after a basic build set the overload accordingly
        this._VirtalRender = this.OverloadRender;
        this.Render();
    }

    OverloadRender(Canvas: js3D.Selection<any>): void
    {
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
    }
}