using System;
using System.Collections.Generic;
using System.Text;

namespace BoxOffice.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            Tickets = new List<Ticket>();
        }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid SessionId { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
