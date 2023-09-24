using QLBDH.Common.DAL;
using QLBDH.Common.Rsp;
using QLBDH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBDH.DAL
{
    public class ProductRep:GenericRep<WatchStoreContext, Product>
    {
        public override Product Read(int id)
        {
            var res = All.FirstOrDefault(p=>p.ProductId == id);
            return res;
        }
        public int Remove(int id)
        {
            var m = base.All.First(i => i.ProductId == id);
            m = base.Delete(m);
            return m.ProductId;
        }
        public SingleRsp CreateProduct(Product product)
        {
            var res = new SingleRsp();
            var categoryId = Context.Categories.Any(u => u.CategoryId == product.CategoryId);
            if (!categoryId)
            {
                res.SetError("Product not found");
                return res;
            }
            
            using (var context = new WatchStoreContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Add(product);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public SingleRsp UpdateProduct(Product product)
        {
            var res =new SingleRsp();
            var categoryId = Context.Categories.Any(u => u.CategoryId == product.CategoryId);
            if (!categoryId)
            {
                res.SetError("Product not found");
                return res;
            }
            using (var context = new WatchStoreContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Update(product);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch(Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public List<Product> SearchProduct(String keyWord)
        {
            return All.Where(x => x.ProductName.Contains(keyWord)).ToList();
        }
        public SingleRsp DeleteProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new WatchStoreContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
    }
}
