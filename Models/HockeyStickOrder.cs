namespace ShopAPI.Models
{
  public class HockeyStickOrder
  {
    public int Id { get; set; }

    // NAVIGATION PROPERTIES
    public int HockeyStickId { get; set; }

    public HockeyStick HockeyStick { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; }
  }
}