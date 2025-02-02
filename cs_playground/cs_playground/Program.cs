using Env = System.Environment;
namespace cs_playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("Hello, World!");
            writeMultiplicationTable(5);
            DateTime dateTest = new(year: 2025, month: 12, day: 3);
            Console.WriteLine(dateTest.ToString());
            Console.WriteLine($"5! = {recursiveFactorial()}");
            Console.WriteLine($"program took {DateTime.Now - start}ms");
            int test = new Random().Next(1, 5);
            string pre = test switch
            {
                1 => "1st",
                2 => "2nd",
                3 => "3rd",
                4 => "4th",
                _ => "default"
            };

            Console.WriteLine(pre);
            Console.WriteLine(Env.OSVersion.ToString());
            int x = 0, y = 1;

            swap(ref x, ref y);
            Console.WriteLine($"{x},{y}");
            var p1 = new person.shared.Person("test", "test", 15);
            Console.WriteLine(p1);

        }
        static void swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;


        }
        static int recursiveFactorial(int n = 5)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            return recursiveFactorial(n - 1) * n;
        }
        static void writeMultiplicationTable(int n)
        {
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    Console.WriteLine($"{i}*{j} = {i * j}");

                }
                Console.WriteLine("____________________");
            }

        }
    }
}
