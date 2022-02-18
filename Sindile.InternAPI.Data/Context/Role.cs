using System;
using System.Collections.Generic;

namespace Sindile.InternAPI.Data
{
    public partial class Role
    {
        public Role()
        {
            Interns = new HashSet<Intern>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; }
        public double? HourlyRate { get; set; }
        public bool? Deleted { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Intern> Interns { get; set; }
    }
}
