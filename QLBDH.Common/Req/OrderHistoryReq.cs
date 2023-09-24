using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBDH.Common.Req
{
    public class OrderHistoryReq
    {
        public int userId { get; set; }

        public int Page { get; set; }
        public int Size { get; set; }

    }
}