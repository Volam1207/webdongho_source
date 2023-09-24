using QLBDH.Common.DAL;
using QLBDH.Common.Req;
using QLBDH.Common.Rsp;
using QLBDH.DAL.Models;

namespace QLBDH.DAL
{
    public class UserRep : GenericRep<WatchStoreContext, User>
    {
        public UserRep()
        {

        }
        public UserRep(WatchStoreContext context)
        {
            Context = context;
        }
        public override User Read(int id)
        {
            try
            {
                var res = All.FirstOrDefault(c => c.UserId == id);
                if (res == null)
                {
                    Console.WriteLine($"Can not find User with ID {id}");
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to read User with ID {id}: {ex.Message}");
                throw;
            }
        }
        public SingleRsp CreateUser(User user)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                var p = Context.Users.Add(user);
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
        public SingleRsp UpdateUserById(User user)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                var p = Context.Update(user);
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

        public SingleRsp DeleteuserById(int id)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                var user = Context.Users.Find(id);
                Context.Users.Remove(user);
                Context.SaveChanges();
                t.Commit();

                res.Data = user;

            }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }
        public List<Order> GetOrderHistory(OrderHistoryReq req)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new WatchStoreContext())
                {
                    var query = from u in context.Users
                                join o in context.Orders on u.UserId equals req.userId
                                select o;
                    return query.ToList();

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
