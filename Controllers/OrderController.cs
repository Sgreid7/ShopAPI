using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ShopAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
      return await db.Orders.OrderBy(o => o.PlacedAt).ToListAsync();
    }

    [HttpPost("{locationId}")]
    public async Task<ActionResult<Order>> CreateOrder(List<HockeyStick> hockeySticks, int locationId)
    {
      // create order and save it
      var orderToAdd = new Order();
      orderToAdd.LocationId = locationId;
      await db.Orders.AddAsync(orderToAdd);
      await db.SaveChangesAsync();
      var hockeyStickOrdersToAdd = new List<HockeyStickOrder>();
      // foreach hockey stick in the list create a disc order with stickId and orderId
      foreach (HockeyStick hs in hockeySticks)
      {
        var stick = await db.HockeySticks.FirstOrDefaultAsync(s => s.Id == hs.Id);
        if (stick.NumberInStock > 0)
        {
          var hockeyStickOrderToAdd = new HockeyStickOrder();
          hockeyStickOrderToAdd.OrderId = orderToAdd.Id;
          hockeyStickOrderToAdd.HockeyStickId = hs.Id;
          db.HockeyStickOrders.Add(hockeyStickOrderToAdd);
          hockeyStickOrdersToAdd.Add(hockeyStickOrderToAdd);
        }
      }

      orderToAdd.HockeyStickOrders = hockeyStickOrdersToAdd;
      await db.SaveChangesAsync();

      return new ContentResult()
      {
        Content = JsonConvert.SerializeObject(orderToAdd,
          new JsonSerializerSettings
          {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          }),
        ContentType = "application/json",
        StatusCode = 200
      };
    }

    // * * * * * GET ORDERS BY LOCATION
    [HttpGet("view/{LocationId}")]
    public async Task<ActionResult<List<Order>>> ViewOrdersInALocation(int locationId)
    {
      var orders = await db.Orders.Where(stick => stick.LocationId == locationId).ToListAsync();
      return Ok(orders);
    }

    // * * * * * UPDATE AN ORDER BY ID
    [HttpPut("{id}")]
    public async Task<ActionResult<Order>> UpdateOrders(int id, Order newData)
    {
      newData.Id = id;
      db.Entry(newData).State = EntityState.Modified;
      await db.SaveChangesAsync();
      return newData;
    }

    // * * * * * DELETE AN ORDER BY ID
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrderById(int id)
    {
      var orderToDelete = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
      db.Orders.Remove(orderToDelete);
      await db.SaveChangesAsync();
      return Ok(new { text = "Order successfully removed!" });
    }

  }
}