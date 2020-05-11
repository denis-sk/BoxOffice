using System;
using System.Collections.Generic;
using System.Text;

namespace BoxOffice.Domain.Entities
{
    public class Session
    {
        public Session()
        {
            Orders = new List<Order>();
        }
        public Guid Id { get; set; }
        public Guid ShowId { get; set; }
        public int PlacesLimit { get; set; }
        public DateTime Time { get; set; }
        public List<Order> Orders { get; set; }
    }
}
