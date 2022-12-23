using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArhitecture.CarReservation.Domain.Reservation;


namespace OnionArhitecture.CarReservation.Infrastructure.Persistence.Configrations
{
    internal class BookingConfiguration: IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
    {
            builder.HasKey(t=> t.Id);

               
    }
}}
