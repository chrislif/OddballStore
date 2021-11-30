using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OddballStore.Models
{
    public class Cart
    {
        private List<CartItem> itemCollection = new List<CartItem>();

        public virtual void addItem(Item item, int quantity)
        {
            CartItem cartItem = itemCollection.Where(p => p.Item.ItemID == item.ItemID).FirstOrDefault();

            if(cartItem == null)
            {
                itemCollection.Add(new CartItem { Item = item, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }

        }

        public virtual void RemoveCartItem(Item item) => itemCollection.RemoveAll(i => i.Item.ItemID == item.ItemID);
        public virtual decimal ComputeTotalValue() => itemCollection.Sum(e => e.Item.Price * e.Quantity);
        public virtual void Clear() => itemCollection.Clear();
        public virtual IEnumerable<CartItem> CartItems => itemCollection;

    }

    public class CartItem
    {
        public int CartItemID { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
