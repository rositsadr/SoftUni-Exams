using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] tasks = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            int[] threads = Console.ReadLine()
                .Split(' ',StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int taskToKill = int.Parse(Console.ReadLine());

            Queue<int> queueOfThreads = new Queue<int>(threads);
            Stack<int> stackOfTasks = new Stack<int>(tasks);

            while (true)
            {
                int currantTask = stackOfTasks.Pop();
                int currantThread = queueOfThreads.Dequeue();

                if (currantTask == taskToKill)
                {
                    Console.WriteLine($"Thread with value {currantThread} killed task {currantTask}");
                    Console.WriteLine($"{currantThread} {string.Join(' ',queueOfThreads)}");
                    break;
                }

                if (currantThread < currantTask)
                {
                    stackOfTasks.Push(currantTask);
                }


            }

        }
    }
}
