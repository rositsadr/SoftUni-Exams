using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> shopingList = Console.ReadLine().Split('!').ToList();
            string input = Console.ReadLine();

            while(input!= "Go Shopping!")
            {
                string[] command = input.Split();
                if (command[0] == "Urgent")
                {
                    if (!shopingList.Contains(command[1]))
                    {
                        shopingList.Insert(0, command[1]);
                    }
                }
                else if (command[0] == "Unnecessary")
                {
                    if (shopingList.Contains(command[1]))
                    {
                        shopingList.Remove(command[1]);
                    }
                }
                else if(command[0]=="Correct")
                {
                    if (shopingList.Contains(command[1]))
                    {
                        int index = shopingList.IndexOf(command[1]);
                        shopingList[index] = command[2];
                    }
                }
                else if(command [0] == "Rearrange")
                {
                    if (shopingList.Contains(command[1]))
                    {
                        shopingList.Remove(command[1]);
                        shopingList.Add(command[1]);
                    }
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(string.Join(", ",shopingList));
        }
    }
}
