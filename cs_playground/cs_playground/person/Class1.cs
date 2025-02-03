
using System.Security.Cryptography.X509Certificates;

namespace person.shared;

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
  delegate int DelegateWithMatchingSignature(string s);




}
