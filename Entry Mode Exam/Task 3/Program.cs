using System;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string ticketClass = Console.ReadLine();
            double flightDistance = double.Parse(Console.ReadLine());
            int countPassengers = int.Parse(Console.ReadLine());
            double priceTickets = 0;

            if (flightDistance < 1500)
            {
                switch (ticketClass)
                {
                    case "Economy":
                        priceTickets = countPassengers * 59.99;
                        break;
                    case "Premium":
                        priceTickets = countPassengers * 179.99;
                        break;
                    case "Business":
                        priceTickets = countPassengers * 254.99;
                        break;
                }
            }
            else if (flightDistance <= 3500)
            {
                switch (ticketClass)
                {
                    case "Economy":
                        priceTickets = countPassengers * 184.99;
                        break;
                    case "Premium":
                        priceTickets = countPassengers * 279.99;
                        break;
                    case "Business":
                        priceTickets = countPassengers * 379.99;
                        break;
                }
            }
            else
            {
                switch (ticketClass)
                {
                    case "Economy":
                        priceTickets = countPassengers * 269.99;
                        break;
                    case "Premium":
                        priceTickets = countPassengers * 394.99;
                        break;
                    case "Business":
                        priceTickets = countPassengers * 619.99;
                        break;
                }
            }

            if (countPassengers > 6)
            {
                priceTickets -= priceTickets * 0.20;
            }

            Console.WriteLine($"The total price of the tickets is: {priceTickets:F2} lv.");
        }
    }
}
