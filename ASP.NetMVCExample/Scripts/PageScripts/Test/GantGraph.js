//2017-04-24 The Trooper
// a Java Script to Render Graphs

//Assume jQuery Loaded

//will quickly calc and produce a diffence from the moments
class DateDiff
{
    //use moments as inputs
    constructor(endDate, beginDate)
    {
        var ED = endDate;
        var BD = beginDate;

        this.DateDiff = moment.duration(ED.diff(BD))
    }

    GetDiffAsSec()
    {
        return this.DateDiff.asSeconds();
    }

}

//holds a simple set of x and y's also has add to alow for transforms
class Vector
{
    constructor(x, y)
    {
        this.X = x;
        this.Y = y;
    }

    PointToString()
    {
        return this.X.toString() + "," + this.Y.toString() + " ";
    }

    Add(Vector2)
    {
        this.Y += Vector2.Y;
        this.X += Vector2.X;
    }
}

// a model of a Task Used to generate A single DOM object for the graph
class GantTask
{
    constructor(taskName, startTime, endTime, postTask)
    {
        this.TaskName = taskName;
        this.PostTask = postTask;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.TimeDiff = DateDiff(this.EndTime, this.StartTime);
    }

    private SetLocation(x, y)
    {
        this.Location = new Location(x, y);
    }

    private SetWidth(x, y)
    {
        this.Width = new Location(x, y);
    }

    SetStartTime(year, day, hour)
    {
        this.StartTime = moment(new DateScaler(year, day, hour));
        this.TimeDiff = DateDiff(this.EndTime, this.StartTime);
    }

    SetEndTime(year, day, hour)
    {
        this.EndTime = moment(new DateScaler(year, day, hour));
        this.TimeDiff = DateDiff(this.EndTime, this.StartTime);
    }

    //will Render the Box on Canvas
    Render(GanttGraph)
    {
        // set the points to a string rep
        var Points = ""
        this.SlideTransform(GantTask, this.StartTime).forEach(function(item){
            Points += item.PointToString();
        })
        Canvas.createElement("polygon").setAttribute("points", Points);
    }


    //slides the bar over based on its time slot. Slide down based on input
    SlideTransform(GanttGraph, y)
    {
    // find the off set from the start of the project to the items place
        var Shift = new Vector(moment.duration(this.StartTime.diff(GanttGraph.ProjectBeginDate)).asSeconds() * GanttGraph.Scale(), y);
        var Points = this.GetShapePoints(GanttGraph);
        Points.forEach(function (Item) {
            Item.Add(Shift);
        });
        return Points;
    }

    //gets all the shapes Points at an non transformed stage
    GetShapePoints(GanttGraph)
    {
        var length = this.GetBarTimeLengthAsPX(GanttGraph);
        // draw a simple sqare for now
        return [new Vector(0,0), new Vector(0, 50), new Vector(length , 50), new Vector(length, 0)]
    }

    GetBarTimeLengthAsPX(GanttGraph)
    {
        return this.GetBarTimeLengthAsSec() * GanttGraph.Scale;
    }

    GetBarTimeLengthAsSec()
    {
        return this.TimeDiff.GetDiffAsSec();
    }
}

class GanttGraph
{
    constructor(TimeScale, projectBeginDate, projectEndDate)
    {
        this.Scale = TimeScale;
        this.ProjectBeginDate = projectBeginDate;
        this.ProjectEndDate = projectEndDate;
    }
}
