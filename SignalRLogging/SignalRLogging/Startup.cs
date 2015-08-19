using Microsoft.Owin;
using Owin;
using SignalRLogging;

namespace SignalRLogging
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}