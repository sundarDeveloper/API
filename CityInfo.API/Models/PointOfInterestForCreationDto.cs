using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
  public class PointOfInterestForCreationDto
  {
    [Required( ErrorMessage = "Cannot be empty or null")]
    [MaxLength(100,ErrorMessage = "Name Max length is 100")]
    public string Name { get; set; }
    [MaxLength(100, ErrorMessage = "Description max lenght is 100")]
    public string Description { get; set; }
  }
}
