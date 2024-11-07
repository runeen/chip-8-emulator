using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Windows.Forms;

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
        //Display d;
        Form1 f1;

        static void Main(string[] args)
        {
            //Display d = new Display(f1);

            Application.Run(new Form1());

        }

        private static ushort HexToUint(string input)
        {
            return Convert.ToUInt16(input, 16);
        }

        private void CitesteFisier()
        {
            FileStream stream = new FileStream("IBM.ch8", FileMode.Open, FileAccess.Read);
            Byte[] instructiune = new Byte[2];
            int c = 0;
            while(stream.Read(instructiune, 0, 2) > 0)
            {
                Memory[c] = instructiune[0];
                Memory[c] = (ushort) (Memory[c] << 8);
                Memory[c] = (ushort)(Memory[c] + instructiune[1]);

                Console.WriteLine(Memory[c].ToString("X4"));
                c++;
            }
        }

        public void Emulator(Form1 form)
        {
            registers = new byte[16];
            Memory = new ushort[4096];
            display = new bool[2048];
            stack = new Stack<ushort>();
            PC = 0;
            f1 = form;

            CitesteFisier();

        }

        public void step()
        {

            Execute(Fetch());
        }

        private Instruction Fetch()
        {
            //TODO: metoda asta de citire a instructiunilor este oribila, repara
            Instruction inst = new Instruction(Memory[PC / 2]);
            PC += 2;
            return inst;
        }

        private void Execute(Instruction instruction)
        {
            Console.WriteLine("PC = {0}", PC);
            switch(instruction.tip)
            {
                case 0:
                    if(instruction.nnn == 224)
                    {
                        f1.clearScreen();
                        Console.WriteLine("Sterge ecran");
                    }
                    else if(instruction.nnn == 0)
                    {
                        //gata programu
                        Console.WriteLine("am dat de instrctiune 0000");
                        Console.ReadKey();
                    }
                    break;
                case 1:
                    PC = instruction.nnn;
                    Console.WriteLine("PC: {0}", PC);
                    break;
                case 6:
                    registers[instruction.x] = instruction.nn;
                    Console.WriteLine("Setam un registru");
                    Console.WriteLine("V{0}: {1}", instruction.x, registers[instruction.x]);
                    break;
                case 7:
                    Console.WriteLine("adunam intr-un registru");
                    registers[instruction.x] += instruction.nn;
                    Console.WriteLine("V{0}: {1}", instruction.x, registers[instruction.x]);
                    break;
                case 10:
                    Console.WriteLine("Setam i");
                    I = instruction.nnn;
                    Console.WriteLine("I: {0}", I);
                    break;
                case 13:
                    Console.WriteLine("draw");
                    bool[,] matrice = f1.getMatrix();
                    matrice[registers[instruction.x], registers[instruction.y]] = true;
                    Console.WriteLine("registers[instruction.x] = {0}, registers[instruction.y] = {1}", registers[instruction.x], registers[instruction.y]);

                    f1.setMatrix(matrice);
                    break;
                default:
                    break;
            }
        }

    }
}
