/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To make some reuseable java containers to use in graphing time objects
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: TypeScript
//  Writer/Publisher: Microsoft
//  Link: https://www.typescriptlang.org/
//  }
*/
import * as MoDate from "../typedeffs/moment";

//A class that encapulates a Task
    export class Task
    {
        //the Tasks name
        protected _TaskName: string;
        //the time a task will start
        protected _StartDate: MoDate.Moment;
        //the time a task will end
        protected _EndDate: MoDate.Moment;
        //the time a task will take as a length
        protected _TimeLength: MoDate.Duration;
         // to overload it should be set to OverloadRender with a void typodis
        // it should do the main render you could also draw axis here
        // wish I could type define this more but you cant function type or else I dont know how :'0 really should have one if you want to extend
        protected _VirtualRender;

        //constructor used to make a task from a set of moments and a name
        constructor(name: string, StartDate: MoDate.Moment, EndDate: MoDate.Moment)
        {
            this._TaskName = name;
            this._StartDate = StartDate;
            this._EndDate = EndDate;
            this._TimeLength = MoDate.duration(EndDate.diff(StartDate));
        }

        // a Extra static funtion that is used to make a Task from a Duration
        public static ConstructFromDuration(name: string, StartDate: MoDate.Moment, Duration: MoDate.Duration): Task
        {
            return new Task(name, StartDate, StartDate.add(Duration));
        }

        //called to call the virtual Render or a Non-virtual interface pattern INV call system for use in rendering
        public Render(Canvas): void
        {
            this._VirtualRender(Canvas);
        }

    }

    //a class that encaplates a Realational connection between tasks
    export class Linker
    {
        //The set of tasks that must be completed befor moving on
        protected _StartTask: Task[];
        //The set of tasks that will be moved on to
        protected _EndTask: Task[];
        // to overload it should be set to OverloadRender with a void typodis
        // it should do the main render you could also draw axis here
        // wish I could type define this more but you cant function type or else I dont know how :'0 really should have one if you want to extend
        protected _VirtualRender;

        //a simple constructor used to make taskLinkers from a set of tasks
        constructor(StartTask: Task[], EndTask: Task[])
        {
            this._StartTask = StartTask;
            this._EndTask = EndTask;
        }

        //called to call the virtual Render or a Non-virtual interface pattern INV call system for use in rendering
        public Render(Canvas): void
        {
            this._VirtualRender(Canvas)
            this._StartTask.forEach(x =>
            {
                x.Render(Canvas);
            });
        }
    }

