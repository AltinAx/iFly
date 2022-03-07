using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace iFly.DAL.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public virtual List<Flight> Flights { get; set; }
    }
}
