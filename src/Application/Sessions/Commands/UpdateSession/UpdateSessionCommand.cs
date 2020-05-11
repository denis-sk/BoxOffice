using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoxOffice.Application.Common.Exceptions;
using BoxOffice.Application.Common.Interfaces;
using BoxOffice.Domain.Entities;
using MediatR;

namespace BoxOffice.Application.Sessions.Commands.UpdateSession
{
    public class UpdateSessionCommand: IRequest
    {
        public  Guid Id { get; set; }
        public DateTime Time { get; set; }
        public int PlacesLimit { get; set; }
    }
    public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSessionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Sessions.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Show), request.Id);
            }

            entity.Time = request.Time;
            entity.PlacesLimit = request.PlacesLimit;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
