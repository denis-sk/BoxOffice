using System;
using System.Collections.Generic;
using System.Text;
using BoxOffice.Application.Common.Mappings;
using BoxOffice.Domain.Entities;

namespace BoxOffice.Application.Sessions.Queries.GetSessions
{
    public class ShowDto: IMapFrom<Show>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
