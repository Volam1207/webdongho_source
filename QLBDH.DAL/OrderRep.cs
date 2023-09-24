using Microsoft.EntityFrameworkCore;
using QLBDH.Common.DAL;
using QLBDH.Common.Req;
using QLBDH.Common.Rsp;
using QLBDH.DAL.Models;


namespace QLBDH.DAL
{
    public class OrderRep : GenericRep<WatchStoreContext, Order>
    {
        public OrderRep()
        {

        }

        public List<Order> Orders { get; set; }
        public List<Order> ReadAll()
        {
            return Context.Orders.ToList();
        }

        public OrderRep(WatchStoreContext context)
        {
            Context = context;
        }
        public List<Product> GetProductsPurchaseByUserId(int userId)
        {
            return Context.OrderItems
                .Where(oi => oi.Order.UserId == userId)
                .Select(oi => oi.Product)
                .Distinct()
                .ToList();
        }
        public int GetTotalQuantitySoldByProductId(int productId)
        {
            return Context.OrderItems
                .Where(oi => oi.ProductId == productId)
                .Sum(oi => oi.Quantity ?? 0);
        }
        public override Order Read(int id)
        {
            try
            {
                var res = All.FirstOrDefault(c => c.OrderId == id);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to read Order with ID {id}: {ex.Message}");
                throw;
            }
        }
        public SingleRsp CreateOrder(Order order)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
                t.Commit();
            }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }
        public SingleRsp UpdateOrderById(Order order)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                Context.Update(order);
                Context.SaveChanges();
                t.Commit();
            }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }

        public SingleRsp DeleteOrderById(int id)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
               
                var orderItems = Context.OrderItems.Where(oi => oi.OrderItemId == id);
                var order = Context.Orders.Find(id);
                Context.OrderItems.RemoveRange(orderItems);

                Context.Orders.Remove(order);
                Context.SaveChanges();
                res.Data = order;
                t.Commit();

            }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }
        public OrderStatistic GetMonthlyStatistics(int year, int month)
        {
            try
            {
                using (var context = new WatchStoreContext())
                {
                    var statistics = context.Orders
                        .Where(o => o.OrderDate.Year == year && o.OrderDate.Month == month)
                        .SelectMany(o => o.OrderItems)
                        .Select(oi => new OrderStatistic
                        {
                            OrderCount = 1,
                            TotalRevenue = oi.Quantity * oi.Price 
                        })
                        .AsEnumerable()
                        .DefaultIfEmpty(new OrderStatistic())
                        .Aggregate((s1, s2) => new OrderStatistic
                        {
                            OrderCount = s1.OrderCount + s2.OrderCount,
                            TotalRevenue = s1.TotalRevenue + s2.TotalRevenue
                        });

                    var orderStatistic = new OrderStatistic
                    {
                        OrderCount = statistics.OrderCount,
                        TotalRevenue = statistics.TotalRevenue
                    };
                    return orderStatistic;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public OrderStatistic GetYearlyStatistics(int year)
        {
            try
            {
                using (var context = new WatchStoreContext())
                {
                    var statistics = context.Orders
                        .Where(o => o.OrderDate.Year == year)
                        .SelectMany(o => o.OrderItems)
                        .Select(oi => new OrderStatistic
                        {
                            OrderCount = 1,
                            TotalRevenue = oi.Quantity * oi.Price
                        })
                        .AsEnumerable()
                        .DefaultIfEmpty(new OrderStatistic())
                        .Aggregate((s1, s2) => new OrderStatistic
                        {
                            OrderCount = s1.OrderCount + s2.OrderCount,
                            TotalRevenue = s1.TotalRevenue + s2.TotalRevenue
                        });

                    var orderStatistic = new OrderStatistic
                    {
                        OrderCount = statistics.OrderCount,
                        TotalRevenue = statistics.TotalRevenue
                    };
                    return orderStatistic;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
