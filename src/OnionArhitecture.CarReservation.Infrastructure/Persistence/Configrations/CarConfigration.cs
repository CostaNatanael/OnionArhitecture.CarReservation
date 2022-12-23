using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnionArhitecture.CarReservation.Domain.Reservation;

namespace OnionArhitecture.CarReservation.Infrastructure.Persistence.Configrations
{
    internal class CarConfigration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {

            builder.HasKey(x => new {  x.CarPrefix , x.CarId});

            //doesnt work with in-memory Db https://github.com/dotnet/efcore/issues/11032
            // builder.Property(t => t.Model).HasComputedColumnSql($"[{nameof(Car.CarPrefix)}] + ', ' + [{nameof(Car.CarId)}]", stored: true).ValueGeneratedOnAddOrUpdate();

            builder.Property(t => t.CarId)
               .ValueGeneratedOnAdd();

            builder.Property(t => t.CarPrefix)
               .HasMaxLength(3);

            builder.Property(t => t.Model)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Make)
              .HasMaxLength(100)
              .IsRequired();

            builder
               .HasMany<Booking>(c => c.Bookings)
               .WithOne(x => x.Car)
               .HasForeignKey(x => new { x.CarPrefix, x.CarId });
        }
    }
}
