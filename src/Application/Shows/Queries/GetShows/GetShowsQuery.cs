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
using Microsoft.EntityFrameworkCore.Internal;

namespace BoxOffice.Application.Shows.Queries.GetShows
{
    public class GetShowsQuery : IRequest<ShowsVm>
    {
    }

    public class GetShowsQueryHandler : IRequestHandler<GetShowsQuery, ShowsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetShowsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShowsVm> Handle(GetShowsQuery request, CancellationToken cancellationToken)
        {
            var shows = new ShowsVm
            {
                List = await _context.Shows
                    .ProjectTo<ShowsDto>(_mapper.ConfigurationProvider)
                    .OrderByDescending(m => m.Id)
                    .ToListAsync(cancellationToken)
            };

            return shows;
        }
    }
}