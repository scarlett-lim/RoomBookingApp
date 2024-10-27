using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            this._roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest)); //nameof(bookingRequest) = string bookingRequest
            }

            var availableRooms = _roomBookingService.GetAvailableRoom(bookingRequest.Date);

            if (availableRooms.Any())//if there is any availableRooms returned
            {
                Room room = availableRooms.First();
                RoomBooking roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);

                roomBooking.RoomId = room.Id;
                _roomBookingService.Save(roomBooking);
            }


            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
        }

        private static TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) where TRoomBooking
            : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                FullName = bookingRequest.FullName,
                Email = bookingRequest.Email,
                Date = bookingRequest.Date
            };
        }
    }
}