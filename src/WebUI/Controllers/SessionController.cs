using System;
using System.Threading.Tasks;
using BoxOffice.Application.Sessions.Commands.CreateSession;
using BoxOffice.Application.Sessions.Commands.UpdateSession;
using BoxOffice.Application.Sessions.Queries.GetSession;
using BoxOffice.Application.Sessions.Queries.GetSessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoxOffice.WebUI.Controllers
{
    [Authorize]
    public class SessionController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateSessionCommand command)
        {
            return await Mediator.Send(command);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<SessionVm>> Get(Guid id)
        {
            return await Mediator.Send(new GetSessionQuery {Id = id});
        }
        [HttpPut]
        public async Task<ActionResult> Update(Guid id, UpdateSessionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
