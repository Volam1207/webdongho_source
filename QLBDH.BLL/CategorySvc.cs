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

namespace QLBDH.BLL
{
    public class CategorySvc:GenericSvc<CategoryRep, Category>
    {
        private CategoryRep categoryRep;
        public CategorySvc()
        {
            categoryRep = new CategoryRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }
        public SingleRsp Create(CategoryReq req)
        {
            var res = new SingleRsp();
            Category cat = new Category();
            cat.CategoryId = req.CategoryId;
            cat.CategoryName = req.CategoryName;
            _rep.CreateCategory(cat);
            res.Data = cat;
            return res;
        }
        public SingleRsp Update(CategoryReq req)
        {
            var res = new SingleRsp();
            var cat = _rep.Read(req.CategoryId);
            if(cat == null)
            {
                res.SetError("Not found");
            }
            cat.CategoryName = req.CategoryName.ToString();
            _rep.UpdateCategoryById(cat);
            res.Data = cat;
            return res;
        }

        public SingleRsp Delete(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.DeleteCategoryById(id);
            return res;
        }

    }
}
