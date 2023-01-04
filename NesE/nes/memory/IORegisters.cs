using System;

namespace NesE.nes.memory
{
    public class IORegisters : IMemory
    {
        private byte[] _oam;
        private IMemory _ram;

        public IORegisters(byte[] oam, IMemory ram)
        {
            _oam = oam;
            _ram = ram;
        }

#if TEST
        public byte this[int index] {
            get { return Get(index); }
            set {
                Set(index, value);
            }
        }
#endif

        public void Set(int index, byte value)
        {
            if (index == 0x4014)
            {
                WriteOAM(value);
            }
        }

        public byte Get(int index)
        {
            return 0;
        }

        private void WriteOAM(byte address)
        {
            int startAdddress = address << 8;
            var endAddresss = startAdddress + 0x100;
            for (var cpuI = startAdddress; cpuI < endAddresss; cpuI++)
            {
                var oamI = cpuI - startAdddress;
                _oam[oamI] = _ram.Get(cpuI);
            }
        }
    }
}
