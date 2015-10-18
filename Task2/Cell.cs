using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Cell
    {
        public int Value { get; set; }
        public int Size { get; set; }
        public int StartIndex { get; set; }

        public Cell(int value, int size)
        {
            Value = value;
            Size = size;
        }
    }
}
