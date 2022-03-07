using iFly.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iFly.BLL.Services.Interfaces
{
    public interface IFlightService
    {
        Task<List<Flight>> All();

        Task Book(string userId, int flightId);

        Task RemoveBooking(string userId, int flightId);

        Task<List<Flight>> GetUserFlights(string userId);

        Task<List<Flight>> SearchFlights(string from, string to);
    }
}