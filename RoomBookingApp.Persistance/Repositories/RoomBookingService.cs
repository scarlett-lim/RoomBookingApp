﻿using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;
using RoomBookingApp.Domain.RoomBooking;

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
            throw new NotImplementedException();
        }

        public void Save(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }
    }
}
