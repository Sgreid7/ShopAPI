using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;

namespace ShopAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocationController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpPost]
    public async Task<Location> CreateLocation(Location location)
    {
      await db.Locations.AddAsync(location);
      await db.SaveChangesAsync();
      return location;
    }

    [HttpGet("all")]
    public async Task<List<Location>> GetLocations()
    {
      return await db.Locations.OrderBy(loc => loc.ManagerName).ToListAsync();
    }

  }
}