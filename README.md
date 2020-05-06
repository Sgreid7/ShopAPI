# Shop API

# Objectives

- Create an API that can CRUD against a Database
- Re-enforce SQL fundamentals
- One to many relationships
- Working with Docker

# Includes

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [EF CORE](https://docs.microsoft.com/en-us/ef/core/)
- [POSTGRESQL](https://www.postgresql.org/)
- [CONTROLLERS](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.controller?view=aspnet-mvc-5.2)
- [POSTMAN](https://www.postman.com/)
- [DOCKER](https://www.docker.com/resources/what-container)
- [SWAGGER](https://swagger.io/solutions/api-documentation/)
- [MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)

# Featured Code

## Using Action Results and Async / Await

```JSX
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
```
