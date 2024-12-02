using RoomBookingApp.Domain;
using RoomBookingApp.Domain.RoomBooking;

namespace RoomBookingApp.Core.DataServices
{
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);
        IEnumerable<Room> GetAvailableRoom(DateTime date);
    }
}
