using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Domain
{
    //1 booking can only have 1 roombooking
    //1 room can have multiple roombooking
    public class RoomBooking : RoomBookingBase
    {
        public int? Id { get; set; }

        //Foreign Key
        public Room Room { get; set; }
        public int RoomId { get; set; }

    }
}