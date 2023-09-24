using QLBDH.Common.BLL;
using QLBDH.Common.Rsp;
using QLBDH.DAL;
using QLBDH.DAL.Models;
using QLBDH.Common.Req;

namespace QLBDH.BLL
{
    public class OrderSvc:GenericSvc<OrderRep, Order>
    {
        private OrderRep orderRep;
        public OrderSvc()
        {
            orderRep = new OrderRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }
        public SingleRsp Create(OrderReq req)
        {
            var res = new SingleRsp();
            Order order = new Order();
            order.OrderId = req.OrderID;
            order.UserId = req.UserID;
            order.OrderDate = req.OrderDate;
            order.TotalAmount = req.TotalAmount;
            order.OrderStatus = req.OrderStatus;
            _rep.CreateOrder(order);
            res.Data = order;
            return res;
        }
        public SingleRsp Update(OrderReq req)
        {
            var res = new SingleRsp();
            var order = _rep.Read(req.OrderID);
            if (order == null)
            {
                res.SetError("Order not found");
            }
            else
            {
                order.OrderId = req.OrderID;
                order.UserId = req.UserID;
                order.OrderDate = req.OrderDate;
                order.TotalAmount = req.TotalAmount;
                order.OrderStatus = req.OrderStatus;
                _rep.UpdateOrderById(order);
            }
            res.Data = order;
            return res;
        }

        public SingleRsp Delete(int id)
        {
            var res = new SingleRsp(); 
            res.Data = _rep.DeleteOrderById(id);
            return res;
        }
        public SingleRsp GetOrdersInAugust2023()
        {
            var res = new SingleRsp();
            DateTime targetDate = new DateTime(2023, 8, 1);
            var ordersInAugust2023 = _rep.ReadAll().Where(o => o.OrderDate >= targetDate && o.OrderDate < targetDate.AddMonths(1)).ToList();

            res.Data = ordersInAugust2023;
            return res;
        }
        public SingleRsp GetProductsPurchasedByUserId(int userId)
        {
            var res = new SingleRsp();
            res.Data = _rep.GetProductsPurchaseByUserId(userId);
            return res;
        }
        public SingleRsp GetTotalQuantitySoldByProductId(int productId)
        {
            var res = new SingleRsp();
            res.Data = _rep.GetTotalQuantitySoldByProductId(productId);
            return res;
        }
        public OrderStatistic GetMonthlyStatistics(int year, int month)
        {
            return _rep.GetMonthlyStatistics(year, month);
        }
        public OrderStatistic GetYearlyStatistics(int year)
        {
            return _rep.GetYearlyStatistics(year);
        }
    }
}
