using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int pepoleHandledByFirst = int.Parse(Console.ReadLine());
            int pepoleHandledBySecond = int.Parse(Console.ReadLine());
            int pepoleHandledByThird = int.Parse(Console.ReadLine());
            int totalPeople = int.Parse(Console.ReadLine());
            int hours = 0;

            int peopleHandledPerHour = pepoleHandledByFirst + pepoleHandledBySecond + pepoleHandledByThird;
            while (totalPeople>0)
            {
                hours++;
                totalPeople -= peopleHandledPerHour;
            }
            int breakHours = hours / 3;
            if (hours%3 == 0)
            {
                breakHours--;
            }

            hours += breakHours;
            if (hours<0)
            {
                hours = 0;
            }

            Console.WriteLine($"Time needed: {hours}h.");
        }
    }
}
