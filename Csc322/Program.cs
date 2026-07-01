using System;
using Csc322.DesignPatterns.Strategy;
using Simulation.Interfaces;

namespace Simulation
{
    class WhitePerson : Person
    {
        public override void Speak(string words)
        {
            base.Speak(words);
        }
    }
    
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
            
            Person person = new WhitePerson();
            person.Name = "sherif";
            person.Speak("I am new to C#, please be nice!");

            B b = new B();
            b.G();
        }
        
    }
}
