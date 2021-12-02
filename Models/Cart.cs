using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OddballStore.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public Item[] cartList { get; set; }

        public Cart() { }

        //public Cart(int quantity, string name, string description, int price, string thumbnail)
        //{
        //    Quantity = quantity;
        //    Name = name;
        //    Description = description;
        //    Price = price;
        //    Thumbnail = thumbnail;
        //}


    }
}
