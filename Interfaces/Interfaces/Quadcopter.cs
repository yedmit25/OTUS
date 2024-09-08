using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class Quadcopter : IFlyingRobot, IChargeable
    {
        List<string> _components = new List<string>() { "rotor1", "rotor2", "rotor3", "rotor4" };
        public void GetComponents()
        {
            foreach (var component in _components)
            {
                Console.WriteLine(component);
            }
              
        }

        public string GetRobotType()
        {
            return "Я Карлсон";
        }


        public void Charge()
        {
            Console.WriteLine("Charging...");

            Thread.Sleep(3000);

            Console.WriteLine("Charged");
        }

        string IRobot.GetInfo()
        {
            return $"Версия: {typeof(Program).Assembly.GetName().Version}\n{{File.GetCreationTime(AppDomain.CurrentDomain.BaseDirectory +\r\n                      System.Diagnostics.Process.GetCurrentProcess().ProcessName + \".exe\")}}\r\n                \"\"\";";
        }

        string IChargeable.GetInfo()
        {
            return $"Версия: {typeof(Program).Assembly.GetName().Version}\n{{File.GetCreationTime(AppDomain.CurrentDomain.BaseDirectory +\r\n                      System.Diagnostics.Process.GetCurrentProcess().ProcessName + \".exe\")}}\r\n                \"\"\";";
        }
    }
}
