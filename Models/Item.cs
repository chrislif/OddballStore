using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OddballStore.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Thumbnail { get; set; }

        public Item() { }

        public Item(string name, string description, int price, string thumbnail)
        {
            Name = name;
            Description = description;
            Price = price;
            Thumbnail = thumbnail;
        }


    }
    
}
