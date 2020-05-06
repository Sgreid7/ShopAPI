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

    // [HttpPost("{locationId}")]
    // public async Task<ActionResult<List<Order>>> CreateOrder(List<HockeyStick> hockeySticks, int hockeyStickId, int locationId)
    // {
    //   // create order and save it
    //   var orderToAdd = new Order();
    //   orderToAdd.LocationId = locationId;
    //   await db.Orders.AddRangeAsync(orderToAdd);
    //   await db.AddRangeAsync();
    //   // await db.SaveChangesAsync();
    //   var hockeyStickOrdersToAdd = new List<HockeyStickOrder>();
    //   // foreach hockey stick in the list create a disc order with stickId and orderId
    //   var stick = await db.HockeySticks.FirstOrDefaultAsync(s => s.Id == hockeyStickId);
    //   if (stick.NumberInStock > 0)
    //   {
    //     var hockeyStickOrderToAdd = new HockeyStickOrder();
    //     hockeyStickOrderToAdd.OrderId = orderToAdd.Id;
    //     hockeyStickOrderToAdd.HockeyStickId = stick.Id;
    //     db.HockeyStickOrders.Add(hockeyStickOrderToAdd);
    //     await db.SaveChangesAsync();
    //     hockeyStickOrdersToAdd.Add(hockeyStickOrderToAdd);

    //   }
    //   orderToAdd.HockeyStickOrders = hockeyStickOrdersToAdd;
    //   await db.SaveChangesAsync();

    //   return new ContentResult()
    //   {
    //     Content = JsonConvert.SerializeObject(orderToAdd,
    //       new JsonSerializerSettings
    //       {
    //         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    //       }),
    //     ContentType = "application/json",
    //     StatusCode = 200
    //   };
    // }

    // [HttpPost("{hockeyStickId}")]
    // public async Task<ActionResult<List<Order>>> CreateNewOrder(Order order, int hockeyStickId, int locationId)
    // {
    //   var stick = db.HockeySticks.FirstOrDefault(s => s.Id == hockeyStickId);
    //   if (stick.NumberInStock < 1)
    //   {
    //     return Ok(new { message = "That item is not in stock" });
    //   }
    //   else
    //   {
    //     var itemOrder = new HockeyStickOrder
    //     {
    //       HockeyStickId = hockeyStickId
    //     };
    //     await db.HockeyStickOrders.AddAsync(order);
    //     await db.SaveChangesAsync();
    //     order.HockeyStickOrders = null;
    //     return Ok(order);
    //     // return new ContentResult()
    //     // {
    //     //   Content = JsonConvert.SerializeObject(itemOrder,
    //     //   new JsonSerializerSettings
    //     //   {
    //     //     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    //     //   }),
    //     //   ContentType = "application/json",
    //     //   StatusCode = 200
    //     // };
    //   }
    // }

    [HttpPost]
    public async Task<ActionResult<List<Order>>> CreateNewOrder(Order order, int hockeyStickId)
    {
      var itemInStock = db.HockeySticks.FirstOrDefault(i => i.Id == hockeyStickId);
      if (itemInStock.NumberInStock < 1)
      {
        return Ok(new { message = "That item is not in stock" });
      }
      else
      {
        await db.Orders.AddAsync(order);
        await db.SaveChangesAsync();
        var orderId = order.Id;
        var itemOrder = new HockeyStickOrder
        {
          OrderId = orderId,
          HockeyStickId = hockeyStickId
        };
        await db.HockeyStickOrders.AddAsync(itemOrder);
        await db.SaveChangesAsync();
        order.HockeyStickOrders = null;
        return Ok(order);
      }
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
    [HttpDelete("{locationId}/{id}")]
    public async Task<ActionResult> DeleteOrderById(int id, int locationId)
    {
      var orderToDelete = await db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.LocationId == locationId);
      db.Orders.Remove(orderToDelete);
      await db.SaveChangesAsync();
      return Ok(new { text = "Order successfully removed!" });
    }

  }
}