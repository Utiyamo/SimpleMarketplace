using Microsoft.AspNetCore.Mvc;

namespace DC.SimpleMarketplace.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get() 
        {
            return Ok();
        }
    }
}
