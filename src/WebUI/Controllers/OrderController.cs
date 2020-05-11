using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxOffice.Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoxOffice.WebUI.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateOrderCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
