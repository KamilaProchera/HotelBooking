using HotelBooking.DataAccessLayer.Models;

namespace HotelBooking.DataAccessLayer
{
    public interface IReservationRepository
    {
        bool GetReservation(Room room);
        void AddReservation(Room room, Booking booking);
        void PopulateReservationRepository();

    }
}
