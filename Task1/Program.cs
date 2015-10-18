using System;
using System.Threading;

namespace Task1
{
    class Program
    {
        static int[] memory = new int[1000];
        static int[] tables = new int[10];
        static int cellMemory = 100;
        static Random random = new Random();
        static object lockObject = new object();


        static void Main()
        {
            while (true)
            {
                Thread.Sleep(random.Next(100, 500));
                Thread thread = new Thread(Tr);
                thread.Start(random.Next(1, 9));
            }
        }

        static void Tr(object value)
        {
            int valueConvetred = Convert.ToInt32(value);
            
            Alloc(valueConvetred, 0);

            ShowMemory();
            Thread.Sleep(random.Next(1500,2000));

            Alloc(0, valueConvetred);
        }

        static void Alloc(int value, int checkValue)
        {
            for (int i = 0; i < tables.Length; i++)
            {
                if (tables[i] == checkValue)
                {
                    tables[i] = value;

                    for (int j = 0; j < cellMemory; j++)
                    {
                        memory[i * 100 + j] = value;
                    }
                    break;
                }
            }
        }

        static void ShowMemory()
        {
            lock (lockObject)
            {
                Console.Clear();
                foreach (int t in memory)
                {
                    Console.Write(t);
                }
            }

        }
    }
}
