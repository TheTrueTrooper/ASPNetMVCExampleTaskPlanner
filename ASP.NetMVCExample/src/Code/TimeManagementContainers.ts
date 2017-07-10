import * as MoDate from "../typedeffs/moment.d.ts";


export module TimeManagementContainers
{
    export class Task
    {
        protected _TaskName: string;
        protected _StartDate: MoDate.Moment;
        protected _EndDate: MoDate.Moment;
        protected _TimeLength: MoDate.Duration;
        protected _VirtualRender;

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
            this._VirtualRender(Canvas);
        }

    }

    export class Linker
    {
        protected _StartTask: Task[];
        protected _EndTask: Task[];
        protected _VirtualRender;

        constructor(StartTask: Task[], EndTask: Task[])
        {
            this._StartTask = StartTask;
            this._EndTask = EndTask;
        }

        public Render(Canvas: SVGSVGElement): void
        {
            this._VirtualRender(Canvas)
            this._StartTask.forEach(x =>
            {
                x.Render(Canvas);
            });
        }
    }
}