using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
  [Route("api/[controller]")]
  public class CitiesController : Controller
  {
    // GET api/values
    [HttpGet]
    public IActionResult Get()
    {
      return new JsonResult(CitiesDataStore.Current.Cities);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var cityToReturn= CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id ==  id);
      if (cityToReturn == null)
      {
        return NotFound();
      }
      return Ok(cityToReturn);
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
