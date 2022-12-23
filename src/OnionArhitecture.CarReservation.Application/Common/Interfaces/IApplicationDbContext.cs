using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using OnionArhitecture.CarReservation.Domain.Reservation;

namespace OnionArhitecture.CarReservation.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Car> Cars { get; }

        DbSet<Booking> Bookings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
