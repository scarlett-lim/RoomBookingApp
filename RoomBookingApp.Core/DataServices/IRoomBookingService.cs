﻿using RoomBookingApp.Core.Domain;

namespace RoomBookingApp.Core.DataServices
{
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);
        IEnumerable<Room> GetAvailableRoom(DateTime date);
    }
}
