using QLBDH.Common.BLL;
using QLBDH.Common.Rsp;
using QLBDH.DAL;
using QLBDH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBDH.Common.Req;
using System.Diagnostics;

namespace QLBDH.BLL
{
    public class OrderItemSvc:GenericSvc<OrderItemRep, OrderItem>
    {
        private OrderItemRep orderItemRep;
        private ProductRep productRep;
        public OrderItemSvc()
        {
            orderItemRep = new OrderItemRep();
            productRep = new ProductRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }
        public SingleRsp Create(OrderItemReq req)
        {
            var res = new SingleRsp();
            var product = productRep.Read(req.ProductID);

            if (product == null)
            {
                res.SetError("Product not found");
            }
            else
            { 
                decimal price = product.Price * req.Quantity;
                OrderItem orderItem = new OrderItem();
                orderItem.OrderItemId = req.OrderItemID;
                orderItem.OrderId = req.OrderID;
                orderItem.ProductId = req.ProductID;
                orderItem.Quantity = req.Quantity;
                orderItem.Price = productRep.Read(req.ProductID).Price * req.Quantity;
                res.Data = _rep.CreateOrderItem(orderItem);
            }
            
            return res;
        }
        public SingleRsp Update(int id, OrderItemReq req)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderItemId = req.OrderItemID;
            orderItem.OrderId = req.OrderID;
            orderItem.ProductId = req.ProductID;
            orderItem.Quantity = req.Quantity;
            orderItem.Price = productRep.Read(req.ProductID).Price * req.Quantity;
            return _rep.UpdateOrderItemById(id, orderItem);
        }

        public SingleRsp Delete(int id)
        {
            var res = new SingleRsp();      
            res.Data = _rep.DeleteOrderItemById(id);
            return res;
        }

    }
}
