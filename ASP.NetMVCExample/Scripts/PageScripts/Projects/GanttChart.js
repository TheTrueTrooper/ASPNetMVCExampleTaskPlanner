/// <reference path="../../3DJS.js" />
/*
public int ChainNumber { get; set; }
        [Required]
        public int NumberInChain { get; set; }
        public int TaskID { get; set; }
        [Required]
        public Nullable<int> SubContractorID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TaskTypeID { get; set; }
        public System.TimeSpan Duration { get; set; }
        public Nullable<System.DateTime> ActualStartDate { get; set; }
        public Nullable<System.DateTime> ActualEndDate { get; set; }
        public System.TimeSpan ActualDuration {
            get {
                //If we have a start date start using it to calculate agenst the end date. If not well we have not even started so the actual durration must be 0.
                return new TimeSpan(ActualStartDate == null || ActualStartDate.HasValue
                    //If the end date has been used apply it agnst start. if not assume now.
                    ? (ActualEndDate == null || ActualEndDate.HasValue
                        ? ActualEndDate.Value.Ticks - ActualStartDate.Value.Ticks
                        : DateTime.UtcNow.Ticks - ActualStartDate.Value.Ticks)
                    : 0);
            }
        }
        [Required]
        public System.DateTime CreationDate { get; set; }
        [Required]
        public List<int> NextTask { get; set; }
        [Required]
        public List<int> PrevTask { get; set; }
*/



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
        .attr("width", /*this.Width()*/100)
        .attr("height", /*this.Height()*/100);

    this._Zoom = d3.zoom();

    this.Draw = function (NewData)
    {
        const YSpacing = 50;
        const XSpacing = 50;
        const TextFont = "Verdana";

        if (!NewData)
            this.Data = NewData;

        

        this._Canvas
            .selectAll("g")
            .remove();

        var groups =
            this._Canvas
            .selectAll("g")
            .data(Data)
            .sort((a, b) =>{ })
            .enter()
            // start an group to set up a group
            .append("g")
            .attr("id", (d, i) => { "ID" + d.TaskID + "Draw" + i; })
            .each(function (d, i) { //ledgen

                var group = $("#ID" + d.TaskID + "Draw" + i);

                //draw a rect for the estimated time
                group
                    .append("rect")
                    .attr("x", XSpacing)
                    .attr("y", i * YSpacing )
                    .attr("width",  d.Duration * XSpacing)
                    .attr("height", YSpacing)
                    .attr("rx", 10)
                    .attr("ry", 10)
                    .attr("fill", "#00ff00");
                //draw a path for actual time data
                group
                    .append("rect")
                    .attr("x", XSpacing)
                    .attr("y", i * YSpacing )
                    .attr("width", d.ActualDuration * XSpacing)
                    .attr("height", YSpacing / 2)
                    .attr("rx", 5)
                    .attr("ry", 5)
                    .attr("fill", d.ActualDuration < d.Duration ? "#0000ff" : "#ff0000");
                //draw The line to next task 

                //add the hover over for the tool tips
                group.addEventListener("onmousemove", function (event) {

                    var text = "Description: " + d.Description
                        + "<br>Expected Duration: " + d.Duration
                        + "<br>Expected start: " + "Not implented"
                        + "<br>Expected End: " + "Not implented"
                        + "<br>Actual Duration: " + d.Duration
                        + "<br>Actual Start: " + d.ActualStart
                        + "<br>Actual End: " + d.ActualEnd
                        + "<br>Previous Tasks: " + "Not implented"
                        + "<br>Next Tasks: " + "Not implented";

                    var size = function () {
                        _Canvas.getContext("2d");
                        //context.font = TextFont;
                        var metrics = context.measureText(text);
                        return { x: metrics.width, y: metrics.height }
                    }

                    this.ToolTip = this.append("g")
                    this.ToolTip
                        .append("rect")
                        .attr("x", event.clientX)
                        .attr("y", event.clientY)
                        .attr("width", size().x)
                        .attr("height", size().y)

                });
                group.addEventListener("mouseout", function () {
                    this.remove(this.ToolTip);
                    this.ToolTip = null;
                });

            });
        
    }

    //this._Parent.resize(() =>{ this.Height(this._Parent.style.height); this.Width(this._Parent.style.width) })


}

