using System;
using System.Linq;
namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] neighborhood = Console.ReadLine()
                .Split('@')
                .Select(int.Parse)
                .ToArray();
            string input = Console.ReadLine();
            int index = 0;
            int valentineCounter = 0;

            while(input != "Love!")
            {
                string[] command = input.Split();
                int length = int.Parse(command[1]);
                index += length;
                if (index>neighborhood.Length-1)
                {
                    index = 0;
                }
                neighborhood[index] -= 2;
                if (neighborhood[index]==0)
                {
                    Console.WriteLine($"Place {index} has Valentine's day.");
                    valentineCounter++;
                }
                else if (neighborhood[index]<0)
                {
                    Console.WriteLine($"Place {index} already had Valentine's day.");
                }

                input = Console.ReadLine();
            }

            Console.WriteLine($"Cupid's last position was {index}.");

            if (valentineCounter == neighborhood.Length)
            {
                Console.WriteLine("Mission was successful.");
            }
            else
            {
                int housesLeft = neighborhood.Length - valentineCounter;
                Console.WriteLine($"Cupid has failed {housesLeft} places.");
            }
        }
    }
}
