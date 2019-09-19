using NesE.nes.cpu;

namespace Tests.nes
{
    public static class MemorySetter
    {
        public static void Accumulator(byte value, CPU cpu)
        {
            cpu.A = value;
        }

        public static void XRegister(byte value, CPU cpu)
        {
            cpu.X = value;
        }

        public static void YRegister(byte value, CPU cpu)
        {
            cpu.Y = value;
        }

        public static void StatusReg(byte value, CPU cpu)
        {
            cpu.P = (PFlag)value;
        }

        public static void StackPointer(byte value, CPU cpu)
        {
            cpu.S = value;
        }

        public static void Immediate(byte value, CPU cpu)
        {
            cpu.Ram[1] = value;
        }

        public static void ZeroPage(byte value, CPU cpu)
        {
            byte address = 0x50;
            cpu.Ram[1] = address;
            cpu.Ram[address] = value;
        }

        public static void ZeroPageX(byte value, CPU cpu)
        {
            byte address = 0x50;
            cpu.Ram[1] = address;
            cpu.X = 0x01;
            cpu.Ram[address + cpu.X] = value;
        }

        public static void ZeroPageY(byte value, CPU cpu)
        {
            byte address = 0x50;
            cpu.Ram[1] = address;
            cpu.Y = 0x01;
            cpu.Ram[address + cpu.Y] = value;
        }

        public static void Absolute(byte value, CPU cpu)
        {
            byte addressLow = 0x50;
            byte addressHigh = 0x40;
            ushort address = 0x4050;

            cpu.Ram[1] = addressLow;
            cpu.Ram[2] = addressHigh;
            cpu.Ram[address] = value;
        }

        public static void AbsoluteX(byte value, CPU cpu)
        {
            byte addressLow = 0x50;
            byte addressHigh = 0x40;
            cpu.X = 1;
            ushort address = 0x4051;

            cpu.Ram[1] = addressLow;
            cpu.Ram[2] = addressHigh;
            cpu.Ram[address] = value;
        }

        public static void AbsoluteY(byte value, CPU cpu)
        {
            byte addressLow = 0x50;
            byte addressHigh = 0x40;
            cpu.Y = 1;
            ushort address = 0x4051;

            cpu.Ram[1] = addressLow;
            cpu.Ram[2] = addressHigh;
            cpu.Ram[address] = value;
        }

        public static void ImmidiateTwoByte(ushort value, CPU cpu)
        {
            cpu.Ram[1] = (byte)value;
            cpu.Ram[2] = (byte)(value >> 8);
        }

        public static void Indirect(ushort value, CPU cpu)
        {
            byte addressLow = 0x50;
            byte addressHigh = 0x40;
            ushort address = 0x4050;

            cpu.Ram[1] = addressLow;
            cpu.Ram[2] = addressHigh;

            cpu.Ram[address] = (byte)value;
            cpu.Ram[address + 1] = (byte)(value >> 8);
        }

        public static void IndirectX(byte value, CPU cpu)
        {
            byte address = 0x50;
            cpu.Ram[1] = address;
            cpu.X = 1;
            byte addressLow = (byte)(address + cpu.X);
            byte addressHigh = (byte)(addressLow + 1);
            cpu.Ram[addressLow] = 0x64;
            cpu.Ram[addressHigh] = 0x32;
            cpu.Ram[0x3264] = value;
        }

        public static void IndirectY(byte value, CPU cpu)
        {
            byte addressLow = 0x55;
            byte addressHigh = 0x56;
            cpu.Ram[1] = addressLow;
            cpu.Ram[addressLow] = 0x30;
            cpu.Ram[addressHigh] = 0x12;
            cpu.Y = 4;
            cpu.Ram[0x1234] = value;
        }
    }
}
