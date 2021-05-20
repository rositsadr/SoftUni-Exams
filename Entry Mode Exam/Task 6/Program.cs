using System;

namespace Task_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int record = int.Parse(Console.ReadLine());
            int countHoles = int.Parse(Console.ReadLine());
            int countHits = 0;

            for (int hole = 1; hole <= countHoles; hole++)
            {
                int powerNeededForTheNextHole = int.Parse(Console.ReadLine());
                double powerleft = 0;

                while (powerleft < powerNeededForTheNextHole)
                {
                    string power = Console.ReadLine();
                    countHits++;
                    double countpower = 0;
                    for (int m = 0; m < power.Length; m++)
                    {
                        countpower += power[m];
                    }
                    powerleft += (countpower / power.Length);
                }

                Console.WriteLine($"New hole reached! Hits so far: {countHits}");
            }

            if (countHits < record)
            {
                Console.WriteLine($"Yes, you won! Total hits: {countHits}");
            }
            else
            {
                Console.WriteLine($"Better luck next time... Total hits: {countHits}");
            }
        }
    }
}
