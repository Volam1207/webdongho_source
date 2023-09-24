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
    public class UserController : ControllerBase
    {
        private UserSvc _UserSvc;
        public UserController()
        {
            _UserSvc = new UserSvc();
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var res = new SingleRsp();
            res = _UserSvc.Read(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserReq req)
        {
            if (req == null)
            {
                return BadRequest("Invalid data");
            }

            var res = _UserSvc.Create(req);
            return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult UpdateUserById([FromBody] UserReq req)
        {
            var res = _UserSvc.Update(req);
            if (res.Data == null)
                return NotFound(res.Message);
            return Ok(res);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            var res = _UserSvc.Delete(id);
            if (res.Data == null)
                return NotFound("User not found");
            return Ok(res);
        }
        [HttpGet("get-order-history")]
        public IActionResult GetOrderHistory([FromQuery] OrderHistoryReq req)
        {
            var res = _UserSvc.GetOrderHistory(req);
            if (res.Data == null)
                return NotFound("User not found");
            return Ok(res);
        }
    }

}
