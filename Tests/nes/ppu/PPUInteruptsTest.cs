using NesE.nes;
using Tests.nes.teststubs;
using Xunit;

namespace Tests.nes.ppu
{
    public class PPUInteruptsTest
    {
        private NES _nes;

        public PPUInteruptsTest()
        {
            _nes = new NES();
            _nes.SetRom(new TestROM());
        }

        [Fact]
        public void ShouldSetNMIOnEnable()
        {
            _nes.PPU.Registers.Status = 0x80;
            _nes.PPU.Registers.Set(0x2000, 0x80);

            Assert.True(_nes.CPU.Interupts.NMI);
        }

        [Fact]
        public void ShouldSetNMIOnOccurance()
        {
            _nes.PPU.Registers.Set(0x2000, 0x80);
            _nes.PPU.Registers.Status = 0x80;

            Assert.True(_nes.CPU.Interupts.NMI);
        }
    }
}
