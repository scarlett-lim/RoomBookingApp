using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistance;
using RoomBookingApp.Persistance.Repositories;

namespace RoomBookingAppPersistance.Tests
{
    public class RoomBookingServiceTest
    {
        [Fact]
        public void ShouldReturnAvailableRooms()
        {
            //Arrange
            var date = new DateTime(2024, 12, 08);

            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>()
                .UseInMemoryDatabase("AvailableRoomTest")
                .Options;

            using var context = new RoomBookingAppDbContext(dbOptions);
            context.Add(new Room { Id = 1, Name = "Room 1" });
            context.Add(new Room { Id = 2, Name = "Room 2" });
            context.Add(new Room { Id = 3, Name = "Room 3" });

            context.Add(new RoomBooking { RoomId = 1, Date = date});
            context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1) });

            context.SaveChanges();

            var roomBookingService = new RoomBookingService(context);

            //Act
            var availableRoom = roomBookingService.GetAvailableRoom(date);

            //Assert
            Assert.Equal(2, availableRoom.Count());
            Assert.Contains(availableRoom, q => q.Id == 2);
            Assert.Contains(availableRoom, q => q.Id == 3);
            Assert.Contains(availableRoom, q => q.Name == "Room 3");
            Assert.Contains(availableRoom, q => q.Name == "Room 2");
            Assert.DoesNotContain(availableRoom, q => q.Id == 1);
            
        }


        [Fact]
        public void ShouldSaveRoomBooking()
        {
            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>()
                .UseInMemoryDatabase("ShouldSaveTest")
                .Options;

            var roomBooking = new RoomBooking
            {
                RoomId = 1,
                Date = new DateTime(2024, 12, 08)
            };

            using var context = new RoomBookingAppDbContext(dbOptions);
            var roomBookingService = new RoomBookingService(context);
            roomBookingService.Save(roomBooking);

            var bookings = context.RoomBookings.ToList();

            //make sure only 1 record is returned
            var booking = Assert.Single(bookings);

            Assert.Equal(roomBooking.Date, booking.Date);
            Assert.Equal(roomBooking.RoomId, booking.RoomId);


        }
    }
}