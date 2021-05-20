using System;
using System.Text.RegularExpressions;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfInputs = int.Parse(Console.ReadLine());
            string pattern = @"^@#+[A-Z][A-Za-z\d]{4,}[A-Z]@#+$";
            for (int i = 1; i <= numberOfInputs; i++)
            {
                string input = Console.ReadLine();
                Match valid = Regex.Match(input, pattern);
                string productGroup = "";

                if (valid.Success)
                {
                    foreach (char symbol in valid.Value)
                    {
                        if (symbol>=48 && symbol<=57)
                        {
                            productGroup += symbol;
                        }
                    }           
                    if (productGroup=="")
                    {
                        productGroup = "00";
                    }
                    Console.WriteLine($"Product group: {productGroup}");
                }
                else
                {
                    Console.WriteLine("Invalid barcode");
                }
            }
        }
    }
}
