
namespace test;

partial class Program
{
    delegate int myTestDelegate(string s);
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
        //8 
        Console.WriteLine(p1.AngerLevel);
        p1.Shout += (sender, args) => Console.WriteLine($"The person {sender} is shouting. Anger level: {p1.AngerLevel}");
        p1.Poke();
        p1.Poke();
        p1.Poke();
        Console.WriteLine(p1.AngerLevel);
        Console.WriteLine("________________________");
        // Usage:
        int MyMethod(string s)
        {
            return s.Length;
        }
        // 1) you have to create a delgate(function pointer) instance 
        myTestDelegate del = new myTestDelegate(MyMethod); // Create a delegate instance
                                                           //2) then you can call the instance of the functional pointer(delegate)
        int length = del.Invoke("Hello"); // Invoke the method through the delegate
        Console.WriteLine(length);
        // so basically delegates are just cpp function pointers that adds new modern features (MAJORLY being TYPE SAFE POINTERS)
        //events 

        #region  usage of button class to demonstrate events
        // Usage:
        Button myButton = new Button();
        myButton.Click += MyClickHandler; // Subscribe to the event

        void MyClickHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Button clicked!");
        }

        myButton.OnClick(); // Simulate a button click
        myButton.OnClick(); // Simulate a button click
        myButton.OnClick(); // Simulate a button click
        myButton.OnClick(); // Simulate a button click
        #endregion







    }
    //testing events
    #region buttonClass 
    public class Button
    {
        // Delegate type for the event:
        public delegate void ClickHandler(object sender, EventArgs e);

        // The event itself:
        public event ClickHandler? Click; // Or: public event EventHandler Click; (common shortcut)

        public void OnClick()
        {
            // Raise the event (notify subscribers):
            if (Click != null) // Check if there are any subscribers
            {
                Click(this, EventArgs.Empty); // Invoke the delegate (and thus, all subscribed methods)
            }
        }
    }
    #endregion

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
            //new C# is to use the a feature similar to js/ts shout?. it exists then invoke it on the same parameters
        }
        #endregion events




    }

}