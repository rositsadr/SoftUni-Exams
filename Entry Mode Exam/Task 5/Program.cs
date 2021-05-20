using System;

namespace Task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            double countPassengers = double.Parse(Console.ReadLine());
            double countWindowsSeats = 0;
            double countMiddleSeats = 0;
            double countPathSeats = 0;

            for (int i = 1; i <= countPassengers; i++)
            {
                string seatNumber= Console.ReadLine();

                if (seatNumber[1] == 'A' || seatNumber[1] == 'F')
                {
                    countWindowsSeats++;
                }
                else if (seatNumber[1] == 'B' || seatNumber[1] == 'E')
                {
                    countMiddleSeats++;
                }
                else if (seatNumber[1] == 'C' || seatNumber[1] == 'D')
                {
                    countPathSeats++;
                }
            }

            Console.WriteLine($"Window Seats: {(countWindowsSeats/countPassengers)*100:F2}%");
            Console.WriteLine($"Middle Seats: {(countMiddleSeats/countPassengers)*100:F2}%");
            Console.WriteLine($"Aisle Seats: {(countPathSeats/countPassengers)*100:F2}% ");
        }
    }
}
