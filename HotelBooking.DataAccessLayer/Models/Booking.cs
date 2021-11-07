using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.DataAccessLayer.Models
{
    public class Booking
    {
        public Booking(Guid id, int roomNumber, DateTimeOffset dateTime, string guestName, bool reservationRejected)
        {
            Id = id;
            RoomNumber = roomNumber;
            DateTime = dateTime;
            GuestName = guestName;
            ReservationRejected = reservationRejected;
        }
        public Guid Id { get; }
        public int RoomNumber { get; }
        public DateTimeOffset DateTime { get; }
        public string GuestName { get; }
        public bool ReservationRejected { get; }
    }
}
