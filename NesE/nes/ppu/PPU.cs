using NesE.nes.memory;

namespace NesE.nes.ppu
{
    public class PPU
    {
        public IMemory Memory;
        public PPURegisters Registers;
        public byte[] OAM;

        public PPU(Memory ppuMem, PPURegisters registers)
        {
            Memory = ppuMem;
            Registers = registers;
            OAM = new byte[0x100];
        }

        public void Step()
        {
        }
    }
}
