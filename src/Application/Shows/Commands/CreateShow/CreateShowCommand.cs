using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoxOffice.Application.Common.Interfaces;
using BoxOffice.Domain.Entities;
using MediatR;

namespace BoxOffice.Application.Shows.Commands.CreateShow
{
    public partial class CreateShowCommand : IRequest<Guid>
    {
        public string Title { get; set; }
    }
    public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateShowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateShowCommand request, CancellationToken cancellationToken)
        {
            var entity = new Show
            {
                Title = request.Title
            };

            _context.Shows.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
