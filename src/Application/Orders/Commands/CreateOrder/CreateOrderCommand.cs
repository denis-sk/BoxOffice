using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoxOffice.Application.Common.Exceptions;
using BoxOffice.Application.Common.Interfaces;
using BoxOffice.Domain.Entities;
using MediatR;

namespace BoxOffice.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid SessionId { get; set; }
        public int Count { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrderCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions.FindAsync(request.SessionId);
            if (session == null)
            {
                throw new NotFoundException(nameof(Session), request.SessionId);
            }

            var totalSell = _context.Orders.Where(m => m.SessionId == request.SessionId).Sum(m => m.Tickets.Count());

            if (totalSell + request.Count >= session.PlacesLimit)
                throw new OrderException(session.Id.ToString(),
                    $"Tickets are not enough. Free {session.PlacesLimit - totalSell}");

            var entity = new Order()
            {
                SessionId = request.SessionId,
                UserId = _currentUserService.UserId
            };

            for (int i = 0; i < request.Count; i++)
            {
                entity.Tickets.Add(new Ticket());
            }
            
            _context.Orders.Add(entity);
            
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;

        }
    }
}
