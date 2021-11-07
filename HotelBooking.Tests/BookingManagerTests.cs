using HotelBooking.DataAccessLayer;
using HotelBooking.DataAccessLayer.Models;
using HotelBooking.DataAccessLayer.Utilities;
using HotelBooking.Managers;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace HotelBooking.Tests
{
    public class BookingManagerTests
    {
        int roomNumber = 101;
        DateTimeOffset date = new DateTimeOffset(new DateTime(2021, 11, 6));

        private readonly IBookingManager _bookingManager;
        private readonly Mock<IReservationRepository> _reservationRepoMock = new Mock<IReservationRepository>();
        private readonly Mock<IRoomRepository> _roomRepoMock = new Mock<IRoomRepository>();

        public BookingManagerTests()
        {
            _bookingManager = new BookingManager(_reservationRepoMock.Object, _roomRepoMock.Object);
        }

        [Fact]
        public void IsRoomAvailable_ShouldReturnFalse()
        {
            // Arrange
            _reservationRepoMock.Setup(x => x.GetReservation(It.IsAny<Room>())).Returns(true);

            // Act
            var result = _bookingManager.IsRoomAvailable(roomNumber, date);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsRoomAvailable_ShouldReturnTrue()
        {
            // Arrange
            _reservationRepoMock.Setup(x => x.GetReservation(It.IsAny<Room>())).Returns(false);

            // Act
            var result = _bookingManager.IsRoomAvailable(roomNumber, date);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddBooking_ShouldNotThrowException()
        {
            // Arrange
            _reservationRepoMock.Setup(x => x.AddReservation(It.IsAny<Room>(), It.IsAny<Booking>()));

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _bookingManager.AddBooking("John", roomNumber, date);

                // Assert
                Assert.Empty(sw.ToString());
            }
        }

        [Fact]
        public void AddBooking_ShouldThrowNotRoomAvailableException()
        {
            // Arrange
            string expected = "NotRoomAvailableException";
            _reservationRepoMock.Setup(x => x.AddReservation(It.IsAny<Room>(), It.IsAny<Booking>())).Throws(new NotRoomAvailableException());

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _bookingManager.AddBooking("John", roomNumber, date);

                // Assert
                Assert.Contains(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetAvailableRooms_ShouldReturn2AvailableRooms()
        {
            // Arrange
            IEnumerable<string> expected = new List<string> { "Room number 101 is available", "Room number 102 is available" };
            _roomRepoMock.Setup(x => x.GetAllRooms()).Returns(GetAllRooms());
            _reservationRepoMock.SetupSequence(x => x.GetReservation(It.IsAny<Room>())).Returns(false).Returns(false);

            // Act
            var result = _bookingManager.GetAvailableRooms(date);

            // Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void GetAvailableRooms_ShouldReturn1AvailableRoom()
        {
            // Arrange
            IEnumerable<string> expected = new List<string> { "Room number 102 is available" };
            _roomRepoMock.Setup(x => x.GetAllRooms()).Returns(GetAllRooms());
            _reservationRepoMock.SetupSequence(x => x.GetReservation(It.IsAny<Room>())).Returns(true).Returns(false);

            // Act
            var result = _bookingManager.GetAvailableRooms(date);

            // Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void GetAvailableRooms_ShouldReturn0AvailableRooms()
        {
            // Arrange
            IEnumerable<string> expected = new List<string> { };
            _roomRepoMock.Setup(x => x.GetAllRooms()).Returns(GetAllRooms());
            _reservationRepoMock.SetupSequence(x => x.GetReservation(It.IsAny<Room>())).Returns(true).Returns(true);

            // Act
            var result = _bookingManager.GetAvailableRooms(date);

            // Assert
            Assert.Equal(expected, result);

        }

        private IEnumerable<Room> GetAllRooms()
        {
            IList<Room> rooms = new List<Room>();
            rooms.Add(new Room(101));
            rooms.Add(new Room(102));
            IEnumerable<Room> allRooms = rooms;
            return allRooms;
        }
    }
}
