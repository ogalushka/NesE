using NesE.nes.cpu;

namespace Tests.nes
{
    public static class MemoryGetter
    {
        public static byte Status(CPU cpu)
        {
            return (byte)cpu.P;
        }

        public static byte Accumulator(CPU cpu)
        {
            return cpu.A;
        }

        public static byte X(CPU cpu)
        {
            return cpu.X;
        }

        public static byte Y(CPU cpu)
        {
            return cpu.Y;
        }

        public static byte StackPointer(CPU cpu)
        {
            return cpu.S;
        }

        public static byte ZeroPage(CPU cpu)
        {
            var address = cpu.RAM[1];
            return cpu.RAM[address];
        }

        public static byte ZeroPageX(CPU cpu)
        {
            byte address = cpu.RAM[1];
            return cpu.RAM[address + cpu.X];
        }

        public static byte ZeroPageY(CPU cpu)
        {
            byte address = cpu.RAM[1];
            return cpu.RAM[address + cpu.Y];
        }

        public static byte Absolute(CPU cpu)
        {
            byte addressLow = cpu.RAM[1];
            byte addressHigh = cpu.RAM[2];
            var address = (addressHigh << 8) | addressLow;
            return cpu.RAM[address];
        }

        public static byte AbsoluteX(CPU cpu)
        {
            byte addressLow = cpu.RAM[1];
            byte addressHigh = cpu.RAM[2];
            var address = (addressHigh << 8) | addressLow;
            address += cpu.X;
            return cpu.RAM[address];
        }

        public static byte AbsoluteY(CPU cpu)
        {
            byte addressLow = cpu.RAM[1];
            byte addressHigh = cpu.RAM[2];
            var address = (addressHigh << 8) | addressLow;
            address += cpu.Y;
            return cpu.RAM[address];
        }

        public static byte IndirectX(CPU cpu)
        {
            byte address = cpu.RAM[1];
            byte addressLow = (byte)(address + cpu.X);
            byte addressHigh = (byte)(addressLow + 1);
            var targetLow = cpu.RAM[addressLow];
            var targetHigh = cpu.RAM[addressHigh];
            var target = (targetHigh << 8) | targetLow;
            return cpu.RAM[target];
        }

        public static byte IndirectY(CPU cpu)
        {
            var addressLow = cpu.RAM[1];
            var targetLow = cpu.RAM[addressLow] + cpu.Y;
            var targetHigh = cpu.RAM[addressLow + 1];
            var target = (targetHigh << 8) | targetLow;
            return cpu.RAM[target];
        }
    }
}
