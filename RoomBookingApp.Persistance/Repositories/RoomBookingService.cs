using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;
using RoomBookingApp.Domain.RoomBooking;

namespace RoomBookingApp.Persistance.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        public IEnumerable<Room> GetAvailableRoom(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Save(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }
    }
}
