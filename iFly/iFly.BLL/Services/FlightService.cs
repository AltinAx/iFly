using iFly.BLL.Services.Interfaces;
using iFly.DAL.Entities;
using iFly.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iFly.BLL.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _repository;
        private readonly UserManager<User> _userManager;

        public FlightService(IFlightRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task Book(string userId, int flightId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var flight = await _repository.Get(flightId);

            if(user != null && flight != null)
            {
                await _repository.Book(user, flight);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<List<Flight>> All()
        {
            return await _repository.All();
        }

        public async Task RemoveBooking(string userId, int flightId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var flight = await _repository.Get(flightId);

            if (user != null && flight != null)
            {
                await _repository.RemoveBooking(user, flight);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<List<Flight>> GetUserFlights(string userId)
        {
            return (await _repository.All())
                .Where(f => f.Users.Exists(u => u.Id == userId))
                .ToList();
        }

        public async Task<List<Flight>> SearchFlights(string from, string to)
        {
            var flights = await _repository.All();

            if (!String.IsNullOrEmpty(from))
            {
                flights = flights.Where(s => s.From.Contains(from)).ToList();
            }

            if (!String.IsNullOrEmpty(to))
            {
                flights = flights.Where(s => s.To.Contains(to)).ToList();
            }

            return flights;
        }
    }
}