using System;
using System.Collections.Generic;

namespace Sindile.InternAPI.Data
{
    public partial class Occupation
    {
        public int Id { get; set; }
        public string? Position { get; set; }
        public double? HourlyRate { get; set; }
        public DateTime? Created { get; set; }
        public bool? Deleted { get; set; }
    }
}
