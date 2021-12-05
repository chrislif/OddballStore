namespace OddballStore.Models
{
    public class CartItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public CartItem() { }

        public CartItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
