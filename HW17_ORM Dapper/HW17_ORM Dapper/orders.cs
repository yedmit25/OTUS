using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW17_ORM_Dapper
{
    internal class orders
    {
        public int Id { get; set; }
        public int customerid { get; set; }
        public int productid { get; set; }
        public int quantity { get; set; }
    }
}
