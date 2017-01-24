using System.Net.Http;
using System.Web.Http;

namespace Prysm.Monitoring.WebApi.Controllers
{
    [RoutePrefix("dashboard")]
    public class DashboardController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok<string>("Hello from demo");
        }
    }
}
