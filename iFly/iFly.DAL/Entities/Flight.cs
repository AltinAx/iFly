using System;
using System.Collections.Generic;

namespace iFly.DAL.Entities
{
    public class Flight
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
