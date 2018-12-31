using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
  public class PointOfInterestForUpdateDto
  {
    [Required(ErrorMessage = "Cannot be empty or null")]
    [MaxLength(100, ErrorMessage = "Name Max length is 100")]
    public string Name { get; set; }
    [MaxLength(100, ErrorMessage = "Description max lenght is 100")]
    public string Description { get; set; }
  }
}
