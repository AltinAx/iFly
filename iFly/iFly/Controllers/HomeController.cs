using iFly.Models;
using iFly.DAL.Database;
using iFly.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using iFly.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace iFly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlightService _flightService;

        public HomeController(ILogger<HomeController> logger, IFlightService flightService)
        {
            _logger = logger;
            _flightService = flightService;
        }

        public async Task<IActionResult> Index(string sortOrder, string from, string to)
        {
            ViewBag.FromSortParm = String.IsNullOrEmpty(sortOrder) ? "from_desc" : "From";
            ViewBag.ToSortParm = sortOrder == "To" ? "to_desc" : "To";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var flights = (IEnumerable<Flight>) await _flightService.SearchFlights(from, to);

            switch (sortOrder)
            {
                case "from_desc":
                    flights = flights.OrderByDescending(s => s.From);
                    break;
                case "To":
                    flights = flights.OrderBy(s => s.To);
                    break;
                case "to_desc":
                    flights = flights.OrderByDescending(s => s.To);
                    break;
                case "Date":
                    flights = flights.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    flights = flights.OrderByDescending(s => s.Date);
                    break;
                default:
                    flights = flights.OrderBy(s => s.From);
                    break;
            }

            return View(flights);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
