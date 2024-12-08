using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistance.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _context;

        public RoomBookingService(RoomBookingAppDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Room> GetAvailableRoom(DateTime date)
        {

            // 1 room can have multiple bookings
            // q = list of rooms
            // x = list of room bookings for the particular room
            // get the room where there are not any bookings with the date requested
            return _context.Rooms.Where(q => q.RoomBookings.Any(x => x.Date == date) == false)
                .ToList();
        }

        public void Save(RoomBooking roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
