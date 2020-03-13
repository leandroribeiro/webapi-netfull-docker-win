using System.Web.Http;

namespace API2.Controllers
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