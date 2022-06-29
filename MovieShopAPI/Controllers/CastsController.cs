using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastsController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastsController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCast(int id)
        {
            var cast = await _castService.GetCastDetails(id);
            if (cast == null)
            {
                return NotFound(new { errorMessage = $"No Cast Found for id: {id}" });
            }

            return Ok(cast);
        }
    }
}
