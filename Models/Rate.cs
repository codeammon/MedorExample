using System.ComponentModel.DataAnnotations;

namespace Connector.Models
{
  public class Rate
  {
    [Key]
    public int ID { get; set; }
    public string Code { get; set; }
    public string Value { get; set; }
    public DateTime Valid {  get; set; }
    public string? Note { get; set; }
  }
}
