namespace Csc322.DesignPatterns.Strategy;

public class MuteQuack : IQuackBehavior
{
    public void Quack()
    {
        System.Console.WriteLine("Silent quack!");
    }
}