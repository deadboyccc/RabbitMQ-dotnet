using person.shared;

namespace test;

class Program
{
    static void Main(string[] args)
    {
        Person p1 = new Person("Test", "Biden", 30);
        Console.WriteLine(p1);
        Console.WriteLine(p1);

        var del = new Person.returnFunLen(p1.original);
        int result = del("test");
        Console.WriteLine(result);
    }
}