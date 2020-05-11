using System;
using System.Collections.Generic;
using System.Text;

namespace BoxOffice.Domain.Entities
{
    public class Show
    {
        public Show()
        {
            Sessions = new List<Session>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
