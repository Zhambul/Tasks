using System;
using System.Collections.Generic;
using System.Threading;

namespace Task3
{
    class Program
    {
        private static int[] memory = new int[1000];
        private static Random random = new Random();
        private static object lockObject = new object();

        private static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(random.Next(500, 1000));

                Cell cell = new Cell(random.Next(1, 10),random.Next(10, 200));
               
                Thread thread = new Thread(delegate()
                {
                    Tr(cell);
                });
                thread.Start();
            }
        }

        private static void Tr(Cell cell)
        {
            Alloc(cell);

            ShowMemory();
            Thread.Sleep(2000);

            Free(cell);
        }

        private static void Free(Cell cell)
        {
            for (int j = 0; j < cell.Size; j++)
            {
                memory[cell.StartIndex + j] = 0;
            }

            ReShuffle();
        }

        private static void ReShuffle()
        {
            for (int index = 0; index < memory.Length; index++)
            {
                if (memory[index] != 0)
                {
                    int pointer = index;

                    while (pointer > 0 && memory[pointer] != 0)
                    {
                        int temp = memory[pointer - 1];
                        memory[pointer - 1] = memory[pointer];
                        memory[pointer] = temp;

                        pointer = pointer - 1;
                    }
                }
            }
        }

        private static void Alloc(Cell cell)
        {
            for (int index = 0; index < memory.Length; index++)
            {
                if (memory.Length < (index + cell.Size))
                {
                    break;
                }

                if (memory[index] == 0)
                {
                    if (IsEnoughtSpace(cell, index))
                    {
                        FillSector(cell, index);
                        break;
                    }
                }
            }
        }

        private static void FillSector(Cell cell, int index)
        {
            cell.StartIndex = index;

            for (int j = 0; j < cell.Size; j++)
            {
                memory[index + j] = cell.Value;
            }
        }

        private static bool IsEnoughtSpace(Cell cell, int startIndex)
        {
            bool isEnought = true;

            for (int sizeIndex = 0; sizeIndex < cell.Size; sizeIndex++)
            {
                if (memory[startIndex + sizeIndex] != 0)
                {
                    isEnought = false;
                }
            }

            return isEnought;
        }

        private static void ShowMemory()
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
