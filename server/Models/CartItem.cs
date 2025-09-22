public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
