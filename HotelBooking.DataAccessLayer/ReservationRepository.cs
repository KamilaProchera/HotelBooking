using HotelBooking.DataAccessLayer.Models;
using HotelBooking.DataAccessLayer.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace HotelBooking.DataAccessLayer
{
    public class ReservationRepository : IReservationRepository
    {
        private ConcurrentDictionary<string, Booking> bookings = new ConcurrentDictionary<string, Booking>();
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(ILogger<ReservationRepository> logger)
        {
            _logger = logger;
        }

        public bool GetReservation(Room room)
        {
            return bookings.ContainsKey($"{room.Id} {room.ReservationDate}");
        }

        public void AddReservation(Room room, Booking booking)
        {
            var bookedRoom = bookings.AddOrUpdate(key: $"{room.Id} {room.ReservationDate}", addValue: booking, (k, t) =>
            {
                return new Booking(t.Id, t.RoomNumber, t.DateTime, t.GuestName, true);
            });

            if (bookedRoom.ReservationRejected)
            {
                _logger.LogError("Room is not available");

                throw new NotRoomAvailableException("Room is not available");
            }
        }

        public void PopulateReservationRepository()
        {
            bookings.TryAdd($"101 {new DateTimeOffset(new DateTime(2021, 11, 6))}", new Booking(Guid.Parse("00000000-0000-0000-0000-000000000001"), 101, new DateTimeOffset(new DateTime(2021, 11, 6)), "John", false));
            bookings.TryAdd($"201 {new DateTimeOffset(new DateTime(2021, 11, 6))}", new Booking(Guid.Parse("00000000-0000-0000-0000-000000000002"), 201, new DateTimeOffset(new DateTime(2021, 11, 6)), "John", false));
        }
    }
}
