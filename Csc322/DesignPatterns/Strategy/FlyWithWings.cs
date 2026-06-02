namespace Csc322.DesignPatterns.Strategy;

public class FlyWithWings : IFlyBehavior
{
    public void Fly()
    {
        Console.WriteLine("Flying with wings");
    }
}