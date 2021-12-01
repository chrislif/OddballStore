namespace OddballStore.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
