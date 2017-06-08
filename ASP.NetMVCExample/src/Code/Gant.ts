/// <reference path="../typedeffs/moment.d.ts" />
import * as MoDate from "../typedeffs/moment.d.ts";

module GantChart
{
    class Vector2D
    {
        //A simple X cord
        public X: number;
        //A simple Y cord
        public Y: number;

        constructor(X: number, Y: number)
        {
            this.X = X;
            this.Y = Y;
        }

        public Add(Vector: Vector2D)
        {
            this.X += Vector.X;
            this.Y += Vector.Y;
        }

        public Sub(Vector: Vector2D)
        {
            this.X -= Vector.X;
            this.Y -= Vector.Y;
        }

        public Scale(Number: number)
        {
            this.X *= Number;
            this.Y *= Number;
        }

        public ToString(): string
        {
            return this.X.toString() + "," + this.Y.toString() + " ";
        }
    }

    class Graph
    {
        //the SVG element to draw on as a Canvas
        protected _Canvas: SVGSVGElement;
        // so that we can change the unit value for X, Y (days, years, etc.)
        protected _UnitPxScale: Vector2D;
        // so that we can scroll maybe a start point to shift things by
        protected _StartPoint: Vector2D;
        // the size of view box we are rendering into
        protected _Size: Vector2D;
        // to overload it should be set to OverloadRender with a void typodis
        // it should do the main render you could also draw axis here
        // wish I could type define this more but you cant function type or else I dont know how :'0 really should have one if you want to extend
        protected _VirtalRender = null;

        constructor(Canvas: SVGSVGElement)
        {
            this._Canvas = Canvas;
            this._Size = new Vector2D(Canvas.getBoundingClientRect().width, Canvas.getBoundingClientRect().height);
            this._StartPoint = new Vector2D(0, 0);
            //rest per graph after
            this._UnitPxScale = new Vector2D(50, 50);
        }

        public Render(): void
        {
            if (this._VirtalRender != null)
                this._VirtalRender();
        }
    }

    class GantChart extends Graph
    {
        // This Is used with the unit scale
        public _AxisTickScale: Vector2D;

        public _Chains: Linker[];

        constructor(Canvas: SVGSVGElement)
        {
            super(Canvas);
            this._AxisTickScale = new Vector2D(5, 1);
            this._UnitPxScale = new Vector2D(50, 50);
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

    class Task
    {
        private _TaskName: string;
        private _StartDate: MoDate.Moment;
        private _EndDate: MoDate.Moment;
        private _TimeLength: MoDate.Duration;

        constructor(name: string, StartDate: MoDate.Moment, EndDate: MoDate.Moment)
        {
            this._TaskName = name;
            this._StartDate = StartDate;
            this._EndDate = EndDate;
            this._TimeLength = MoDate.duration(EndDate.diff(StartDate));
        }

        public ConstructFromDuration(name: string, StartDate: MoDate.Moment, Duration: MoDate.Duration): Task
        {
            return new Task(name, StartDate, StartDate.add(Duration));
        }

        public Render(Canvas: SVGSVGElement): void
        {
            

        }

    }

    class Linker
    {
        private _StartTask: Task[];
        private _EndTask: Task[];
        
        constructor(StartTask: Task[], EndTask: Task[])
        {
            this._StartTask = StartTask;
            this._EndTask = EndTask; 
        }

        public Render(Canvas: SVGSVGElement): void
        {
            this._StartTask.forEach(x =>
            {
                x.Render(Canvas);
            });
        }
    }

}
