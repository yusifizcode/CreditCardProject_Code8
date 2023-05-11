using Microsoft.AspNetCore.Mvc;
using TechnestHackhaton.Application.Consts;
using TechnestHackhaton.Application.CustomAttributes;
using TechnestHackhaton.Application.Enums;
using TechnestHackhaton.Application.Features.Commands.AppUser.CreateUser;

namespace TechnestHackhaton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.User, ActionType = ActionType.Writing, Definition = "Creating User")]
        public async Task<IActionResult> Create([FromForm] CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await Mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
    }
}

