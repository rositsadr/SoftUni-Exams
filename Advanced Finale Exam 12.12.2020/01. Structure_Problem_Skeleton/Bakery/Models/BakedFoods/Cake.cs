using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.BakedFoods
{
    public class Cake : BakedFood
    {
        private const int cakePortion = 245;

        public Cake(string name, decimal price) : base(name, cakePortion, price)
        {
        }
    }
}
