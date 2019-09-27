using System;

namespace NesE.nes.memory
{
    public class IORegisters : IMemory
    {
        public event Action<byte> OAMWrite;

        public byte this[int i] {
            get { return 0; }
            set {
                if (i == 0x4014)
                {
                    OAMWrite(value);
                }
            }
        }

        public void Set(int index, byte value)
        {
            if (index == 0x4014)
            {
                OAMWrite(value);
            }
        }

        public byte Get(int index)
        {
            return 0;
        }
    }
}
