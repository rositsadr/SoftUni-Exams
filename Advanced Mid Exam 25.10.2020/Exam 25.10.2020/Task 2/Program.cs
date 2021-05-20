using System;
using System.Linq;

namespace Task_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] dimentions = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int gardenRows = dimentions[0];
            int gardenCols = dimentions[1];
            int[,] garden = new int[gardenRows, gardenCols];

            for (int row = 0; row < gardenRows; row++)
            {
                for (int col = 0; col < gardenCols; col++)
                {
                    garden[row, col] = 0;
                }
            }

            string command = Console.ReadLine();

            while (command!= "Bloom Bloom Plow")
            {
                int[] flower = command
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                int flowerRow = flower[0];
                int flowerCol = flower[1];

                if (flowerRow < 0 || flowerRow >= gardenRows || flowerCol < 0 || flowerCol >= gardenCols)
                {
                    Console.WriteLine("Invalid coordinates.");
                }
                else
                {
                    for (int row = 0; row < gardenRows; row++)
                    {
                        garden[row, flowerCol] += 1;
                    }

                    for (int col = 0; col < gardenCols; col++)
                    {
                        garden[flowerRow, col] += 1;
                    }

                    garden[flowerRow, flowerCol] -= 1;
                }

                command = Console.ReadLine();
            }

            for (int row = 0; row < gardenRows; row++)
            {
                for (int col = 0; col < gardenCols; col++)
                {
                    Console.Write($"{garden[row,col]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
