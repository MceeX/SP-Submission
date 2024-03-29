﻿using System;
using System.Collections.Generic;

namespace Sindile.InternAPI.Data
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Created { get; set; }
        public bool? Deleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
