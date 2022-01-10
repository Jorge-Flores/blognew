using Microsoft.AspNetCore.Mvc;


//Health Check : Checkar o status da API

namespace BlogNew.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        //Health Check : Checkar o status da API
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();

        }




    }
}