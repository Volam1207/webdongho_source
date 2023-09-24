using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBDH.Common.Req
{
    public class ProductReq
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string? Description { get; set; }
        public int? StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
    }
}
