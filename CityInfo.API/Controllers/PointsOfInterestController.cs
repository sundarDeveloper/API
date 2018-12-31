using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
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

    [HttpGet("{cityId}/pointsofinterest/{Id}", Name = "GetPointOfInterest")]
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

    [HttpPost("{cityId}/pointsofinterest")]
    public IActionResult CreatePointsOfInterest(int cityId, 
                            [FromBody] PointOfInterestForCreationDto pointofInterest)
    {
      if (pointofInterest == null)
      {
        return BadRequest();
      }
      if (pointofInterest.Name == pointofInterest.Description)
      {
        ModelState.AddModelError("Description", "Name and Description cannot be the same.");
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null )
      {
        return NotFound();
      }
      var maxPointOfInterest = CitiesDataStore.Current.Cities.SelectMany(x => x.PointsOfInterest).Max(p => p.Id);
      var poiForAdd = new PointOfInterestDto
      {
        Id = ++maxPointOfInterest,
        Name = pointofInterest.Name,
        Description = pointofInterest.Description
      };
      city.PointsOfInterest.Add(poiForAdd);

      return CreatedAtRoute("GetPointOfInterest", new { cityId, id = poiForAdd.Id }, poiForAdd);
    }

    [HttpPut("{cityId}/pointsofinterest/{Id}")]
    public IActionResult UpdatePointsOfInterest(int cityId, int id,
                        [FromBody] PointOfInterestForCreationDto pointofInterest)
    {
      if (pointofInterest == null)
      {
        return BadRequest();
      }
      if (pointofInterest.Name == pointofInterest.Description)
      {
        ModelState.AddModelError("Description", "Name and Description cannot be the same.");
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var poi = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
      if (poi == null)
      {
        return NotFound();
      }
      poi.Description = pointofInterest.Description;
      poi.Name = pointofInterest.Name;

      return NoContent();
    }
  }
}