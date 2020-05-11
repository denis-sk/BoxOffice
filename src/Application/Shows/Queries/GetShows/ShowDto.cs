using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxOffice.Application.Common.Mappings;
using BoxOffice.Domain.Entities;

namespace BoxOffice.Application.Shows.Queries.GetShows
{
    public class ShowsDto: IMapFrom<Show>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IList<SessionDto> Sessions { get; set; }
    }
}
