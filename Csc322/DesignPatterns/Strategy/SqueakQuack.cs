namespace Csc322.DesignPatterns.Strategy;

public class SqueakQuack : IQuackBehavior
{
    public void Quack()
    {
        System.Console.WriteLine("squeak!");
    }
}