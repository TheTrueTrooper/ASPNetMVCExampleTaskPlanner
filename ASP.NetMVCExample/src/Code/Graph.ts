/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To make a reusable graph base to extend
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: TypeScript
//  Writer/Publisher: Microsoft
//  Link: https://www.typescriptlang.org/
//  }
*/
import * as BasicVectors from "../Code/BasicVectors";
import * as js3D from "../TypeDeffs/3Djs";

export namespace Graphing
{
    export class Graph
    {
        // should it render on any change
        public _RerenderOnChange = true;
        // the SVG element to draw on as a Canvas
        protected _Canvas: js3D.Selection<any>;
        // the parents stuff
        protected _CanvasParent: js3D.Selection<any>;
        // so that we can change the unit value for X, Y (days, years, etc.)
        protected _UnitPxScale: BasicVectors.Vector2D;
        // so that we can scroll maybe a start point to shift things by
        protected _Origin: BasicVectors.Vector2D;
        // the size of view box we are rendering into
        protected _Size: BasicVectors.Vector2D;
        // to overload it should be set to OverloadRender with a void typodis
        // it should do the main render you could also draw axis here
        // wish I could type define this more but you cant function type or else I dont know how :'0 really should have one if you want to extend
        protected _VirtalRender = null;

        constructor(finder : string)
        {
            // the canvas box and the parents box
            this._CanvasParent = js3D.select(finder);
            this._Canvas = this._CanvasParent.append("svg");

            // get the size of the graph
            this._Size = new BasicVectors.Vector2D(Number(this._CanvasParent.attr("Width")), Number(this._CanvasParent.attr("Hieght")));
            this._Canvas
                .attr("width", this._Size.X)
                .attr("hieght", this._Size.Y);
            // set the basic start
            this._Origin = new BasicVectors.Vector2D(0, 0);
            // rest per graph after
            this._UnitPxScale = new BasicVectors.Vector2D(0, 0);
        }

        // Set the origin to draw from
        public SetOrigin(x: number, y: number): void
        {
            this._Origin = new BasicVectors.Vector2D(x, y);
            if (this._RerenderOnChange)
                this.Render();
        }

        // Set the scale
        public SetOriginV(Location: BasicVectors.Vector2D): void
        {
            this._Origin = Location;
            if (this._RerenderOnChange)
                this.Render();
        }

        // Set the scale
        public SetScale(x: number, y: number): void
        {
            this._UnitPxScale = new BasicVectors.Vector2D(x, y);
            if (this._RerenderOnChange)
                this.Render();
        }

        // Set the scale
        public SetScaleV(Scale : BasicVectors.Vector2D): void
        {
            this._UnitPxScale = Scale;
            if (this._RerenderOnChange)
                this.Render();
        }

        // Sets the size
        public SetSize(x: number, y: number): void
        {
            this._Size = new BasicVectors.Vector2D(x, y);
            this._Canvas
                .attr("width", this._Size.X)
                .attr("hieght", this._Size.Y);
            if (this._RerenderOnChange)
                this.Render();
        }

        // Set the size
        public SetSizeV(Size: BasicVectors.Vector2D): void
        {
            this._Size = Size;
            if (this._RerenderOnChange)
                this.Render();
        }

        // Retruns the size
        public GetSize(): BasicVectors.Vector2D
        {
            return this._Size;
        }

        // Returns the orgin to draw at
        public GetOrigin()
        {
            return this._Origin;
        }

        // Gets the parent
        public GetParent()
        {
            return this._CanvasParent;
        } 

        // Called to call the virtual Render or a Non-virtual interface pattern INV call system for use in rendering
        public Render(): void
        {
             this._VirtalRender();
        }

        // Clears out the rendered nodes so that you dcan redre
        public ClearRender(): void
        {
            // Get the canvas node and clear it
            var CanvNode = this._Canvas.node();
            while (CanvNode.childNodes.length > 0)
            {
                CanvNode.removeChild(CanvNode.firstChild);
            }
        }

        // Used like a deconstructor Call this and return it to your reffernce to the object for GC.
        public KillThis()
        {
            this._CanvasParent.node().removeChild(this._Canvas.node());
            this._Canvas = null;
            return null;
        }
    }

    

}
