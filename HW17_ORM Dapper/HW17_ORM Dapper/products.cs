using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW17_ORM_Dapper
{
    internal class products
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int stockquantity { get; set; }
        public decimal price { get; set; }
    }
}
