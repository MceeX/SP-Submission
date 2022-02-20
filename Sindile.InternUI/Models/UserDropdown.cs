using System.ComponentModel.DataAnnotations;

namespace Sindile.InternUI.Models
{
  public class UserDropdown
  {
    /// <summary>
    /// Gets or sets an employees identifier
    /// </summary>
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets an employees first name
    /// </summary>
    public string FirstName { get; set; }
  }
}
