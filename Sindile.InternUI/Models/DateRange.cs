using System.ComponentModel.DataAnnotations;

namespace Sindile.InternUI.Models
{
  public class DateRange
  {
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime StartDateTime { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime EndDateTime { get; set; }
  }
}
