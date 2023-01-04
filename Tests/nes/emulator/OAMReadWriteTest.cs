using NesE.nes;
using NesE.nes.cpu;
using Tests.nes.teststubs;
using Xunit;

namespace Tests.nes.emulator
{
    public class OAMReadWriteTest
    {
        private NES _nes;

        public OAMReadWriteTest()
        {
            _nes = new NES();
            _nes.SetRom(new TestROM());
        }

        [Fact]
        public void ShouldReturnValueAtAddress()
        {
            const byte Expected = 32;
            const byte Address = 10;
            _nes.CPU.RAM.Set(0x2003, Address);
            _nes.PPU.OAM[Address] = Expected;
            
            _nes.CPU.RAM.Set(0, OP.LDA_ABS);
            _nes.CPU.RAM.Set(1, 0x04);
            _nes.CPU.RAM.Set(2, 0x20);

            _nes.Update(0);

            Assert.Equal(Expected, _nes.CPU.A);
        }

        [Fact]
        public void ShouldWriteValueAtAddress()
        {
            const byte Expected = 32;
            const byte Address = 10;
            _nes.CPU.RAM.Set(0x2003, Address);

            _nes.CPU.A = Expected;

            _nes.CPU.RAM.Set(0, OP.STA_ABS);
            _nes.CPU.RAM.Set(1, 0x04);
            _nes.CPU.RAM.Set(2, 0x20);

            _nes.Update(0);

            Assert.Equal(Expected, _nes.PPU.OAM[Address]);
        }

        [Fact]
        public void ShouldIncrementAddresOnWrite()
        {
            const byte Address = 10;
            const byte Expected = 32;
            _nes.CPU.RAM.Set(0x2003, Address);
            _nes.PPU.OAM[Address + 1] = Expected;

            _nes.CPU.RAM.Set(0, OP.STA_ABS);
            _nes.CPU.RAM.Set(1, 0x04);
            _nes.CPU.RAM.Set(2, 0x20);
            _nes.CPU.RAM.Set(3, OP.LDA_ABS);
            _nes.CPU.RAM.Set(4, 0x04);
            _nes.CPU.RAM.Set(5, 0x20);

            _nes.Update(0);
            _nes.Update(0);

            Assert.Equal(Expected, _nes.CPU.A);
        }
    }
}
