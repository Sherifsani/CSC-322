using System;
namespace Csc322.DesignPatterns.Strategy;

public abstract class Duck
{
    
    public IFlyBehavior FlyBehavior { get; set; }
    public IQuackBehavior QuackBehavior { get; set; }


    public void Swim()
    {
        Console.WriteLine($"This duck is swimming");
    }

    public abstract void Display();

    public void PerformQuack()
    {
        QuackBehavior?.Quack();
    }

    public void PerformFly()
    {
        FlyBehavior?.Fly();
    }

}