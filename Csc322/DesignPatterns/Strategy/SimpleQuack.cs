using System;

namespace Csc322.DesignPatterns.Strategy;

public class SimpleQuack : IQuackBehavior
{
    public void Quack()
    {
        Console.WriteLine("simple quack!");
    }
}