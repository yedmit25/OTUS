using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees_Heaps
{
    internal class EmployeeTree
    {
        /// <summary>
        /// Корень бинарного дерева
        /// </summary>
        private EmployeeNode root;

        /// <summary>
        /// Добавление данных в бинарное дерево
        /// </summary>
        public void Add(string name, int salary)
        {
            root = AddRecursive(root, name, salary);
        }

        /// <summary>
        /// Добавление нового узла в бинарное дерево
        /// </summary>
        private EmployeeNode AddRecursive(EmployeeNode node, string name, int salary)
        {
            if (node == null)
            {
                return new EmployeeNode(name, salary);
            }

            if (salary < node.Salary)
            {
                node.Left = AddRecursive(node.Left, name, salary);
            }
            else
            {
                node.Right = AddRecursive(node.Right, name, salary);
            }

            return node;
        }

        public void InOrderTraversal()
        {
            InOrderTraversalRecursive(root);
        }
        /// <summary>
        /// Поиск узла по значению
        /// </summary>
        private void InOrderTraversalRecursive(EmployeeNode node)
        {
            if (node != null)
            {
                InOrderTraversalRecursive(node.Left);
                Console.WriteLine($"{node.Name} - {node.Salary}");
                InOrderTraversalRecursive(node.Right);
            }
        }

        public string FindEmployeeBySalary(int salary)
        {
            return FindEmployeeBySalaryRecursive(root, salary);
        }

        private string FindEmployeeBySalaryRecursive(EmployeeNode node, int salary)
        {
            if (node == null)
            {
                return null;
            }

            if (salary == node.Salary)
            {
                return node.Name;
            }

            return salary < node.Salary
                ? FindEmployeeBySalaryRecursive(node.Left, salary)
                : FindEmployeeBySalaryRecursive(node.Right, salary);
        }
    }
}
