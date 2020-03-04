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
  public class HockeyStickController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet]
    public async Task<List<HockeyStick>> GetHockeySticks()
    {
      return await db.HockeySticks.OrderBy(stick => stick.Id).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<HockeyStick> GetSingleHockeyStick(int id)
    {
      return await db.HockeySticks.FirstOrDefaultAsync(stick => stick.Id == id);
    }

    [HttpGet("out")]
    public async Task<List<HockeyStick>> GetOutOfStockHockeySticks()
    {
      return await db.HockeySticks.Where(stick => stick.NumberInStock == 0).ToListAsync();
    }

    [HttpGet("SKU/{SKU}")]
    public async Task<HockeyStick> GetBySKU(int sku)
    {
      return await db.HockeySticks.FirstOrDefaultAsync(stick => stick.SKU == sku);
    }

    [HttpPost]
    public async Task<HockeyStick> CreateHockeyStick(HockeyStick hockeyStick)
    {
      await db.HockeySticks.AddAsync(hockeyStick);
      await db.SaveChangesAsync();
      return hockeyStick;
    }

    [HttpPut("{id}")]
    public async Task<HockeyStick> UpdateHockeyStick(int id, HockeyStick newData)
    {
      newData.Id = id;
      db.Entry(newData).State = EntityState.Modified;
      await db.SaveChangesAsync();
      return newData;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHockeyStick(int id)
    {
      var hockeyStick = await db.HockeySticks.FirstOrDefaultAsync(stick => stick.Id == id);
      db.HockeySticks.Remove(hockeyStick);
      await db.SaveChangesAsync();
      return Ok();
    }

  }
}