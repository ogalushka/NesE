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
            var address = cpu.Ram[1];
            return cpu.Ram[address];
        }

        public static byte ZeroPageX(CPU cpu)
        {
            byte address = cpu.Ram[1];
            return cpu.Ram[address + cpu.X];
        }

        public static byte ZeroPageY(CPU cpu)
        {
            byte address = cpu.Ram[1];
            return cpu.Ram[address + cpu.Y];
        }

        public static byte Absolute(CPU cpu)
        {
            byte addressLow = cpu.Ram[1];
            byte addressHigh = cpu.Ram[2];
            var address = (addressHigh << 8) | addressLow;
            return cpu.Ram[address];
        }

        public static byte AbsoluteX(CPU cpu)
        {
            byte addressLow = cpu.Ram[1];
            byte addressHigh = cpu.Ram[2];
            var address = (addressHigh << 8) | addressLow;
            address += cpu.X;
            return cpu.Ram[address];
        }

        public static byte AbsoluteY(CPU cpu)
        {
            byte addressLow = cpu.Ram[1];
            byte addressHigh = cpu.Ram[2];
            var address = (addressHigh << 8) | addressLow;
            address += cpu.Y;
            return cpu.Ram[address];
        }

        public static byte IndirectX(CPU cpu)
        {
            byte address = cpu.Ram[1];
            byte addressLow = (byte)(address + cpu.X);
            byte addressHigh = (byte)(addressLow + 1);
            var targetLow = cpu.Ram[addressLow];
            var targetHigh = cpu.Ram[addressHigh];
            var target = (targetHigh << 8) | targetLow;
            return cpu.Ram[target];
        }

        public static byte IndirectY(CPU cpu)
        {
            var addressLow = cpu.Ram[1];
            var targetLow = cpu.Ram[addressLow] + cpu.Y;
            var targetHigh = cpu.Ram[addressLow + 1];
            var target = (targetHigh << 8) | targetLow;
            return cpu.Ram[target];
        }
    }
}
