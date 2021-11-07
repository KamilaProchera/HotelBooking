using HotelBooking.DataAccessLayer.Models;
using System.Collections.Generic;

namespace HotelBooking.DataAccessLayer
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAllRooms();
        void PopulateRoomRepository();
    }
}
