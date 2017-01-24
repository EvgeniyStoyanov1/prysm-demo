using System;
using System.Diagnostics.Tracing;

namespace Prysm.Monitoring.WebApi.Diagnostics
{
    [EventSource(Name = "WebApiEventSource")]
    public class WebApiEventSource : EventSource, IEventSourceLogger
    {
        public class Keywords
        {
            public const EventKeywords Diagnostic = (EventKeywords)4;
        }

        [Event(1, Keywords = Keywords.Diagnostic, Level = EventLevel.Informational, Message = "Begin request")]
        public void RequestBegin(string payload = "")
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Diagnostic))
            {
                this.WriteEvent(1, payload);
            }
        }

        [Event(2, Keywords = Keywords.Diagnostic, Level = EventLevel.Informational, Message = "End request")]
        public void RequestEnd(string payload = "")
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Diagnostic))
            {
                this.WriteEvent(2, payload);
            }
        }
    }
}