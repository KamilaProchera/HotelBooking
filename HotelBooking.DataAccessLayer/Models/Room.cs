using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.DataAccessLayer.Models
{
    public class Room
    {
        public Room(int id) => Id = id;
        public Room(int id, DateTimeOffset reservationDate)
        {
            Id = id;
            ReservationDate = reservationDate;
        }
        public int Id { get; set; }
        public DateTimeOffset ReservationDate { get; set; }
    }
}
