using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.DataAccessLayer.Utilities
{
    public class NotRoomAvailableException : Exception
    {
        public NotRoomAvailableException()
        {
        }
        public NotRoomAvailableException(string message): base(message)
        {
        }
        public NotRoomAvailableException(string message, Exception inner):base(message, inner)
        {
        }
    }
}
