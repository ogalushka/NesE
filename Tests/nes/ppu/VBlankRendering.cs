using NesE.nes;
using Tests.nes.teststubs;
using Xunit;

namespace Tests.nes.ppu
{
    public class VBlankRendering
    {
        [Fact]
        public void ShouldSetVBlankOnCycle()
        {
            var nes = new NES();
            nes.SetRom(new TestROM());
            var ppu = nes.PPU;

            const int CyclesPerLine = 341;
            const int LinesTillSet = 241;
            const int CyclesTillVBlank = (CyclesPerLine * LinesTillSet) + 1; // should occur on second cycle of line 241

            for (var i = 0; i < CyclesTillVBlank; i++)
            {
                ppu.Step();
                Assert.True((ppu.Registers.Status & 0x80) == 0);
            }

            ppu.Step();
            Assert.True((ppu.Registers.Status & 0x80) != 0);
        }

        [Fact]
        public void ShouldClearVBlankOnFirstCycle()
        {
            var nes = new NES();
            nes.SetRom(new TestROM());
            var ppu = nes.PPU;

            ppu.Registers.Status |= 0x80;
            ppu.Step();

            Assert.True((ppu.Registers.Status & 0x80) == 0);
        }
    }
}
