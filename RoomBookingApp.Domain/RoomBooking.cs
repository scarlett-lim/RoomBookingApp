using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Domain.RoomBooking
{
    public class RoomBooking : RoomBookingBase
    {
        public int? Id { get; set; }

        //Foreign Key
        public Room Room { get; set; }
        public int RoomId { get; set; }

    }
}