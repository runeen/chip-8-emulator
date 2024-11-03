using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip_8
{
    class Program
    {
        ushort PC;
        ushort I;
        byte delayTimer;
        byte soundTimer;
        Stack<ushort> stack;
        bool[] display;
        ushort[] memory;
        byte[] registers;

        static void Main(string[] args)
        {
            Instruction test = new Instruction(12345);
        }


        private void Emulator()
        {
            registers = new byte[16];
            memory = new ushort[4096];
            display = new bool[2048];
            stack = new Stack<ushort>();

            while (true)
            {
                Decode(Fetch());
                //Execute();
            }
        }

        private Instruction Fetch()
        {
            // Read instruction

            PC += 2;
            return new Instruction(1234);
        }

        private void Decode(Instruction instruction)
        {
            switch (instruction)
            {
                default:
                    break;
            }
        }

    }
}
