using QLBDH.Common.DAL;
using QLBDH.Common.Req;
using QLBDH.Common.Rsp;
using QLBDH.DAL.Models;
using System.Security.AccessControl;

namespace QLBDH.DAL
{
    public class OrderItemRep:GenericRep<WatchStoreContext, OrderItem>
    {
        public OrderItemRep()
        {

        }
        public override OrderItem Read(int id)
        {
            try
            {
                var res = All.FirstOrDefault(c => c.OrderItemId == id);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to read OrderItem with ID {id}: {ex.Message}");
                throw;
            }
        }
        public SingleRsp CreateOrderItem(OrderItem oi)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            var productId = Context.Products.Any(u => u.ProductId == oi.ProductId);
            try
            {
                Context.OrderItems.Add(oi);
                Context.SaveChanges();
                t.Commit();

                res.Data = oi;
            }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }
        public SingleRsp UpdateOrderItemById(int id, OrderItem oi)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                var orderItem = Context.OrderItems.Find(id);
                if (orderItem == null)
                {
                    res.SetError("OrderItem not found.");
                }
                else
                {
                    Context.SaveChanges();
                    t.Commit();

                    res.Data = orderItem;
                }
            }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }

        public SingleRsp DeleteOrderItemById(int id)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                var oi = Context.OrderItems.Find(id);
                Context.OrderItems.Remove(oi);
                Context.SaveChanges();
                t.Commit();
                res.Data = oi;
            }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }
    }
}
