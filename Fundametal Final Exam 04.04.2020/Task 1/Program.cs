using System;
using System.Text;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            string[] input = Console.ReadLine().Split();
            StringBuilder newPassword = new StringBuilder();

            while (input[0] != "Done")
            {
                string command = input[0];
                if (command == "TakeOdd")
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (i%2 !=0)
                        {
                            newPassword.Append(text[i]);
                        }
                    }
                    text = newPassword.ToString();
                    Console.WriteLine(text);
                }
                else if (command == "Cut")
                {
                    int index = int.Parse(input[1]);
                    int lenght = int.Parse(input[2]);
                    text = text.Remove(index, lenght);
                    Console.WriteLine(text);
                }
                else if (command == "Substitute")
                {
                    string substring = input[1];
                    string substitute = input[2];
                    if (text.Contains(substring))
                    {
                        text = text.Replace(substring, substitute);
                        Console.WriteLine(text);
                    }
                    else
                    {
                        Console.WriteLine("Nothing to replace!");
                    }
                }              
                input = Console.ReadLine().Split();
            }
            Console.WriteLine($"Your password is: {text}");
        }
    }
}
