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
using System.Data;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;

namespace QLBDH.BLL
{
    public class UserSvc:GenericSvc<UserRep, User>
    {
        private UserRep UserRep;
        public UserSvc()
        {
            UserRep = new UserRep();
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }
        
        public SingleRsp Create(UserReq req)
        {
            var res = new SingleRsp();
            if (!IsValidEmail(req.Email))
            {
                return new SingleRsp($"Invalid email format: {req.Email}");
            }
            var user = new User
            {
                UserId = req.UserID,
                Username = req.Username,
                Password = req.Password,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email,
                Phone = req.Phone,
                Address = req.Address,
                Role = req.Role
            };

            _rep.CreateUser(user);
            res.Data = user;
            return res;
        }
        public SingleRsp Update(UserReq req)
        {
            var res = new SingleRsp();
            if (!IsValidEmail(req.Email))
            {
                return new SingleRsp($"Invalid email format: {req.Email}");
            }
            var user = _rep.Read(req.UserID);
            if (user == null)
            {
                res.SetError($"Not found user with id {req.UserID}");
            }
            else
            {
                user.Username = req.Username;
                user.Password = req.Password;
                user.FirstName = req.FirstName;
                user.LastName = req.LastName;
                user.Email = req.Email;
                user.Address = req.Address;
                user.Role = req.Role;
                _rep.UpdateUserById(user);
                res.Data = user;
            }
            
            return res;
        }

        public SingleRsp Delete(int id)
        {
            var res = new SingleRsp();      
            res.Data = _rep.DeleteuserById(id);
            return res;
        }
        public SingleRsp GetOrderHistory(OrderHistoryReq req)
        {
            var res = new SingleRsp();
            var orders = _rep.GetOrderHistory(req);
            var offset = (req.Page - 1) * req.Size;
            var total = orders.Count;
            int totalPage = (total % req.Size) == 0 ? (int)(total / req.Size) :
                (int)(1 + (total / req.Size));
            var data = orders.Skip(offset).Take(req.Size).ToList();
            var tmp = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = req.Page,
                Size = req.Size

            };
            res.Data = tmp;
            return res;    
        }
    }
}
