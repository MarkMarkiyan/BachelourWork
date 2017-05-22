        using System.Web.Http;

namespace GraphsWeb.Controllers
{
    public class GraphsApiController : ApiController
    {

        [HttpPost]
        public int GetCpf([FromBody]object file)
        {
            return 1;
        }
    }
}