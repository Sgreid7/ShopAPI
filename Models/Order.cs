using System;
using System.Collections.Generic;

namespace ShopAPI.Models
{
  public class Order
  {
    public int Id { get; set; }

    public DateTime PlacedAt { get; set; } = DateTime.Now;

    public int LocationId { get; set; }

    public Location Location { get; set; }

    // NAVIGATION
    public List<HockeyStickOrder> HockeyStickOrders { get; set; } = new List<HockeyStickOrder>();

  }
}