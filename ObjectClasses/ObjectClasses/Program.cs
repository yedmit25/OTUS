namespace ObjectClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = new Stack("asdf54", "b", "c", "d", "sdfsdf");

            try
            {

                Console.WriteLine(s.Top);

                s.Pop(out string ss);

                Console.WriteLine(ss);

                Console.WriteLine(s.Top);

                var sm = new Stack();

                Console.WriteLine(sm.Top);



                sm.Pop(out string stm);
                Console.WriteLine(stm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
