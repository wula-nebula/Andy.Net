using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Template.HealthCheck
{
    public class MyHealthCheck : EventListener, IHealthCheck
    {
        private EventSource[] sources;
        private readonly ILogger logger;

        public MyHealthCheck()
        {
            sources = EventSource.GetSources().ToArray();


            foreach (var item in sources)
            {
                if (item.Name == "System.Threading.Tasks.TplEventSource")
                    continue;

                if (item.Name == "Microsoft-System-Net-NameResolution")
                    continue;

                if (item.Name == "Microsoft-System-Net-Sockets")
                    continue;

                if (item.Name == "Microsoft-System-Net-Http")
                    continue;

                if (item.Name == "System.Diagnostics.Eventing.FrameworkEventSource")
                    continue;

                if (item.Name == "Microsoft-System-Net-Primitives")
                    continue;

                EnableEvents(item, EventLevel.LogAlways, EventKeywords.None);
            }
        }
        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {

            Console.WriteLine(eventData.EventSource.Name);

            base.OnEventWritten(eventData);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var data = sources.ToDictionary(item => item.Name, item => item.IsEnabled());
            var json = JObject.FromObject(data);

            return HealthCheckResult.Healthy(json.ToString());
        }
    }
}
