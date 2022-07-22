using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("purchases/{id:int}")]
        public async Task<IActionResult> GetUserPurchasesById()
        {
            // we need to get the userId from the token, using HttpContext
            return Ok();
        }

        [HttpGet]
        [Route("Details/{id:int}")]
        public async Task<IActionResult> GetUserDetailsById()
        {
            // we need to get the userId from the token, using HttpContext
            return Ok();
        }
    }
}