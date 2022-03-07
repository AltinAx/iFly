using iFly.DAL.Database;
using iFly.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iFly.DAL.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FlightRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public async Task<List<Flight>> All()
        {
            return await _databaseContext.Set<Flight>().ToListAsync();
        }

        public async Task Book(User user, Flight flight)
        {
            user.Flights.Add(flight);
            await _databaseContext.SaveChangesAsync();
        }

        public List<Flight> Find(Func<Flight, bool> predicate)
        {
            return _databaseContext.Set<Flight>().Where(predicate).ToList();
        }

        public async Task<Flight> Get(Expression<Func<Flight, bool>> predicate)
        {
            return await _databaseContext.Set<Flight>().FirstOrDefaultAsync(predicate);
        }

        public async Task<Flight> Get(int id)
        {
            return await _databaseContext.Set<Flight>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task RemoveBooking(User user, Flight flight)
        {
            user.Flights.Remove(flight);
            await _databaseContext.SaveChangesAsync();
        }
    }
}