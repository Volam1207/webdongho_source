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
    public class OrderController : ControllerBase
    {
        private OrderSvc _OrderSvc;
        public OrderController()
        {
            _OrderSvc = new OrderSvc();
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var res = new SingleRsp();
            res = _OrderSvc.Read(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] OrderReq req)
        {
            if (req == null)
            {
                return BadRequest("Invalid data");
            }

            var res = _OrderSvc.Create(req);
            return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult UpdateOrderById([FromBody] OrderReq req)
        {
            var res = _OrderSvc.Update(req);
            if (res.Data == null)
                return NotFound(res.Message);
            return Ok(res);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOrderById(int id)
        {
            var res = _OrderSvc.Delete(id);
            if (res.Data == null)
                return NotFound("Order not found");
            return Ok(res);
        }
        [HttpGet("GetOrdersInAugust2023")]
        public ActionResult<SingleRsp> GetOrdersInAugust2023()
        {
            var response = _OrderSvc.GetOrdersInAugust2023();
            return Ok(response);
        }
        [HttpGet("GetProductsPurchasedByUserId/{userId}")]
        public ActionResult<SingleRsp> GetProductsPurchasedByUserId(int userId)
        {
            var response = _OrderSvc.GetProductsPurchasedByUserId(userId);
            return Ok(response);
        }
        [HttpGet("GetTotalQuantitySoldByProductId/{productId}")]
        public ActionResult<SingleRsp> GetTotalQuantitySoldByProductId(int productId)
        {
            var response = _OrderSvc.GetTotalQuantitySoldByProductId(productId);
            return Ok(response);
        }

        [HttpGet("monthly-statistic")]
        public IActionResult GetMonthlyStatistics(int year, int month)
        {
            var statistics = _OrderSvc.GetMonthlyStatistics(year, month);

            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }
        [HttpGet("yearly-statistic")]
        public IActionResult GetYearlyStatistics(int year)
        {
            var statistics = _OrderSvc.GetYearlyStatistics(year);

            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }
    }

}
