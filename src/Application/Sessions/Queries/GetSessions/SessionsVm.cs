using System;
using System.Collections.Generic;
using System.Text;

namespace BoxOffice.Application.Sessions.Queries.GetSessions
{
    public class SessionsVm
    {
        public int Total { get; set; }
        public IList<SessionDto> List { get; set; }
    }
}
