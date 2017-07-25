///<reference path="graph.ts" />
import * as MoDate from "../typedeffs/moment.d.ts";
import * as Graphing from "../Code/Graph.ts";
import * as TimeManagementContainers from "../Code/TimeManagementContainers.ts";
import * as BasicVectors from "../Code/BasicVectors.ts";

export module GantGraphing
{
    // an extention of the task to allow specfic gant rendering of the data
    class GantTask extends TimeManagementContainers.Task
    {
        constructor(name: string, StartDate: MoDate.Moment, EndDate: MoDate.Moment)
        {
            super(name, StartDate, EndDate)
            //after a basic build set the overload accordingly
            this._VirtualRender = this.OverloadRender;
        }

        OverloadRender(Canvas: SVGSVGElement): void
        {
            
        }
    }

    // an extention of the linker to allow specfic gant rendering of the data
    class GantLinker extends TimeManagementContainers.Linker
    {
        constructor(StartTask: GantTask[], EndTask: GantTask[])
        {
            super(StartTask, EndTask)
            //after a basic build set the overload accordingly
            this._VirtualRender = this.OverloadRender;
        }
        
        OverloadRender(Canvas: SVGSVGElement): void
        {
            //Render all lines


            //then render all tasks accordingly
            this._StartTask.forEach(x => x.Render(Canvas));
            this._EndTask[this._EndTask.length - 1].Render(Canvas);
        }
    }

    //a class for drawing the gant chart using the Graphing.Graph base
    class GantChart extends Graphing.Graphing.Graph
    {
        // This Is used with the unit scale
        public _AxisTickScale: BasicVectors.Vector2D;

        //the Gant linkers to draw from
        public _Chains: GantLinker[];

        public constructor(Canvas: SVGSVGElement)
        {
            super(Canvas);
            //set some draw values
            this._AxisTickScale = new BasicVectors.Vector2D(5, 1);
            this._UnitPxScale = new BasicVectors.Vector2D(50, 50);
            //after a basic build set the overload accordingly
            this._VirtalRender = this.OverloadRender;
        }

        //the Over load render
        OverloadRender(): void
        {
            this._Chains.forEach(x =>
            {
                x.Render(this._Canvas);

            })
        }
    }
}