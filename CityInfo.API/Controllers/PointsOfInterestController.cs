using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
  [Route("api/Cities")]
  public class PointsOfInterestController : Controller
  {
    [HttpGet("{cityId}/pointsofinterest")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var poi = city.PointsOfInterest;
      if (poi == null)
      {
        return NotFound();
      }
      return Ok(poi);
    }

    [HttpGet("{cityId}/pointsofinterest/{Id}")]
    public IActionResult GetPointsOfInterest(int cityId, int Id)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var poi = city.PointsOfInterest.FirstOrDefault(p => p.Id == Id);
      if (poi == null)
      {
        return NotFound();
      }
      return Ok(poi);
    }
  }
}