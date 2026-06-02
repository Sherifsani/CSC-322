namespace Csc322.DesignPatterns.Strategy;

public class MallardDuck : Duck
{
    public MallardDuck()
    {
        this.FlyBehavior = new FlyWithWings();
        this.QuackBehavior = new SimpleQuack();
    }
    public override void Display()
    {
        Console.WriteLine("I am a Mallard Duck");
    }
}