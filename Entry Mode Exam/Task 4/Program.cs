using System;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            double amountForCar = double.Parse(Console.ReadLine());
            double monthlyExpense = double.Parse(Console.ReadLine());
            double savings = 0;
            int countMonths = 0;

            while (savings < amountForCar && savings >= 0)
            {
                double monthlyProfits = double.Parse(Console.ReadLine());
                savings += (monthlyProfits - monthlyExpense);
                countMonths++;
            }

            if (savings < 0)
            {
                Console.WriteLine("It seems you have bankrupted...");
                Console.WriteLine($"You have worked {countMonths / 12} years {countMonths % 12} months");
            }
            else
            {
                Console.WriteLine("You did it!");
                Console.WriteLine($"It took you {countMonths / 12} years {countMonths % 12} months");
            }
        }
    }
}
