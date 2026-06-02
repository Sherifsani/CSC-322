namespace Csc322.DesignPatterns.Strategy;

public class FlyNoWay : IFlyBehavior
{
    public void Fly()
    {
        Console.WriteLine("Flying not possible!");
    }
}