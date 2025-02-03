
namespace test;

class Program
{
    static void Main(string[] args)
    {
        Person p1 = new Person("Test", "Biden", 30);
        Console.WriteLine(p1);
        Console.WriteLine(p1);
        var a = p1.methodIwantTocall("hello");
        Console.WriteLine(a);
        Person.DelegateWithMatchingSignature myDelegate = new Person.DelegateWithMatchingSignature(p1.methodIwantTocall);
        int answer = myDelegate("hfoaewjf");
        Console.WriteLine(answer);





    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Person(string firstName, string lastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age} years old";
        }
        public int methodIwantTocall(string s)
        {
            return s.Length;
        }
        public delegate int DelegateWithMatchingSignature(string s);
        public delegate void EventHandler<TEventsArgs>(object? sender, TEventsArgs e);
        #region events
        // Delegate field to define the event.
        // THE EVENT : SHOUT
        public EventHandler? Shout; // null initially || ? 

        // data field related to the event.
        public int AngerLevel;
        // Method to trigger the event in certain conditions.
        public void Poke()
        {
            AngerLevel++;
            if (AngerLevel < 3) return;

            // If something is listening to the event...
            if (Shout is not null)
            {
                // ...then call the delegate to "raise" the event.
                Shout(this, EventArgs.Empty);
            }
        }
        #endregion events




    }

}