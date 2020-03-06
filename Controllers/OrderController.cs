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

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(List<HockeyStick> hockeySticks)
    {
      // create order and save it
      var orderToAdd = new Order();
      await db.Orders.AddAsync(orderToAdd);
      await db.SaveChangesAsync();
      var hockeyStickOrdersToAdd = new List<HockeyStickOrder>();
      // foreach hockey stick in the list create a disc order with stickId and orderId
      foreach (HockeyStick hs in hockeySticks)
      {
        var hockeyStickOrderToAdd = new HockeyStickOrder();
        hockeyStickOrderToAdd.OrderId = orderToAdd.Id;
        hockeyStickOrderToAdd.HockeyStickId = hs.Id;
        db.HockeyStickOrders.Add(hockeyStickOrderToAdd);
        hockeyStickOrdersToAdd.Add(hockeyStickOrderToAdd);
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



  }
}