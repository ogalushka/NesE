using NesE.nes;
using NesE.nes.cpu;
using Tests.nes.teststubs;
using Xunit;

namespace Tests.nes.emulator
{
    public class OAMDMACopyTest
    {
        [Fact]
        public void ShouldCopyMemOnWrite()
        {
            const byte Page = 0x02;

            var rom = new TestROM();
            var nes = new NES();
            nes.SetRom(rom);
            var expectedValues = new byte[0x100];
            for (int i = 0; i < 0x100; i++)
            {
                var address = (Page << 8) | i;
                nes.CPU.RAM.Set(address, (byte)i);
                expectedValues[i] = (byte)i;
            }

            //load 0x02 page into 0x4014 address to start copy
            nes.CPU.RAM.Set(0, OP.LDA_IMM);
            nes.CPU.RAM.Set(1, Page);
            nes.CPU.RAM.Set(2, OP.STA_ABS);
            nes.CPU.RAM.Set(3, 0x14); //low
            nes.CPU.RAM.Set(4, 0x40); //high

            nes.Update(0);
            nes.Update(0);

            Assert.Equal(expectedValues, nes.PPU.OAM);
        }
    }
}
