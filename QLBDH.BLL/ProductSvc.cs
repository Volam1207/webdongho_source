using QLBDH.Common.BLL;
using QLBDH.Common.Req;
using QLBDH.Common.Rsp;
using QLBDH.DAL;
using QLBDH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBDH.BLL
{
    public class ProductSvc:GenericSvc<ProductRep, Product>
    {
        private ProductRep productRep;
        public ProductSvc() { 
            productRep = new ProductRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }
        public override SingleRsp Update(Product m)
        {
            var res = new SingleRsp();

            var m1 = m.ProductId > 0 ? _rep.Read(m.ProductId) : _rep.Read(m.ProductName);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        public object SearchProduct(SearchProductReq searchProductReq)
        {
            var products = productRep.SearchProduct(searchProductReq.Keyword);
            var offset = (searchProductReq.Page - 1) * searchProductReq.Size;
            var total = products.Count();
            int totalPage = (total % searchProductReq.Size) == 0 ? (int)(total / searchProductReq.Size) :
                (int)(1 + (total / searchProductReq.Size));
            var data = products.OrderBy(x => x.ProductName).Skip(offset).Take(searchProductReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = searchProductReq.Page,
                Size = searchProductReq.Size

            };

            return res;
        }

        public SingleRsp CreateProduct(ProductReq productReq)
        {
            var res = new SingleRsp();
            Product product = new Product();
            product.ProductId = productReq.ProductId;
            product.ProductName = productReq.ProductName;
            product.CategoryId = productReq.CategoryId;
            product.Price = productReq.Price;
            product.Brand = productReq.Brand;
            product.Description = productReq.Description;
            product.StockQuantity = productReq.StockQuantity;
            product.ImageUrl = productReq.ImageUrl;
            
            res = productRep.CreateProduct(product);
            return res;
        }
        public SingleRsp UpdateProduct(ProductReq productReq)
        {
            var res = new SingleRsp();
            Product product = new Product();
            product.ProductId = productReq.ProductId;
            product.ProductName = productReq.ProductName;
            product.CategoryId = productReq.CategoryId;
            product.Price = productReq.Price;
            product.Brand= productReq.Brand;
            product.Description = productReq.Description;
            product.StockQuantity = productReq.StockQuantity;
            product.ImageUrl = productReq.ImageUrl;
            res = productRep.UpdateProduct(product);
            return res;
        }
        public SingleRsp DeleteProduct(int productId)
        {
            var res = new SingleRsp();
            var existingProduct = productRep.Read(productId);
            if(existingProduct == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = productRep.DeleteProduct(existingProduct);
            }
            return res;
        }
        public SingleRsp GetProductById(int productId)
        {
            var res = new SingleRsp();

            var product = productRep.Read(productId);

            if (product == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res.Data = product;
            }

            return res;
        }
    }
}
