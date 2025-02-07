using System.Runtime.CompilerServices;

namespace b;
internal class Program
{
  private static void Main(string[] args)
  {
    // list of person

    Person[] people = { new Person("z"), new Person("bbbbbbbb"), new Person("aaaaaaaaaaaaaaaaaaaaaaaa") };
    Array.Sort(people, new CompareTwo());
    // listing them
    Console.WriteLine("________________________");
    foreach (var person in people)
    {
      Console.WriteLine(person.FirstName);
    }
    Console.WriteLine("________________________");
    Console.WriteLine("________________________");
    // Expermenting with the system.threads :\
    Console.WriteLine("Current Thread ID: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
    Console.WriteLine("________________________");
    Console.WriteLine("Is Background: " + System.Threading.Thread.CurrentThread.IsBackground);
    Console.WriteLine("________________________");
    Console.WriteLine(Thread.CurrentThread.IsBackground);
    Console.WriteLine("________________________");
    Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
    Console.WriteLine("________________________");
    Console.WriteLine(Thread.CurrentThread.IsAlive);
    Console.WriteLine("________________________");
    Console.WriteLine(Thread.CurrentThread.Priority);
    Console.WriteLine("________________________");
    Console.WriteLine(Thread.CurrentThread.Name);
    Console.WriteLine("________________________");
    Console.WriteLine(Thread.CurrentThread.Priority);
    Console.WriteLine("________________________");
    Console.WriteLine(Thread.CurrentThread.Name);
    Console.WriteLine("________________________");
    Console.WriteLine("________________________");
  }
}
public interface IMediaPlyaer
{
  // pub abstract interfaces
  void Play();
  void Pause();
  // BUT XD
  void Stop()
  {
    Console.WriteLine("Stop");
  }
}

public class MusicPlayer : IMediaPlyaer
{
  public void Play()
  {
    Console.WriteLine("Play Music");
  }
  public void Pause()
  {
    Console.WriteLine("Pause Music");
  }

}

class Person
{
  public string FirstName { get; set; }
  public Person(string name)
  {
    FirstName = name;
  }
  // List of Person
}
class CompareTwo : IComparer<Person>
{
  public int Compare(Person? x, Person? y)
  {
    if (x!.FirstName.Length > y!.FirstName.Length)
    {
      return 1;
    }
    else if (x.FirstName.Length < y.FirstName.Length)
    {
      return -1;
    }
    else
    {
      return 0;
    }
  }
}