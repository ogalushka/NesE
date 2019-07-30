using NesE.nes.cpu;

namespace Tests.nes
{
    public static class MemoryGetter
    {
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
    }
}
