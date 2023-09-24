using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBDH.BLL;
using QLBDH.Common.Req;
using QLBDH.Common.Rsp;

namespace QLBDH.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductSvc productSvc;
        public ProductController()
        {
            productSvc = new ProductSvc();
        }
        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody] ProductReq productReq)
        {
            var res = new SingleRsp();
            res = productSvc.CreateProduct(productReq);
            return Ok(res);
        }
        [HttpPost("search-product")]
        public IActionResult SearchProduct([FromBody] SearchProductReq searchProductReq)
        {
            var res = new SingleRsp();
            res.Data = productSvc.SearchProduct(searchProductReq);
            return Ok(res);
        }
        [HttpPut("update-product")]
        public IActionResult UpdateProduct([FromBody] ProductReq productReq)
        {
            var res = new SingleRsp();
            res = productSvc.UpdateProduct(productReq);
            return Ok(res);
        }
        [HttpDelete("delete-product")]
        public IActionResult DeleteProduct(int productId)
        {
            var res = new SingleRsp();
            res = productSvc.DeleteProduct(productId);
            return Ok(res);
        }
        [HttpGet("get-product/{productId}")]
        public IActionResult GetProductById(int productId)
        {
            var res = productSvc.GetProductById(productId);
            return Ok(res);
        }
    }
}
