using System;
using System.Collections.Generic;

#nullable disable

namespace QLBDH.DAL.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int? StockQuantity { get; set; }
        public string ImageUrl { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
