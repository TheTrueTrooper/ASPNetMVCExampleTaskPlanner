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
    this.Data = DataToBuild;

    this._Parent = $(IDParentSelector);

    this._Canvas = d3.select(IDParentSelector).append("svg");


    //DATA cleaning
    var TempArray = [];
    this.Data.forEach((item, index) =>
    {
        item.PrevTask.forEach((ItemsPrevTask, index) =>
        {
            item.PrevTask[index] = this.Data.find(a => a.TaskID === ItemsPrevTask);
        });
        item.NextTask.forEach((ItemsNextTask, index) => {
            item.NextTask[index] = this.Data.find(a => a.TaskID === ItemsNextTask);
        });

    });

    function GetTaskTimeBefore(Data, Task, CurrentTotal) {
        if (CurrentTotal === null)
            CurrentTotal = 0;

        if (LongestPrevTask === null || Task.PrevTask.length === 0)
            return CurrentTotal;

        var LongestPrevTask = null;
        Task.PrevTask.forEach(
            (item, index) => {
                if (LongestPrevTask === null)
                    LongestPrevTask = item;
                else if (LongestPrevTask.Duration < item.Duration)
                    LongestPrevTask = item;
            });
        CurrentTotal += LongestPrevTask.Duration;
        return GetTaskTimeBefore(Data, LongestPrevTask, CurrentTotal);
    }

    function GetMarkTotalTimeOnRoute(Task) {
        var CurrentTotal = Task.TimeBefore + Task.Duration;
        if (typeof Task.RouteTotal === "undefined" || Task.RouteTotal === null || Task.RouteTotal < CurrentTotal)
            Task.RouteTotal = CurrentTotal;
        Task.PrevTask.forEach(
            (item, index) => {
                if (item.RouteTotal === null || item.RouteTotal < CurrentTotal)
                    item.RouteTotal = CurrentTotal;
            });
    }

    this.Data.forEach((item, index) => {
        item.TimeBefore = GetTaskTimeBefore(this.Data, item, null);
        GetMarkTotalTimeOnRoute(item);
    });

    this.Data.sort((x, y) => { return d3.ascending(x.RouteTotal, y.RouteTotal) || d3.ascending(x.TimeBefore, y.TimeBefore) || d3.ascending(x.Duration, y.Duration); });

    //begin drawing

    var Width = this._Parent.width();
    var Height = this._Parent.height();

    this._Canvas
        .attr("width", /*this.Width()*/Width)
        .attr("height", /*this.Height()*/Height)
        .style("background-color", "#EEEEFF");
    //this._Canvas.enter();
    var Durations = this.Data.map(Item => Item.Duration + Item.TimeBefore);
    var Max = Durations.reduce((Total, Item) => { return Total + Item; });

    this.BottomScale = d3.scaleLinear()
        .domain([d3.min([0, Max]), d3.max(Durations)])
        .range([0, Width]);

    this.BottomAxis = d3.axisBottom()
        .scale(this.BottomScale);

    var YSpacing = 50;
    var HalfYSpacing = Height / 2;
    var XSpacing = this.BottomScale(1);

    this._Canvas
        .data(this.Data)
        .enter()
        .append("rect")
        .attr("x", d => {
            d.X = d.TimeBefore * XSpacing;
            return d.X;
        })
        .attr("y", d => {
            d.Y = d.TaskID * YSpacing;
            return d.Y;
        })
        .attr("width", d => { return d.Duration * XSpacing; })
        .attr("height", YSpacing)
        .attr("rx", 10)
        .attr("ry", 10)
        .attr("fill", "#AAAAAA");

    //this._Canvas
    //    .data(this.Data.filter((d) => { return d.PrevTask.length > 0;  }))
    //    //.filter(d => { return d.PrevTask.length > 0; })
    //    .enter()
    //    .call(d => {
    //        var lineDrawer = d3.line()
    //        .x(function (d) { return x(d.x); })
    //        .y(function (d) { return y(d.y); });

    //        d.PrevTask.forEach((Item, Index)=>
    //        {
    //            var Line = [{ "x": d.X, "y": d.Y + HalfYSpacing }, { "x": d.X, "y": Item.Y + HalfYSpacing }, { "x": Item.X + Item.Duration * XSpacing, "y": ItemX.Y + HalfYSpacing }];

    //            this._Canvas
    //                .data(Line)
    //                .enter()
    //                .append("path")
    //                .attr("d", lineDrawer)
    //                .attr("stroke", "#Black")
    //                .attr("stroke-width", 2)
    //                .attr("fill", "none");
    //        });
    //    });

    this._Canvas
        .call(this.BottomAxis);

    //this._Zoom = d3.zoom();

    //this.Draw = function (NewData)
    //{
    //    const YSpacing = 50;
    //    const XSpacing = 50;
    //    const TextFont = "Verdana";

    //    if (!NewData)
    //        this.Data = NewData;

        

    //    this._Canvas
    //        .selectAll("g")
    //        .remove();

    //    var groups =
    //        this._Canvas
    //        .selectAll("g")
    //        .data(Data)
    //        .sort((a, b) =>{ })
    //        .enter()
    //        // start an group to set up a group
    //        .append("g")
    //        .attr("id", (d, i) => { "ID" + d.TaskID + "Draw" + i; })
    //        .each(function (d, i) { //ledgen

    //            var group = $("#ID" + d.TaskID + "Draw" + i);

    //            //draw a rect for the estimated time
    //            group
    //                .append("rect")
    //                .attr("x", XSpacing)
    //                .attr("y", i * YSpacing )
    //                .attr("width",  d.Duration * XSpacing)
    //                .attr("height", YSpacing)
    //                .attr("rx", 10)
    //                .attr("ry", 10)
    //                .attr("fill", "#00ff00");
    //            //draw a path for actual time data
    //            group
    //                .append("rect")
    //                .attr("x", XSpacing)
    //                .attr("y", i * YSpacing )
    //                .attr("width", d.ActualDuration * XSpacing)
    //                .attr("height", YSpacing / 2)
    //                .attr("rx", 5)
    //                .attr("ry", 5)
    //                .attr("fill", d.ActualDuration < d.Duration ? "#0000ff" : "#ff0000");
    //            //draw The line to next task 

    //            //add the hover over for the tool tips
    //            group.addEventListener("onmousemove", function (event) {

    //                var text = "Description: " + d.Description
    //                    + "<br>Expected Duration: " + d.Duration
    //                    + "<br>Expected start: " + "Not implented"
    //                    + "<br>Expected End: " + "Not implented"
    //                    + "<br>Actual Duration: " + d.Duration
    //                    + "<br>Actual Start: " + d.ActualStart
    //                    + "<br>Actual End: " + d.ActualEnd
    //                    + "<br>Previous Tasks: " + "Not implented"
    //                    + "<br>Next Tasks: " + "Not implented";

    //                var size = function () {
    //                    _Canvas.getContext("2d");
    //                    //context.font = TextFont;
    //                    var metrics = context.measureText(text);
    //                    return { x: metrics.width, y: metrics.height }
    //                }

    //                this.ToolTip = this.append("g")
    //                this.ToolTip
    //                    .append("rect")
    //                    .attr("x", event.clientX)
    //                    .attr("y", event.clientY)
    //                    .attr("width", size().x)
    //                    .attr("height", size().y)

    //            });
    //            group.addEventListener("mouseout", function () {
    //                this.remove(this.ToolTip);
    //                this.ToolTip = null;
    //            });

    //        });
        
    //}

    //this._Parent.resize(() =>{ this.Height(this._Parent.style.height); this.Width(this._Parent.style.width) })


}

