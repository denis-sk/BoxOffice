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

namespace BoxOffice.Application.Sessions.Queries.GetSessions
{
    public class GetSessionsQuery : IRequest<SessionsVm>
    {
        public string? Title { get; set; }
        public int Page { get; set; }
        public int? Month { get; set; }
        public int Limit { get; set; }
    }
    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, SessionsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSessionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SessionsVm> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        {
            var shows =  _context.Shows
                .AsNoTracking()
                .ProjectTo<ShowDto>(_mapper.ConfigurationProvider);

            var temp = _context.Sessions
                .AsNoTracking()
                .ProjectTo<SessionDto>(_mapper.ConfigurationProvider)
                .Select(m => new SessionDto
                {
                    Id = m.Id,
                    ShowId = m.ShowId,
                    PlacesLimit = m.PlacesLimit,
                    Time = m.Time,
                    Show = shows.FirstOrDefault(s => s.Id == m.ShowId)
                })
                .Where(m => request.Title == null || m.Show.Title.ToLower().StartsWith(request.Title.ToLower()))
                .Where(m => request.Month == null || m.Time.Month == request.Month)
                .OrderBy(t => t.Show.Title);
            var total = temp.Count();

            var session = new SessionsVm
            {
                Total = total,
                List = await temp.Skip((request.Page - 1) * request.Limit).Take(request.Limit)
                    .ToListAsync(cancellationToken)
            };

            return session;
        }
    }
}
