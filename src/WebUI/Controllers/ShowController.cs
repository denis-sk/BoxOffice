using System;
using System.Threading.Tasks;
using BoxOffice.Application.Shows.Commands.CreateShow;
using BoxOffice.Application.Shows.Commands.UpdateShow;
using BoxOffice.Application.Shows.Queries.GetShows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoxOffice.WebUI.Controllers
{
    [Authorize]
    public class ShowController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateShowCommand command)
        {
            return await Mediator.Send(command);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ShowsVm>> Get()
        {
            return await Mediator.Send(new GetShowsQuery());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateShowCommand command)
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
