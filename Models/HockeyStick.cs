using System;
using System.Collections.Generic;

namespace ShopAPI.Models
{
  public class HockeyStick
  {
    public int Id { get; set; }

    public int SKU { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int NumberInStock { get; set; }

    public decimal Price { get; set; }

    public DateTime DateOrdered { get; set; }

    // NAVIGATION PROPERTIES
    public int? LocationId { get; set; }

    public Location Location { get; set; }

    public List<HockeyStickOrder> HockeyStickOrders { get; set; }
  }
}