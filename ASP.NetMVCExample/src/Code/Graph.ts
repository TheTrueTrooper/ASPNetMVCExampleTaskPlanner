/// <reference path="../typedeffs/moment.d.ts" />

export namespace Graphing
{

    export class Vector2D
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

    export class Graph
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
             this._VirtalRender();
        }
    }

}
