using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            double priceLuggageOver20Kg = double.Parse(Console.ReadLine());
            double kilosLuggage = double.Parse(Console.ReadLine());
            int daysBeforTheTrip = int.Parse(Console.ReadLine());
            int countLuggages = int.Parse(Console.ReadLine());
            double priceOver = 0;

            if (kilosLuggage < 10)
            {
                priceOver = priceLuggageOver20Kg * 0.20;
            }
            else if (kilosLuggage <= 20)
            {
                priceOver = priceLuggageOver20Kg * 0.50;
            }
            else if (kilosLuggage > 20)
            {
                priceOver = priceLuggageOver20Kg;
            }

            if (daysBeforTheTrip > 30)
            {
                priceOver += priceOver * 0.10;
            }
            else if (daysBeforTheTrip >= 7)
            {
                priceOver += priceOver * 0.15;
            }
            else 
            {
                priceOver += priceOver * 0.40;
            }

            Console.WriteLine($"The total price of bags is: {(priceOver*countLuggages):F2} lv.");
        }
    }
}
