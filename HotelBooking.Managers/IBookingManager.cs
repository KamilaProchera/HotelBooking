using System;
using System.Collections.Generic;

namespace HotelBooking.Managers
{
    public interface IBookingManager
    {
        bool IsRoomAvailable(int roomNumber, DateTimeOffset date);
        void AddBooking(string guest, int roomNumber, DateTimeOffset date);
        IEnumerable<string> GetAvailableRooms(DateTimeOffset date);
    }
}
