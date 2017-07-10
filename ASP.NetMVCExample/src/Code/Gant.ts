///<reference path="graph.ts" />
import * as MoDate from "../typedeffs/moment.d.ts";
import * as Graphing from "../Code/Graph.ts";
import * as TimeManagementContainers from "../Code/TimeManagementContainers.ts";

export module GantGraphing
{
    export class GantTask extends TimeManagementContainers.TimeManagementContainers.Task
    {
        constructor(name: string, StartDate: MoDate.Moment, EndDate: MoDate.Moment)
        {
            super(name, StartDate, EndDate)
            this._VirtualRender = this.OverloadRender;
        }

        public OverloadRender(Canvas: SVGSVGElement): void
        {
        }
    }

    export class GantLinker extends TimeManagementContainers.TimeManagementContainers.Linker
    {
        constructor(StartTask: GantTask[], EndTask: GantTask[])
        {
            super(StartTask, EndTask)
            this._VirtualRender = this.OverloadRender;
        }
        
        public OverloadRender(Canvas: SVGSVGElement): void
        {
        }
    }

    export class GantChart extends Graphing.Graphing.Graph
    {
        // This Is used with the unit scale
        public _AxisTickScale: Graphing.Graphing.Vector2D;

        public _Chains: GantLinker[];

        constructor(Canvas: SVGSVGElement)
        {
            super(Canvas);
            this._AxisTickScale = new Graphing.Graphing.Vector2D(5, 1);
            this._UnitPxScale = new Graphing.Graphing.Vector2D(50, 50);
            this._VirtalRender = this.OverloadRender;
        }

        public OverloadRender(): void
        {
            this._Chains.forEach(x =>
            {
                x.Render(this._Canvas);

            })
        }
    }
}