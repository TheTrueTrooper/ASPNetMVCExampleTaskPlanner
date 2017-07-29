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
import * as BasicVectors from "../Code/BasicVectors.ts";

export namespace Graphing
{
    export class Graph
    {
        //the SVG element to draw on as a Canvas
        protected _Canvas: SVGSVGElement;
        // so that we can change the unit value for X, Y (days, years, etc.)
        protected _UnitPxScale: BasicVectors.Vector2D;
        // so that we can scroll maybe a start point to shift things by
        protected _StartPoint: BasicVectors.Vector2D;
        // the size of view box we are rendering into
        protected _Size: BasicVectors.Vector2D;
        // to overload it should be set to OverloadRender with a void typodis
        // it should do the main render you could also draw axis here
        // wish I could type define this more but you cant function type or else I dont know how :'0 really should have one if you want to extend
        protected _VirtalRender = null;

        constructor(Canvas: SVGSVGElement)
        {
            this._Canvas = Canvas;
            //get the size of the graph
            this._Size = new BasicVectors.Vector2D(Canvas.getBoundingClientRect().width, Canvas.getBoundingClientRect().height);
            //set the basic start
            this._StartPoint = new BasicVectors.Vector2D(0, 0);
            //rest per graph after
            this._UnitPxScale = new BasicVectors.Vector2D(50, 50);
        }

        //called to call the virtual Render or a Non-virtual interface pattern INV call system for use in rendering
        public Render(): void
        {
             this._VirtalRender();
        }
    }

}
