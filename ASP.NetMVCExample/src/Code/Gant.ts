/// <reference path="../typedeffs/moment.d.ts" />
import * as MoDate from "../typedeffs/moment.d.ts";


class Task
{
    public Name: string;
    public StartDate: MoDate.Moment;
    public EndDate: MoDate.Moment;
    public TimeLength: MoDate.Duration; 

    constructor(name: string, StartDate: MoDate.Moment, EndDate: MoDate.Moment)
    {
        this.Name = name;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.TimeLength = MoDate.duration(EndDate.diff(StartDate));
    }


    

}
