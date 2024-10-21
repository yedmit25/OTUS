using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees_Heaps
{
    internal class EmployeeNode
    {
        public string Name;
        public int Salary;
        /// <summary>
        /// Левая ветка
        /// </summary>
        public EmployeeNode Left;
        /// <summary>
        /// Правая ветка
        /// </summary>
        public EmployeeNode Right;

        /// <summary>
        /// Начальное расположение узла
        /// </summary>
        public EmployeeNode(string name, int salary)
        {
            Name = name;
            Salary = salary;
            Left = null;
            Right = null;
        }
    }
}
