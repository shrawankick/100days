using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptOfGeneralProgramming
{
    /// <summary>
    /// Base Class 
    /// </summary>
    public class Bird
    {
        //int value = 10;
        public Bird(int value = 10)
        {

            Console.WriteLine($"Bird() called with {value}");

        }
        virtual public void Fly()
        {
            Console.WriteLine("birds will fly ");
        }
    }

    /// <summary>
    /// Derived class 
    /// </summary>
    public abstract class Parrot : Bird
    {
        public Parrot() : base()
        {
            Console.WriteLine($"Parrot called with ");
        }
        public int age = 32;
        public abstract void GetDetails(string color, string beek);

    }
    /// <summary>
    /// Derived class 2 
    /// </summary>
    public class Ostrich : Bird
    {
        public Ostrich(int value) : base(value)
        {
            Console.WriteLine($"Ostrich called with {value}");
        }
        override public void Fly()
        {
            Console.WriteLine("this bird will run");
        }
    }

    public class BirdsthatTalk: Parrot
    {
        public BirdsthatTalk()
        {
            Console.WriteLine($"birds that talk");
        }
        public override void GetDetails(string color, string beek)
        {
            Console.WriteLine($"this bird has {color}");
        }
    }

    /// <summary>
    /// Program class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Ostrich ostrich = new Ostrich(10);
            ostrich.Fly();
            BirdsthatTalk birdsthatTalk = new BirdsthatTalk();
            birdsthatTalk.GetDetails("Green", "round beak");
            birdsthatTalk.Fly();
            Console.WriteLine(":::DONE:::");
            Console.ReadLine();
        }
    }
}
