namespace BinaryTrees_Heaps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                EmployeeTree employeeTree = new EmployeeTree();
                Console.WriteLine("Введите информацию о сотрудниках:");

                while (true)
                {
                    Console.Write("Имя сотрудника (или пустая строка для завершения): ");
                    string name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }

                    Console.Write("Зарплата сотрудника: ");
                    if (int.TryParse(Console.ReadLine(), out int salary))
                    {
                        employeeTree.Add(name, salary);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод зарплаты. Попробуйте еще раз.");
                    }
                }

                Console.WriteLine("\nСотрудники в порядке возрастания зарплат:");
                employeeTree.InOrderTraversal();

                while (true)
                {
                    Console.Write("\nВведите зарплату для поиска: ");
                    if (int.TryParse(Console.ReadLine(), out int searchSalary))
                    {
                        string employeeName = employeeTree.FindEmployeeBySalary(searchSalary);
                        if (employeeName != null)
                        {
                            Console.WriteLine($"Сотрудник с зарплатой {searchSalary}: {employeeName}");
                        }
                        else
                        {
                            Console.WriteLine("Такой сотрудник не найден.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод зарплаты. Попробуйте еще раз.");
                    }

                    Console.WriteLine("Введите 0 для начала заново или 1 для повторного поиска зарплаты.");
                    if (int.TryParse(Console.ReadLine(), out int choice) && choice == 0)
                    {
                        break;
                    }
                }
        }
    }
    }
}
