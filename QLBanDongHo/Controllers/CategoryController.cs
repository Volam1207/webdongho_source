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
    public class CategoryController : ControllerBase
    {
        private CategorySvc _categorySvc;
        public CategoryController()
        {
            _categorySvc = new CategorySvc();
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var res = new SingleRsp();
            res = _categorySvc.Read(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult CreateCategory([FromBody] CategoryReq req)
        {
            if (req == null)
            {
                return BadRequest("Invalid data");
            }

            var res = _categorySvc.Create(req);
            return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult UpdateCategoryById([FromBody] CategoryReq req)
        {
            var res = _categorySvc.Update(req);
            if (res.Data == null)
                return NotFound(res.Message);
            return Ok(res);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            var res = _categorySvc.Delete(id);
            if (res.Data == null)
                return NotFound("Category not found");
            return Ok(res);
        }
    }

}
