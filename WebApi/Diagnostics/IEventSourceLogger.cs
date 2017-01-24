namespace Prysm.Monitoring.WebApi.Diagnostics
{
    public interface IEventSourceLogger
    {
        void RequestBegin(string payload = "");
        void RequestEnd(string payload = "");
    }
}
