using System;
using System.Collections.Generic;
using System.Text;
using BoxOffice.Application.Common.Mappings;
using BoxOffice.Domain.Entities;

namespace BoxOffice.Application.Sessions.Queries.GetSessions
{
    public class SessionDto : IMapFrom<Session>
    {
        public Guid Id { get; set; }
        public Guid ShowId { get; set; }
        public DateTime Time { get; set; }
        public int PlacesLimit { get; set; }
        public ShowDto Show { get; set; }

    }
}
