using iFly.Models;
using iFly.DAL.Database;
using iFly.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using iFly.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace iFly.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IFlightService _flightService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlightController(ILogger<FlightController> logger, IHttpContextAccessor httpContextAccessor, IFlightService flightService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _flightService = flightService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            return View(await _flightService.GetUserFlights(currentUserId));
        }

        public async Task<IActionResult> AddFlight(int? id)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _flightService.Book(currentUserId, (int)id);
 
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _flightService.RemoveBooking(currentUserId, (int)id);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}