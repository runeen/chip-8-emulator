using System;
using System.Text;

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
            nnn = (ushort)((ushort)nn + 256 * x);

            Console.WriteLine("Instructiune: {0}", this.ToString());
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Tip:");
            sb.Append(tip.ToString("X1"));
            sb.Append(" X:");
            sb.Append(x.ToString("X1"));
            sb.Append(" Y:");
            sb.Append(y.ToString("X1"));
            sb.Append(" n:");
            sb.Append(n.ToString("X1"));
            sb.Append(" nn:");
            sb.Append(nn.ToString("X2"));
            sb.Append(" nnn:");
            sb.Append(nn.ToString("X3"));

            sb.Append(" (");
            sb.Append(tip.ToString("X1"));
            sb.Append(x.ToString("X1"));
            sb.Append(y.ToString("X1"));
            sb.Append(n.ToString("X1"));
            sb.Append(")");

            return sb.ToString();
        }

    }
}
