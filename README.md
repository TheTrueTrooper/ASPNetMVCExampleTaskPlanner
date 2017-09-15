# ASP.NetMVC-razor-Example-TaskPlanner
Using ASP.NET MVC with razor I am creating a task manager site to show case my Model View Control skills and build on them.

<h3>Introduction</h3> 
<p>I recently started this project to both demonstrate and build on my ability to develop cloud applications. I see this project both as a point for learning, development, and show casing my skills. The project is primarily C# ASP.NET Core/MVC a cloud. The project is an amalgamation of technologies/approaches in reality and will be listed here.<br/>
&#9;I)	Technologies<br/>
    1)	C# 6.0 including the .NET Core framework, Lambdas, Attributes, and LINQS can be observed through most of the project. This   can be sub categorized. <br/>
        i)	ASP.NET Core/MVC Models are used for serving data located in any file in the “Models” folder.<br/>
        ii)	ASP.NET Core/MVC Views are used for rendering html located in any file in the “Views” folder.<br/>
        iii)	ASP.NET Core/MVC Controllers are used for serving calls located in any file in the “Controllers” folder.<br/>
        iv)	ASP.NET Core/MVC Bundling are used for serving located in the “App_Start\BundleConfig.cs” file and in the views such as _Layout.<br/>
        v)	Custom ASP.NET Core/MVC  attributes can be observed in the SecurityValidation\DBFSPAuthorizeAttribute.cs<br/>
    2)	HTML is used in any and all ASP.NET Core/MVC Razor views. Razor pages that is a combination of HTML and C#. For Example any file in the “Views” folder will have Razor HTML.<br/>
    3)	CSS files can be found on all views as well. They can be observed in the CSS classes used on objects. For Example any file in the “Views” folder will have Razor HTML and for Source Example any file in the “Content” folder.  <br/>
    4)	Bootstrap CSS and JS library can be found on all views as well. They can be observed in the CSS classes used on objects. For Example any file in the “Views” folder will have Razor HTML.<br/>
    5)	JavaScript with JQuery integrated in all of JavaScript files. These Client side instructions be found in the “Scripts” folder. Various views use them in various ways such as on load or calls.<br/>
    6)	MomentJS, JQueryUI, and 3DJS are all used where appropriate JavaScript to display or input data.<br/>
    7)	AngularJS is used in a few different places. Modals can be found in “PageScripts\DashBoard\DashBoard.js” and “Scripts\PageScripts\Projects\Index.js” files. Related AngularJS augmented HTML Code “Views\Project” and “Views\Dashboard” folder. Most of these pages consume ASP.Net restful (not truly) APIs and razor pages. <br/>
    8)	Typescript using type definitions for JavaScript libraries can be observed in the “src” folder. JavaScript outputs can be observed in the “Scripts/TypeScriptOutput” folder<br/>
    9)	Project makes heavy use of my AngelASPExtentions library for ASP.NET. found at https://github.com/TheTrueTrooper/AngelASPExtentions. In this project there are custom attributes for data annotations, custom extensions for strings; files; images; byte arrays; controllers; HtmlHelper, Custom Results, and a Razor hack to template to a string for emails.<br/>
    10)	Project as makes use of the MS C# System.Security.Cryptography library for salting and hashing in the _HelpersAndExtens\SecurityHelper.cs file<br/>
    11)	It MS C# System.Net/System.Net.Mail in the ASP.NET Core for email in the “_HelpersAndExtens\SMTPHelper.cs” file <br/>
    12)	The Project makes use of the Entity Framework (C#) in the “Models\TaskMasterData.edmx” file. This Is how I access my database from a (DB First approach with stored procedures)<br/>
    13)	The Project uses PLSQL in the MVCTaskMasterAppData project. <br/>
II)	Approaches<br/>
    1)	This project makes use at its base a ASP.NET Core and MVC approach.<br/>
    2)	This project makes use of a stored procedure database first approach.<br/>
    3)	This Project makes use of a Typescript approach for complicated JavaScript Problems.<br/>
    4)	This Project makes use for certain pages some APIs though they are not truly restful as they leverage a state (signed in?). This is mostly seen in tandem with Angular<br/>
    5)	This Project makes use for certain pages AngularJS’s MVVM approach.</p>

<h3>Why</h3>  

<p>As I stated before, I recently started this project to both demonstrate and build on my ability to develop cloud applications. I see this project both as a point for learning, development, and show casing my skills. The project is primarily C# ASP.NET Core/MVC a cloud application that fills in a void I saw while I worked in the construction industry. </p>

<h3>The Problem</h3>

<p>It’s a simple problem of communication that could feel like the following scenario.</p>
<p>“More often than not you would be told to go and do work. Being receiving pay by the hour you would go not thinking much of it. When you get there; however, you would see that the plumber still need to lay down his in floor pipes for the sewer, or in floor heating. You being responsible for the concrete on top would realize you wasted your time and gas coming out. 
It’s not a big deal; you are payed by the hour, but now you need to phone your boss and inform him. It probably won’t be too bad as it’s not your fault, but it is his company’s time and money wasted. Add to that this is the fifth time the contractor has miscommunicated with him.“</p>
<p>Well that is a serious problem for any contractor or subcontractor. You must pay these people regardless and replace the gas. This is money out of some ones pocket and puts strain on the relationships as a result.</p>
<p>It’s not anyone’s real fault, as all parties are generally pretty busy with multiple contracts, projects, and jobs to fill. All of this divides our attention leaving holes in our view of the picture (data) and thus communication.</p>

<h3>Why we fail</h3>

<p>We plan a project from the start with planning. The planning is an analytics from what we know of earlier problems with the same characteristic. The construction industry is huge and required everywhere and as a result can often nail this step with statistic. This is the data at its root and often is quite close to the truth in this industry, so why does it fail then.
The problem is in the communication. We know there will need to be in floor heating from the start. We even had it in the gantt chart in our office no one is allowed into, but does the concrete guys know about it no.</p>
Similarly the data is never static. It grows and adapts. Often changing what we planned. So static statistical data is a good starting point, but communication of the dynamic outlaying data is just as important if not more important after the fact. For instance the plumber knows about the in floor heating, but learns he needs a special tubing he does not have enough of. Well now he has to phone (that phones his boss), so that he can notify the proper party. Add to that this is happening for his many jobs.
So wouldn’t it be nice if using the framing work we set up in the planning step we could just go ahead and notify everyone involved in a single step. It’s not that complicated when you think about it. Just connect the data on an accessible platform (The web).</p>

<h3>The Solution</h3>

<p>This is what my application would like to remove from the table. By taking the data from the beginning to end and placing it on a cloud that can be accessed by everyone (within reason abstraction of certain things is still good) we cut down on these issues.</p>
<p>More often than not in the planning step we make gantt charts that identify the critical routes for work (tasks). Often these can be assigned to a profession and a group that handles it. If Task A for the plumbers finishes why not notify the Task B concrete finisher party automatically using the management’s confirmation step. Using the groupings responsible for the task abstract the data to who needs it, or just leave it visible for transparency.</p>
<p>And while we are busy interconnecting this data why not allow for communication up the chain as well. For instance the plumber will take longer he posts to a virtual board rooms wall and notifies them of it with the reason. All invested parities would probably like to know why it is late and how late will it be. If this is critical issue now we all actively join a virtual board room with voice and go over the problem for a solution together.</p>
</span>
