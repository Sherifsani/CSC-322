using System;
using Csc322.DesignPatterns.Strategy;

namespace Simulation
{
    class Program 
    {
        static void Main(string[] args)
        {
            Duck mallardDuck = new MallardDuck();
            
            mallardDuck.Swim();
            mallardDuck.Display();
            mallardDuck.PerformFly();
            mallardDuck.PerformQuack();
            
            Console.WriteLine("\nchanging mallardDuck quack behaviour");
            mallardDuck.QuackBehavior = new SqueakQuack();
            mallardDuck.PerformQuack();
        }
        
    }
}
