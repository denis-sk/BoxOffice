using BoxOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BoxOffice.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Show> Shows { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<Session> Sessions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
