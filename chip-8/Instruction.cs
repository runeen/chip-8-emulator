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
        byte tip;
        byte x;
        byte y;
        byte n;
        byte nn;
        ushort nnn;

        public Instruction(ushort input)
        {

            //TODO: transforma instructiuni in bucati tip, x, y, n, nn, nnn
            BitArray instructiune = new BitArray(16);




            byte pow = 1;
            for(int i = 0; i < 8; i++)
            {
                Console.WriteLine("instructiune[{0}] = {1}", i, instructiune[i]);
                if (instructiune[i]) tip += pow;
                pow *= 2;
            }

            pow = 1;
            for (int i = 8; i < 16; i++)
            {
                if (instructiune[i]) x += pow;
                pow *= 2;
            }

            pow = 1;
            for (int i = 16; i < 24; i++)
            {
                if (instructiune[i]) y += pow;
                pow *= 2;
            }

            pow = 1;
            for (int i = 24; i < 32; i++)
            {
                if (instructiune[i]) n += pow;
                pow *= 2;
            }


            Console.WriteLine("Avem: {0}, {1}, {2}, {3}", tip, x, y, n);
            Console.ReadKey();
        }

    }
}
