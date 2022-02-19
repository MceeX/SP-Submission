namespace Sindile.InternUI.Models
{
  /// <summary>
  /// Represents an employee's log
  /// </summary>
  public class EmployeeLog  : TaskLog
  {
    /// <summary>
    /// Gets or sets employee first name associated with a log
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Gets or sets task associated with a log
    /// </summary>
    public string TaskName { get; set; }
  }
}
