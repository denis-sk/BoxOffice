using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoxOffice.Application.Common.Interfaces;
using BoxOffice.Domain.Entities;
using MediatR;

namespace BoxOffice.Application.Sessions.Commands.CreateSession
{
    public class CreateSessionCommand : IRequest<Guid>
    {
        public Guid ShowId { get; set; }
        public DateTime Time { get; set; }
        public int PlacesLimit { get; set; }
    }
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateSessionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
         
            var entity = new Session()
            {
                ShowId = request.ShowId,
                PlacesLimit = request.PlacesLimit,
                Time = request.Time
            };

            _context.Sessions.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
