using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using TechnestHackhaton.Application.Features.Commands.CreateCard;
using TechnestHackhaton.Controllers;

namespace TechnestHackhaton.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Sliding")]
    [OutputCache]
    public class CardController : BaseController
    {
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Get([FromForm] CreateCardCommandRequest createCardCommandRequest)
        {
            CreateCardCommandResponse reponse = await Mediator.Send(createCardCommandRequest);
            return Ok(reponse);
        }
    }
}
