using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip_8
{
    class Instruction
    {
        public byte tip;
        public byte x;
        public byte y;
        public byte n;
        public byte nn;
        public ushort nnn;

        public Instruction(ushort input)
        {

            Console.WriteLine(input);

            //Totul este foarte neoptimizat pentru ca doar incerc sa fac ceva care functioneaza


            //TODO: optimizeaza
            BitArray instructiune = new BitArray(16);

            for(int i = 15; i >= 0; i--)
            {
                instructiune[i] = input % 2 > 0;
                input /= 2;
            }

            for(int i = 0; i < 16; i++)
            {
                if(instructiune[i])
                {
                    Console.Write(1);
                }
                else
                {
                    Console.Write(0);
                }
            }
            Console.WriteLine();

            byte pow = 1;
            for(int i = 3; i >= 0; i--)
            {
                if (instructiune[i]) tip += pow;
                pow *= 2;
            }

            pow = 1;
            for (int i = 7; i >= 4; i--)
            {
                if (instructiune[i]) x += pow;
                pow *= 2;
            }

            pow = 1;
            for (int i = 11; i >= 8; i--)
            {
                if (instructiune[i]) y += pow;
                pow *= 2;
            }

            pow = 1;
            for (int i = 15; i >= 12; i--)
            {
                if (instructiune[i]) n += pow;
                pow *= 2;
            }

            pow = 1;
            for (int i = 15; i >= 8; i--)
            {
                if (instructiune[i]) nn += pow;
                pow *= 2;
            }

            ushort shortpow = 1;
            for (int i = 15; i >= 4; i--)
            {
                if (instructiune[i]) nnn += shortpow;
                shortpow *= 2;
            }
            Console.WriteLine("Avem: Tip:{0}, X:{1}, Y:{2}, N:{3}, NN:{4}, NNN:{5}", tip, x, y, n, nn, nnn);
        }

    }
}
