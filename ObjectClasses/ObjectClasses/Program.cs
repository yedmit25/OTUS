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
                string ss;
                ss = s.Pop();

                Console.WriteLine(ss);

                Console.WriteLine(s.Top);

                var sm = new Stack();

                Console.WriteLine(sm.Top);


                var st1 = new Stack("1", "2", "3", "5");
                var st2 = new Stack("_zxq", "_poi_", "_bhuy");

                foreach (var x in st1)
                {
                    Console.WriteLine("Стек 1 " + x);
                }

                foreach (var x in st2)
                {

                    Console.WriteLine("Стек 2 " + x);
                }



                st1.Merge(st2);

                foreach (var x in st1)
                {
                    Console.WriteLine("Стек после объединения " + x);
                }
                string stm = sm.Pop();
                Console.WriteLine(stm);

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
