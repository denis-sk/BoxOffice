using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BoxOffice.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BoxOffice.Application.Sessions.Queries.GetSession
{
    public class GetSessionQuery: IRequest<SessionVm>
    {
       public Guid Id { get; set; }
    }
    public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, SessionVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSessionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SessionVm> Handle(GetSessionQuery request, CancellationToken cancellationToken)
        {
            var session =
                await _context.Sessions
                    .ProjectTo<SessionVm>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Time)
                    .Where(m => m.Id == request.Id)
                    .Select(m => new SessionVm
                    {
                        Time = m.Time,
                        Id = m.Id,
                        ShowId = m.ShowId,
                        PlacesLimit = m.PlacesLimit,
                        FreePlaces = m.PlacesLimit -
                                     _context.Orders.Where(o => o.SessionId == m.Id).Sum(o => o.Tickets.Count())
                    })
                    .FirstOrDefaultAsync(cancellationToken);

            return session;
        }
    }
}
