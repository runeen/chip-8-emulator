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
        ushort[] Memory;
        byte[] registers;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Emulator();
        }

        private static ushort HexToUint(string input)
        {
            return Convert.ToUInt16(input, 16);
        }

        private void Emulator()
        {
            registers = new byte[16];
            Memory = new ushort[4096];
            display = new bool[2048];
            stack = new Stack<ushort>();
            PC = 0;

            //TODO: sa citim dintr-un fisier programul
            Memory[0] = 224;
            Memory[1] = 41514;
            Memory[2] = 24588;
            Memory[3] = 24840;
            Memory[4] = 53279;
            Memory[5] = 28681;
            Memory[6] = 41529;
            Memory[7] = 53279;
            Memory[8] = 4096;

            while (true)
            {
                Execute(Fetch());
                Console.ReadKey();
            }
        }

        private Instruction Fetch()
        {
            //TODO: metoda asta de citire a instructiunilor este oribila
            Instruction inst = new Instruction(Memory[PC / 2]);
            PC += 2;
            return inst;
        }

        private void Execute(Instruction instruction)
        {
            switch(instruction.tip)
            {
                case 0:
                    if(instruction.nnn == 224)
                    {
                        // clear screen
                        Console.WriteLine("Sterge ecran");
                    }
                    break;
                case 1:
                    PC = instruction.nnn;
                    break;
                case 6:
                    registers[instruction.x] = instruction.nn;
                    break;
                case 7:
                    registers[instruction.x] += instruction.nn;
                    break;
                case 10:
                    I = instruction.nnn;
                    break;
                case 13:
                    Console.WriteLine("deseneaza ceva");
                    break;
                default:
                    break;
            }
        }

    }
}
