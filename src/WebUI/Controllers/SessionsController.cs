using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxOffice.Application.Sessions.Queries.GetSessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoxOffice.WebUI.Controllers
{
  
    public class SessionsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SessionsVm>> Get(string? search, int? month, int page = 1, int limit = 5)
        {
            return await Mediator.Send(new GetSessionsQuery { Page = page, Title = search, Month = month, Limit = limit });
        }
    }
}
