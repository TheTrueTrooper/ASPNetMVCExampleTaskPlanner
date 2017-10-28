using Microsoft.Owin;
using Owin;


namespace ASP.NetMVCExample
{
    public partial class Startup
    {
        public void ConfigurationSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}