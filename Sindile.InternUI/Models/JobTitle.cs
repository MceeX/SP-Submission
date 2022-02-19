using System.ComponentModel.DataAnnotations;

namespace Sindile.InternUI.Models
{
    public class JobTitle
    {
        /// <summary>
        /// Gets or sets the identifier for a role
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the role description/name
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Gets or sets the hourly rate for role
        /// </summary>
        public double? HourlyRate { get; set; }
    }
}
