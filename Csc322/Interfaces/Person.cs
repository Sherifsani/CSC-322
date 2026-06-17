namespace Simulation.Interfaces;

public abstract class Person
{
    public string Name { get; set; }

    public void Greet()
    {
        Console.WriteLine($"Hello {Name}");
    }

    public virtual void Speak(string words)
    {
        Console.WriteLine($"Hello I'm {Name}, {words}");
    }

}

interface IInterface
{
    
}