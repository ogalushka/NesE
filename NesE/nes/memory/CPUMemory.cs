using System.Collections.Generic;

namespace NesE.nes.memory
{
    public class CPUMemory : Memory
    {
        const int bankAddresShift = 13;

        public CPUMemory() : base(bankAddresShift)
        {
            var inernalRam = new byte[0x800];
            var ppuRegisters = new byte[0x8];
            var apuRegisters = new byte[0x20];
            var dummySpace = new byte[0x2000];
            AddAddressSpace(0, 0b0000_0111_1111_1111, inernalRam);
            AddAddressSpace(1, 0b0000_0000_0000_0111, ppuRegisters);
            AddAddressSpace(2, 0b0000_0000_0000_0111, apuRegisters);
            AddAddressSpace(3, 0x1FFF, dummySpace);
            AddAddressSpace(4, 0x1FFF, dummySpace);
            AddAddressSpace(5, 0x1FFF, dummySpace);
            AddAddressSpace(6, 0x1FFF, dummySpace);
            AddAddressSpace(7, 0x1FFF, dummySpace);
        }
    }
}
