using System;
using System.Threading;

namespace Task1
{
    class Program
    {
        static int[] memory = new int[1000];
        static int[] tables = new int[10];
        static int cellMemory = 100;
        static Random r = new Random();
        static object o = new object();


        static void Main()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i] = 0;
            }

            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = 0;
            }


            while (true)
            {
                Thread.Sleep(r.Next(3000, 3500));
                Thread thread = new Thread(Tr);
                thread.Start(r.Next(0, 9));
            }
            Console.Read();
        }

        static void Tr(object o)
        {
            int x = Convert.ToInt32(o);
            Alloc(x);
            ShowMemory();
            Thread.Sleep(r.Next(5000, 8000));
            Free(x);
        }

        static void Alloc(int x)
        {
            for (int i = 0; i < tables.Length; i++)
            {
                if (tables[i] == 0)
                {
                    tables[i] = x;

                    for (int j = 0; j < cellMemory; j++)
                    {
                        memory[i * 100 + j] = x;
                    }
                    break;
                }
            }
        }

        static void Free(int x)
        {
            for (int i = 0; i < 10; i++)
            {
                if (tables[i] == x)
                {
                    tables[i] = 0;

                    for (int j = 0; j < cellMemory; j++)
                    {
                        memory[i * 100 + j] = 0;
                    }
                    break;
                }
            }
        }

        static void ShowMemory()
        {
            lock (o)
            {
                Console.Clear();
                for (int i = 0; i < tables.Length; i++)
                {
                    Console.WriteLine("Ячейка номер" + i + Environment.NewLine);
                    for (int j = 0; j < cellMemory; j++)
                    {
                        if (j != 0 && j % 20 == 0) Console.Write(Environment.NewLine);
                        Console.Write(memory[i * 100 + j]);
                    }
                    Console.Write(Environment.NewLine);
                }
            }

        }
    }
}
