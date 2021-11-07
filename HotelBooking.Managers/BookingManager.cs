using HotelBooking.DataAccessLayer;
using HotelBooking.DataAccessLayer.Models;
using HotelBooking.DataAccessLayer.Utilities;
using System;
using System.Collections.Generic;

namespace HotelBooking.Managers
{
    public class BookingManager : IBookingManager
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingManager(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }
        public bool IsRoomAvailable(int roomNumber, DateTimeOffset date)
        {
            try
            {
                return !_reservationRepository.GetReservation(new Room(roomNumber, date));
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
        }
        public void AddBooking(string guest, int roomNumber, DateTimeOffset date)
        {
            try
            {
                _reservationRepository.AddReservation(new Room(roomNumber, date), new Booking(new Guid(), roomNumber, date, guest, false));
            }
            catch (NotRoomAvailableException e)
            {
                Console.WriteLine("{0} exception caught by {1}.", e.Message, nameof(BookingManager));
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught", e);
            }
        }

        public IEnumerable<string> GetAvailableRooms(DateTimeOffset date)
        {
            var availableRooms = new List<string>();
            try
            {
                var allRooms = _roomRepository.GetAllRooms();
                foreach (var room in allRooms)
                {
                    if (!_reservationRepository.GetReservation(new Room(room.Id, date)))
                        availableRooms.Add($"Room number {room.Id.ToString()} is available");
                }
                return availableRooms;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return Array.Empty<string>();
            }
        }
    }
}
