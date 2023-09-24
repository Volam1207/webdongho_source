using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBDH.BLL;
using QLBDH.Common.Req;
using QLBDH.Common.Rsp;
using QLBDH.DAL;

namespace QLBDH.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private OrderItemSvc _OrderItemSvc;
        public OrderItemController()
        {
            _OrderItemSvc = new OrderItemSvc();
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id)
        {
            var res = new SingleRsp();
            res = _OrderItemSvc.Read(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult CreateOrderItem([FromBody] OrderItemReq req)
        {
            if (req == null)
            {
                return BadRequest("Invalid data");
            }

            var res = _OrderItemSvc.Create(req);
            return Ok(res);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateOrderItemById(int id, [FromBody] OrderItemReq req)
        {
            var res = _OrderItemSvc.Update(id, req);
            if (res.Data == null)
                return NotFound(res.Message);
            return Ok(res);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOrderItemById(int id)
        {
            var res = _OrderItemSvc.Delete(id);
            if (res.Data == null)
                return NotFound("OrderItem not found");
            return Ok(res);
        }
    }

}
