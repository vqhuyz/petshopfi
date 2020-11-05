using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetShop.Models;

namespace PetShop.Models
{
    public class Cart
    {
        PetShopDataContext db = new PetShopDataContext();

        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int Discount { get; set; }

        public decimal TotalPrice
        {
            get {
                if (Discount > 0)
                    return (Price - Price * Discount / 100) * Count;
                else
                    return Price * Count;
            }
        }

        public Cart(long id)
        {
            Id = id;
            Product product = db.Products.Single(x => x.Id == Id);
            Name = product.Name;
            Image = product.Image;
            Price = decimal.Parse(product.Price.ToString());
            Count = 1;
            Discount = (int)product.Discount;
            PromotionPrice = product.PromotePrice;
        }
    }
}
