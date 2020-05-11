using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxOffice.Application.Common.Mappings;
using BoxOffice.Domain.Entities;

namespace BoxOffice.Application.Shows.Queries.GetShows
{
    public class SessionDto : IMapFrom<Session>
    {
        public Guid Id { get; set; }
        public int PlacesLimit { get; set; }
        public DateTime Time { get; set; }
    }
}
