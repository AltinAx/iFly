using iFly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iFly.DAL.Repositories
{
    public interface IFlightRepository
    {
        Task<Flight> Get(Expression<Func<Flight, bool>> predicate);

        Task<Flight> Get(int id);

        Task<List<Flight>> All();

        List<Flight> Find(Func<Flight, bool> predicate);

        Task Book(User user, Flight flight);

        Task RemoveBooking(User user, Flight flight);
    }
}