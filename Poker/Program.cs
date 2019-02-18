using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {

            var list_card = new List<Card>()
            {
                new Card(){Number=5},
                new Card(){Number=5},
                new Card(){Number=5},
                new Card(){Number=6},
                new Card(){Number=6},
                new Card(){Number=6},
                new Card(){Number=7},
                new Card(){Number=8},
        };



            var list = list_card.GroupBy(m => m.Number).Where(p => p.Count() == 3).ToList();
            
            Console.WriteLine("Hello World!");
        }
    }
}
