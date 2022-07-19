using System;
using System.Collections.Generic;

namespace Film_Management_System_API.Models
{
    public partial class Actor
    {
        public Actor()
        {
            Films = new HashSet<Film>();
        }

        public decimal ActorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<Film> Films { get; set; }
    }
}
