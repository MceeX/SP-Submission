using System;
using System.Collections.Generic;

namespace Sindile.InternAPI.Data
{
    public partial class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Idnumber { get; set; }
        public string? EmailAddress { get; set; }
        public byte[]? ProfileImage { get; set; }
        public int? OccupationId { get; set; }
        public bool? Deleted { get; set; }
        public bool? Dismissed { get; set; }
        public DateTime? SignOnDate { get; set; }

        public virtual Role? Occupation { get; set; }
    }
}
