using RoomBookingApp.Domain;
using RoomBookingApp.Domain.RoomBooking;

namespace RoomBookingApp.Core.DataServices
{
    //need to have this interface as these functions will connect to db
    //if connect to external source then may use interface
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);
        IEnumerable<Room> GetAvailableRoom(DateTime date);
    }
}
