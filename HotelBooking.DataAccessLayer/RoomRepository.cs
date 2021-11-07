using HotelBooking.DataAccessLayer.Models;
using System.Collections.Generic;

namespace HotelBooking.DataAccessLayer
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IList<Room> rooms = new List<Room>();
        public IEnumerable<Room> GetAllRooms()
        {
            return rooms;
        }

        public void PopulateRoomRepository()
        {
            rooms.Add(new Room(101));
            rooms.Add(new Room(102));
            rooms.Add(new Room(201));
            rooms.Add(new Room(203));
        }
    }
}
