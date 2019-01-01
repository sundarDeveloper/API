using System;
using System.Linq;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
  [Route("api/Cities")]
  public class PointsOfInterestController : Controller
  {
    private ILogger<PointsOfInterestController> _logger;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
    {
      _logger = logger;
    }
    [HttpGet("{cityId}/pointsofinterest")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      try
      {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
          _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
          return NotFound();
        }
        var poi = city.PointsOfInterest;
        if (poi == null)
        {
          return NotFound();
        }
        return Ok(poi);
      }
      catch (Exception ex)
      {
        _logger.LogCritical($"Exception while trying to get city {cityId} ", ex );
        return StatusCode(500, "A problem happened while handling your request ");
      }
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

    [HttpDelete("{cityId}/pointsOfInterest/{id}")]
    public IActionResult DeletePointOfInterest(int cityId, int id)
    {
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
      city.PointsOfInterest.Remove(poi);
      return NoContent();
    }
  }
}