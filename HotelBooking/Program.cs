using HotelBooking.DataAccessLayer;
using HotelBooking.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace HotelBooking
{
    class Program
    {
        private readonly BookingService _bookingService;
        public Program(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Program>().Run();
        }
        public void Run()
        {
            _bookingService.Run();
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
            {
                services.AddTransient<Program>();
                services.AddTransient<BookingService>();
                services.AddScoped<IBookingManager, BookingManager>();
                services.AddScoped<IReservationRepository, ReservationRepository>();
                services.AddScoped<IRoomRepository, RoomRepository>();
            });
        }
    }
    public class BookingService
    {
        private readonly IBookingManager _bookingManager;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<BookingService> _logger;
        public BookingService(
            IBookingManager bookingManager,
            IReservationRepository reservationRepository,
            IRoomRepository roomRepository,
            ILogger<BookingService> logger)
        {
            _bookingManager = bookingManager;
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public void Run()
        {
            _logger.LogInformation("Booking system started work");

            _reservationRepository.PopulateReservationRepository();
            _roomRepository.PopulateRoomRepository();


            DateTimeOffset today = new DateTimeOffset(new DateTime(2021, 11, 6));
            Console.WriteLine(_bookingManager.IsRoomAvailable(102, today)); // outputs true

            _bookingManager.AddBooking("Patel", 102, today);

            Console.WriteLine(_bookingManager.IsRoomAvailable(102, today)); // outputs false

            _bookingManager.AddBooking("Li", 102, today); // throws an exception

            Console.WriteLine("Available rooms:");

            foreach (var item in _bookingManager.GetAvailableRooms(today))
            {
                Console.WriteLine(item);
            }

            _logger.LogInformation("Booking system finished work, have a nice day :)");
        }
    }
}
