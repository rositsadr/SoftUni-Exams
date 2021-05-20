using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int countBoxesPaint = int.Parse(Console.ReadLine());
            int countRolls = int.Parse(Console.ReadLine());
            double pricePairGloves = double.Parse(Console.ReadLine());
            double pricePaintingBrush = double.Parse(Console.ReadLine());

            double pricePaint = countBoxesPaint * 21.50;
            double priceRolls = countRolls * 5.20;
            double priceGloves = Math.Ceiling(0.35 * countRolls) * pricePairGloves;
            double priceBrushes = Math.Floor(0.48 * countBoxesPaint) * pricePaintingBrush;

            double allPrice = pricePaint + priceRolls + priceGloves + priceBrushes;
            double priceDelivery = allPrice / 15;
            Console.WriteLine($"This delivery will cost {priceDelivery:F2} lv.");
        }
    }
}
