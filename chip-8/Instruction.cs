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

        public Instruction(byte[] memory, ushort PC)
        {

            
            byte[] instructiune = new byte[2];

            instructiune[0] = memory[PC];
            instructiune[1] = memory[PC + 1];


            tip = (byte)(instructiune[0] / 16);
            x = (byte)((byte)(instructiune[0] * 16) / 16);

            y = (byte)(instructiune[1] / 16);
            n = (byte)((byte)(instructiune[1] * 16) / 16);

            nn = instructiune[1];
            nnn = (ushort)((ushort) nn + 256 * x);

            Console.WriteLine("Avem: Tip:{0}, X:{1}, Y:{2}, N:{3}, NN:{4}, NNN:{5}", tip, x, y, n, nn, nnn);
        }

    }
}
