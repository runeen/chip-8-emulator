using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

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
        byte[] Memory;
        byte[] registers;
        byte[] Font;
        //Display d;
        Form1 f1;

        static void Main(string[] args)
        {
            //Display d = new Display(f1);

            Application.Run(new Form1());
        }

        private void SetFont()
        {
            Font = new byte[80]
            {
                0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
                0x20, 0x60, 0x20, 0x20, 0x70, // 1
                0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
                0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
                0x90, 0x90, 0xF0, 0x10, 0x10, // 4
                0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
                0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
                0xF0, 0x10, 0x20, 0x40, 0x40, // 7
                0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
                0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
                0xF0, 0x90, 0xF0, 0x90, 0x90, // A
                0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
                0xF0, 0x80, 0x80, 0x80, 0xF0, // C
                0xE0, 0x90, 0x90, 0x90, 0xE0, // D
                0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
                0xF0, 0x80, 0xF0, 0x80, 0x80  // F
            };

            for(int i = 0; i < 80; i += 2)
            {
                Memory[i] = Font[i];
            }

        }

        private static ushort HexToUint(string input)
        {
            return Convert.ToUInt16(input, 16);
        }

        private void CitesteFisier()
        {
            FileStream stream = new FileStream("test_opcode.ch8", FileMode.Open, FileAccess.Read);
            Byte[] instructiune = new Byte[2];
            int c = 512;
            while(stream.Read(instructiune, 0, 1) > 0)
            {
                Memory[c] = instructiune[0];

                Console.WriteLine(Memory[c].ToString("X2"));
                c++;
            }
        }

        public void Emulator(Form1 form)
        {
            registers = new byte[16];
            Memory = new byte[4096];
            display = new bool[2048];
            stack = new Stack<ushort>();
            SetFont();
            PC = 512;
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
            Instruction inst;
            inst = new Instruction(Memory, PC);
            PC += 2;
            return inst;
        }

        private void Execute(Instruction instruction)
        {
            Console.WriteLine("PC = {0}", PC);
            switch(instruction.tip)
            {
                //TODO: Implemeneaza restul instructiunilor
                case 0:
                    if(instruction.nnn == 224)
                    {
                        f1.clearScreen();
                        Console.WriteLine("Sterge ecran");
                    }
                    else if(instruction.nnn == 0)
                    {
                        //gata programu
                        PC -= 2;
                        Console.WriteLine("am dat de instrctiune 0000");
                        Thread.Sleep(100000);
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
                    BitArray sprite_data;
                    //matrice[registers[instruction.x], registers[instruction.y]] = true;

                    byte y = registers[instruction.y];
                    while (y > 64)
                    {
                        y -= 64;
                    }

                    registers[15] = 0;

                    for(int i = 0; i < instruction.n; i++)
                    {
                        if (y >= 32) break;

                        sprite_data = new BitArray(8);
                        byte sprite_byte = Memory[I + i];

                        int c = 0;
                        while(sprite_byte > 0)
                        {
                            sprite_data[c] = (sprite_byte % 2 > 0);
                            sprite_byte /= 2;
                            c++;
                        }

                        byte x = (byte) (registers[instruction.x] + 8);
                        x = (byte)(x & 63);

                        for (int j = 0; j < 8; j++)
                        {
                            if (x >= 64) break;
                            // Verifica daca bitul mask din sprite data este 1
                            if (sprite_data[j] == true)
                            {
                                if(matrice[x, y])
                                {
                                    matrice[x, y] = false;
                                    registers[15] = 1;
                                }
                                matrice[x, y] = true;
                            }
                            x--;
                        }
                        y++;
                    }



                    f1.setMatrix(matrice);


                    break;
                default:
                    break;
            }
        }

    }
}
