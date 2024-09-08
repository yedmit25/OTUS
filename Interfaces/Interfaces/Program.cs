namespace Interfaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Quadcopter quadcopter = new Quadcopter();

            Console.WriteLine(quadcopter.GetRobotType());

            Console.WriteLine("How many rotors are there?");

            quadcopter.GetComponents();

            Console.WriteLine($"What are doing?");
            quadcopter.Charge();



        }
    }
}
