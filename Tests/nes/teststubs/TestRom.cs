using NesE.nes.rom;

namespace Tests.nes.teststubs
{
    class TestROM : IROM
    {
        private byte[] _chrRom;
        private byte[] _prgRom;

        public TestROM()
        {
            _chrRom = GetDefaultChrRom();
            _prgRom = GetDefaultPrgRom();
        }

        public void SetChrRom(byte[] bytes)
        {
            _chrRom = bytes;
        }

        public void SetPrgRom(byte[] bytes)
        {
            _prgRom = bytes;
        }

        public int Mapper => 0;

        public byte[] GetChrRom()
        {
            return _chrRom;
        }

        public byte[] GetPrgRom()
        {
            return _prgRom;
        }

        public static byte[] GetDefaultPrgRom(ushort startAddress = 0)
        {
            var result = new byte[16384];
            result[result.Length - 3] = (byte)(startAddress & 0xFF); //FFFD low
            result[result.Length - 4] = (byte)(startAddress >> 8); //FFFC high
            return result;
        }

        public static byte[] GetDefaultChrRom()
        {
            var result = new byte[8192];
            return result;
        }
    }
}
