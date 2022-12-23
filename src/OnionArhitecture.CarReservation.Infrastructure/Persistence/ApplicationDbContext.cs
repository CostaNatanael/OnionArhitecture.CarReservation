using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Domain.Reservation;

namespace OnionArhitecture.CarReservation.Infrastructure.Persistence
{

    public class ApplicationDbContext : DbContext , IApplicationDbContext
    {
        /// <summary>
        /// will be used for dispachingdomain events
        /// </summary>
        private readonly IMediator _mediator;


        public ApplicationDbContext(IMediator mediator)

        {
            _mediator = mediator;
        }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CarReservationDb");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           // await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

     
    }
}