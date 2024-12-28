using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Observable
{
    public class Customer
    {
        public void OnItemChanged(Item item, string action)
        {
            Console.WriteLine($"Товар {action}: {item.Name} (ID: {item.Id})");
        }
    }
}
