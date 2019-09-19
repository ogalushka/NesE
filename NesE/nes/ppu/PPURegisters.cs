namespace NesE.nes.ppu
{
    public class PPURegisters
    {
        public const int PPUCTRL = 0;
        public const int MASK = 1;
        public const int STATUS = 2;
        public const int OAMADDR = 3;
        public const int OAMDATA = 4;
        public const int SCROLL = 5;
        public const int ADDR = 6;
        public const int DATA = 7;

        private readonly byte[] _ppuRegisterBytes;

        public PPURegisters(byte[] ppuRegisters)
        {
            _ppuRegisterBytes = ppuRegisters;
        }

        public byte Get(int registerIndex)
        {
            return _ppuRegisterBytes[registerIndex];
        }
    }
}
