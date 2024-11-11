using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace chip_8
{
    class Program
    {
        public ushort PC;
        public ushort I;
        public byte delayTimer;
        public byte soundTimer;
        public Stack<ushort> stack;
        public bool[] display;
        public byte[] Memory;
        public byte[] registers;
        public byte[] Font;
        public Fereastra f1;
        public Instruction lastRun;
        public Random r;

        static void Main(string[] args)
        {

            Application.Run(new Fereastra());
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

            for (int i = 0; i < 80; i++)
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
            FileStream stream = new FileStream("TETRIS", FileMode.Open, FileAccess.Read);
            Byte[] instructiune = new Byte[2];
            int c = 512;
            while (stream.Read(instructiune, 0, 1) > 0)
            {
                Memory[c] = instructiune[0];

                Console.WriteLine(Memory[c].ToString("X2"));
                c++;
            }
        }

        public void Emulator(Fereastra form)
        {
            registers = new byte[16];
            Memory = new byte[4096];
            display = new bool[2048];
            stack = new Stack<ushort>();
            SetFont();
            PC = 512;
            f1 = form;
            r = new Random();
            delayTimer = 1;

            CitesteFisier();

        }

        public bool step(bool isKeyDown, Keys Key_pressed)
        {
            byte key_code = translate(Key_pressed);
            return Execute(Fetch(), isKeyDown, key_code);
        }

        private byte translate(Keys key)
        {
            if (key == Keys.D1)
                return 0x1;
            else if (key == Keys.D2)
                return 0x2;
            else if (key == Keys.D3)
                return 0x3;
            else if (key == Keys.D4)
                return 0x4;
            else if (key == Keys.Q)
                return 0x5;
            else if (key == Keys.W)
                return 0x6;
            else if (key == Keys.R)
                return 0x7;
            else if (key == Keys.A)
                return 0x8;
            else if (key == Keys.S)
                return 0x9;
            else if (key == Keys.D)
                return 0xA;
            else if (key == Keys.F)
                return 0xB;
            else if (key == Keys.Z)
                return 0xC;
            else if (key == Keys.X)
                return 0xD;
            else if (key == Keys.C)
                return 0xE;
            else if (key == Keys.V)
                return 0xF;
            return 0;
        }

        private Instruction Fetch()
        {
            Instruction inst;
            inst = new Instruction(Memory, PC);
            PC += 2;
            return inst;
        }

        private bool Execute(Instruction instruction, bool isKeyDown, byte Key_pressed)
        {
            //TODO: probabil e problema cu imput-uri idk
            bool refresh = false;
            if (isKeyDown)
            {
                Console.WriteLine("este apasata tasta {0}", Key_pressed);
            }
            Console.WriteLine("PC = {0}", PC);
            switch (instruction.tip)
            {
                case 0x0:
                    if (instruction.nnn == 224)
                    {
                        f1.clearScreen();
                        Console.WriteLine("Sterge ecran");
                    }
                    else if (instruction.x == 0 && instruction.y == 12)
                    {
                        bool[,] matrix = f1.getMatrix();
                        bool[,] matrix_output = new bool[64, 32];
                        for (int i = instruction.n; i < 32; i++)
                        {
                            for (int j = 0; j < 64; j++)
                            {
                                matrix_output[j, i] = matrix[j, i - instruction.n];
                            }
                        }
                        f1.setMatrix(matrix_output);
                    }
                    else if (instruction.nn == 0xEE)
                    {
                        PC = stack.Pop();
                    }
                    else
                    {
                        Console.WriteLine("AM DAT DE INSTRUCTIUNE NECUNOSCUTA HELP ---------------------------------------------");
                    }

                    break;
                case 0x1:
                    PC = instruction.nnn;
                    Console.WriteLine("PC: {0}", PC);
                    break;
                case 0x2:
                    stack.Push(PC);
                    PC = instruction.nnn;
                    Console.WriteLine("Sarim la subrutina, pc:{0}", PC);
                    break;
                case 0x3:
                    Console.WriteLine("Verificam daca registru{0} este egal cu val {1}", instruction.x, instruction.nn);
                    if (registers[instruction.x] == instruction.nn)
                    {
                        PC += 2;
                    }
                    break;
                case 0x4:
                    Console.WriteLine("Verificam daca registru{0} nu este egal cu val {1}", instruction.x, instruction.nn);
                    if (registers[instruction.x] != instruction.nn)
                    {
                        PC += 2;
                    }
                    break;
                case 0x5:
                    Console.WriteLine("Verificam daca registru{0} este egal cu registrul {1}", instruction.x, instruction.y);
                    if (registers[instruction.x] == registers[instruction.y])
                    {
                        PC += 2;
                    }
                    break;
                case 0x6:
                    registers[instruction.x] = instruction.nn;
                    Console.WriteLine("Setam un registru");
                    Console.WriteLine("V{0}: {1}", instruction.x, registers[instruction.x]);
                    break;
                case 0x7:
                    Console.WriteLine("adunam intr-un registru");
                    if (registers[instruction.x] + instruction.nn > 0xFF)
                    {
                        registers[instruction.x] = (byte)(instruction.nn + registers[instruction.x] - 0x100);
                        registers[0xF] = 1;
                    }
                    else
                    {
                        registers[instruction.x] += instruction.nn;
                        registers[0xF] = 0;
                    }
                    Console.WriteLine("V{0}: {1}", instruction.x, registers[instruction.x]);
                    break;
                case 0x8:
                    if (instruction.n == 0)
                    {
                        Console.WriteLine("Mutam or intre v{0} si v{1}", instruction.x, instruction.y);
                        registers[instruction.x] = registers[instruction.y];
                    }
                    else if (instruction.n == 1)
                    {
                        Console.WriteLine("Facem or intre v{0} si v{1}", instruction.x, instruction.y);
                        Console.WriteLine("x:{0}, y:{1}, or = {2}", registers[instruction.x], registers[instruction.y], registers[instruction.x] | registers[instruction.y]);
                        registers[instruction.x] = (byte)(registers[instruction.x] | registers[instruction.y]);
                    }
                    else if (instruction.n == 2)
                    {
                        Console.WriteLine("Facem and intre v{0} si v{1}", instruction.x, instruction.y);
                        /*
                        byte temp = 0;
                        byte copy_x = registers[instruction.x];
                        byte copy_y = registers[instruction.y];
                        for (int i = 0; i < 8; i++)
                        {
                            temp *= 2;
                            if (copy_x % 2 == 1 && copy_y % 2 == 1) temp++;
                            copy_x /= 2;
                            copy_y /= 2;
                        }
                        registers[instruction.x] = temp;
                        */
                        registers[instruction.x] = (byte)(registers[instruction.x] & registers[instruction.y]); 
                    }
                    else if (instruction.n == 3)
                    {
                        Console.WriteLine("Facem xor intre v{0} si v{1}", instruction.x, instruction.y);


                        registers[instruction.x] = (byte)(registers[instruction.x] ^ registers[instruction.y]);
                        Console.WriteLine(registers[instruction.x]);
                    }
                    else if (instruction.n == 4)
                    {
                        Console.WriteLine("Facem suma intre v{0} si v{1}", instruction.x, instruction.y);
                        if (registers[instruction.x] + registers[instruction.y] > 0x100)
                        {
                            registers[instruction.x] = (byte)(registers[instruction.y] + registers[instruction.x] - 0x100);
                            registers[0xF] = 1;
                        }
                        else
                        {
                            registers[instruction.x] += registers[instruction.y];
                            registers[0xF] = 0;
                        }
                    }
                    else if (instruction.n == 5)
                    {
                        //Gen ce ar trb sa se intample aici nici nu stiu
                        Console.WriteLine("Facem scadere intre v{0} si v{1}", instruction.x, instruction.y);
                        if (registers[instruction.x] < registers[instruction.y])
                        {
                            registers[15] = 0;
                            registers[instruction.x] = (byte)((registers[instruction.x] + 0x100) - registers[instruction.y]);
                        }
                        else
                        {
                            registers[15] = 1;
                            registers[instruction.x] = (byte)(registers[instruction.x] - registers[instruction.y]);
                        }
                    }
                    else if (instruction.n == 6)
                    {
                        if (registers[instruction.x] % 2 == 1) registers[0xF] = 1;
                        else registers[0xF] = 0;
                        registers[instruction.x] /= 2;
                    }
                    else if (instruction.n == 7)
                    {
                        Console.WriteLine("Facem scadere intre v{0} si v{1}", instruction.x, instruction.y);
                        if (registers[instruction.x] < registers[instruction.y])
                        {
                            registers[15] = 1;
                            registers[instruction.x] = (byte)((registers[instruction.x] + 256) - registers[instruction.y]);
                        }
                        else
                        {
                            registers[15] = 0;
                            registers[instruction.x] = (byte)(registers[instruction.x] - registers[instruction.y]);
                        }
                    }
                    else if (instruction.n == 0xE)
                    {
                        byte temp = registers[instruction.x];

                        for (int i = 0; i < 7; i++)
                        {
                            temp /= 2;
                        }

                        registers[0xF] = temp;

                        temp = registers[instruction.x];

                        registers[instruction.x] = (byte)(temp * 2);
                    }
                    else
                    {
                        Console.WriteLine("AM DAT DE INSTRUCTIUNE NECUNOSCUTA HELP ---------------------------------------------");
                    }
                    break;

                case 0x9:
                    if (registers[instruction.x] != registers[instruction.y]) PC += 2;
                    break;

                case 0xA:
                    Console.WriteLine("Setam i");
                    I = instruction.nnn;
                    Console.WriteLine("I: {0}", I);
                    break;

                case 0xB:
                    PC = (ushort)(instruction.nnn + registers[0]);
                    break;

                case 0xC:
                    byte[] randomByte = new Byte[1];
                    r.NextBytes(randomByte);

                    byte temp1 = 0;
                    byte tempInstNN = instruction.nn;
                    for (int i = 0; i < 8; i++)
                    {
                        temp1 *= 2;
                        if (randomByte[0] % 2 == 1 && tempInstNN % 2 == 1) temp1++;
                        randomByte[0] /= 2;
                        tempInstNN /= 2;
                    }
                    registers[instruction.x] = temp1;
                    break;

                case 0xD:

                    Console.WriteLine("draw");
                    bool[,] matrice = f1.getMatrix();

                    byte[] sprite_data = new byte[instruction.n];

                    for(int i = 0; i < instruction.n; i++)
                    {
                        sprite_data[i] = Memory[I + i];
                    }

                    registers[0xF] = 0;

                    byte mask;
                    int c;
                    for(int i = 0; i < instruction.n; i++)
                    {
                        mask = 0x80;
                        c = 0;
                        while(mask != 0)
                        {
                            if ((sprite_data[i] & mask) == mask)
                            {
                                if(matrice[(registers[instruction.x] + c) % 64, (registers[instruction.y] + i) % 32] == true)
                                {
                                    matrice[(registers[instruction.x] + c) % 64, (registers[instruction.y] + i) % 32] = false;
                                    registers[0xF] = 1;
                                }
                                else
                                {
                                    matrice[(registers[instruction.x] + c) % 64, (registers[instruction.y] + i) % 32] = true;
                                }
                            }
                            c++;
                            mask /= 2;
                        }
                    }

                    refresh = true;

                    f1.setMatrix(matrice);



                    break;
                case 0xE:
                    if (instruction.nn == 0x9e)
                    {
                        Console.WriteLine("sarim daca e apasat ce scrie in v{0}", instruction.x);
                        Console.WriteLine("{0}, {1}", isKeyDown, Key_pressed);
                        if (isKeyDown && Key_pressed == registers[instruction.x]) PC += 2;
                    }
                    else if (instruction.nn == 0xA1)
                    {
                        Console.WriteLine("sarim daca nu e apasat ce scrie in v{0}", instruction.x);
                        Console.WriteLine("{0}, {1}", isKeyDown, Key_pressed);
                        if ((!isKeyDown) || (isKeyDown && Key_pressed != registers[instruction.x])) PC += 2;
                    }
                    else
                    {
                        Console.WriteLine("AM DAT DE INSTRUCTIUNE NECUNOSCUTA HELP ---------------------------------------------");
                    }
                    break;

                case 0xF:
                    if (instruction.nn == 0x07)
                    {
                        Console.WriteLine("setam valoarea la reg{0} egala cu d timer", instruction.x);
                        registers[instruction.x] = delayTimer;
                    }
                    else if (instruction.nn == 0x0A)
                    {
                        Console.WriteLine("{0}, {1}", isKeyDown, Key_pressed);
                        Console.WriteLine("Asteptam keypress");
                        if (isKeyDown) registers[instruction.x] = Key_pressed;
                        else PC -= 2;
                    }
                    else if (instruction.nn == 0x15)
                    {
                        Console.WriteLine("setam valoarea la delay timer egala cu reg {0}", instruction.x);
                        delayTimer = registers[instruction.x];
                    }
                    else if (instruction.nn == 0x18)
                    {
                        Console.WriteLine("setam sound timer egal cu reg {0}", instruction.x);
                        soundTimer = registers[instruction.x];
                    }
                    else if (instruction.nn == 0x1E)
                    {
                        Console.WriteLine("Adunam reg {0} in I", instruction.x);
                        I += registers[instruction.x];
                    }
                    else if (instruction.nn == 0x29)
                    {
                        Console.WriteLine("Scriem in I locatia sprite-ului in font cu val reg{0} ", instruction.x);
                        I = (ushort)(5 * registers[instruction.x]);
                    }
                    else if (instruction.nn == 0x33)
                    {
                        byte temp = registers[instruction.x];

                        Memory[I + 2] = (byte)(temp % 10);
                        temp /= 10;
                        Memory[I + 1] = (byte)(temp % 10);
                        temp /= 10;
                        Memory[I] = (byte)(temp % 10);
                        temp /= 10;
                    }
                    else if (instruction.nn == 0x55)
                    {
                        Console.WriteLine("Stockam memorie in registrii");
                        for (int i = 0; i <= instruction.x; i++)
                        {
                            Memory[I + i] = registers[i];
                        }
                    }
                    else if (instruction.nn == 0x65)
                    {
                        Console.WriteLine("Stockam registrii in memorie");
                        for (int i = 0; i <= instruction.x; i++)
                        {
                            registers[i] = Memory[I + i];
                        }
                    }
                    else
                    {
                        Console.WriteLine("AM DAT DE INSTRUCTIUNE NECUNOSCUTA HELP ---------------------------------------------");
                    }
                    break;
                default:
                    Console.WriteLine("AM DAT DE INSTRUCTIUNE NECUNOSCUTA HELP ---------------------------------------------");
                    break;
            }

            if(delayTimer != 0) delayTimer--;
            if(soundTimer != 0) soundTimer--;
            lastRun = instruction;
            return refresh;
        }

    }
}
