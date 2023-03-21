using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Save(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }
    }
}
