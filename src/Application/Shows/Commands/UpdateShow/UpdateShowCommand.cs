using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoxOffice.Application.Common.Exceptions;
using BoxOffice.Application.Common.Interfaces;
using BoxOffice.Domain.Entities;
using MediatR;

namespace BoxOffice.Application.Shows.Commands.UpdateShow
{
    public class UpdateShowCommand: IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }

    public class UpdateShowCommandHandler : IRequestHandler<UpdateShowCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateShowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateShowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Shows.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Show), request.Id);
            }

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
