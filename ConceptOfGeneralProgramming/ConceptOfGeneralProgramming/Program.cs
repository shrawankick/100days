using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptOfGeneralProgramming
{
    class Program
    {
        public class Bird
        {
            //int value = 10;
            public Bird(int value =10)
            {
                
                Console.WriteLine($"Bird() called with {value}");
                
            }
         virtual  public void Fly()
            {
                Console.WriteLine("birds will fly ");
            }
        }

        public class Parrot : Bird
        {
            public Parrot(int value) : base(value)
            {
                Console.WriteLine($"Parrot called with {value}");
            }
            
        }
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

        static void Main(string[] args)
        {
             Parrot parrot = new Parrot(450);
            Ostrich ostrich = new Ostrich(10);
            parrot.Fly();
            ostrich.Fly();
            Console.WriteLine(":::DONE:::");
            Console.ReadLine();
        }
    }
}
