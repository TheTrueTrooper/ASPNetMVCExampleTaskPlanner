/// <reference path="../../3DJS.js" />

function Gantt(IDParentSelector, DataToBuild)
{
    this.Data = d3.json(DataToBuild);

    this._Parent = $(IDParentSelector);

    this._Canvas = d3.select(this._parent).append("SVG");

    //this.Height = (set)=>{ 
    //    if(!set)
    //        return this._Parent.style.width; 
    //    this._Parent.style.width = set;
    //    //this._Canvas.attr("height", this.Height());// may not be needed as par resize event
    //};
    //this.Width = (set)=>{ 
    //    if(!set)
    //        return this._Parent.style.height; 
    //    this._Parent.style.height = set;
    //    // set the canvas to the correct width
    //    //this._Canvas.attr("width", this.Width());//may not be needed
    //};

    this._Canvas
        .attr("width", this.Width())
        .attr("height", this.Height());

    this._Zoom = d3.zoom();

    this.Draw = function (NewData)
    {
        const YSpacing = 50;
        const XSpacing = 50;


        if (!NewData)
            this.Data = NewData;



        this._Canvas
            .selectAll("g")
            .remove();
        var groups =
            this._Canvas
            .datum(Data)
            .sort((a, b) =>{ })
            .enter()
            // start an group to set up a group
            .append("g")
            .attr("id", (d, i) =>{ "ID#" + d.TaskID + "Draw#" + i });
            //draw a rect
        groups
            .append("rect")
            .attr("x", XSpacing)
            .attr("y", (d, i) =>{ return i * YSpacing })
            .attr("width", 50)
            .attr("height", 50)
            .attr("rx", XSpacing)
            .attr("ry", YSpacing)
            .attr("fill", "#00ff00");
            //draw a path for actual time data
        //groups
        //    .append("path")
        //    .attr("d", (d, i)=>
        //      { 
        //          return "M " + XSpacing + " " + (i * YSpacing) + l
        //      })
        //    .attr("stroke", "black")
        //    .attr("fill", "transparent");


        groups
            .append("text")
            .attr("font-family", "Verdana")
            .attr("x", XSpacing)
            .attr("y", (d, i) =>{ return i * YSpacing })
            .attr("font-size", YSpacing)
            .attr("fill", "#000088")
            .attr("stroke", "#000000")
            .text((d) =>{ d.Description });
        
    }

    //this._Parent.resize(() =>{ this.Height(this._Parent.style.height); this.Width(this._Parent.style.width) })


}

