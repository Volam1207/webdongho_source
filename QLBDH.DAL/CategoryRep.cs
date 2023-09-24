using QLBDH.Common.DAL;
using QLBDH.Common.Req;
using QLBDH.Common.Rsp;
using QLBDH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBDH.DAL
{
    public class CategoryRep:GenericRep<WatchStoreContext, Category>
    {
        public CategoryRep()
        {

        }
        public override Category Read(int id)
        {
            try
            {
                var res = All.FirstOrDefault(c => c.CategoryId == id);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to read category with ID {id}: {ex.Message}");
                throw;
            }
        }
        public SingleRsp CreateCategory(Category category)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                Context.Categories.Add(category);
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
        public SingleRsp UpdateCategoryById(Category category)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                Context.Categories.Update(category);
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

        public SingleRsp DeleteCategoryById(int id)
        {
            var res = new SingleRsp();
            using var t = Context.Database.BeginTransaction();
            try
            {
                var category = Context.Categories.Find(id);
                Context.Categories.Remove(category);
                Context.SaveChanges();
                t.Commit();

                res.Data = category;
        }
            catch (Exception ex)
            {
                t.Rollback();
                res.SetError(ex.Message);
            }
            return res;
        }
    }
}
