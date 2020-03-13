using System.Web.Http;

namespace API1.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public string Ping()
        {
            return "Pong!";
        }
    }
}