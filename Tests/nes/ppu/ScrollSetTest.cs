using NesE.nes;
using NesE.nes.cpu;
using Tests.nes.teststubs;
using Xunit;

namespace Tests.nes.ppu
{
    public class ScrollSetTest
    {
        [Fact]
        public void ShouldSetFirstScrollByte()
        {
            const ushort ExpectedScroll = 0x2000;
            var nes = new NES();
            nes.SetRom(new TestROM());

            var op = new OpWriter(nes);

            op.PushByte(OP.BIT_ABS);
            op.PushUShort(0x2002); //reset latch
            op.PushByte(OP.LDA_IMM);
            op.PushByte(0x20);
            op.PushByte(OP.STA_ABS);
            op.PushUShort(0x2005);

            nes.Update(0);
            nes.Update(0);
            nes.Update(0);

            Assert.Equal(ExpectedScroll, nes.PPU.Registers.Scroll);
        }

        [Fact]
        public void ShouldSetSecondScrollByte()
        {
            const ushort ExpectedScroll = 0x2030;
            var nes = new NES();
            nes.SetRom(new TestROM());

            var op = new OpWriter(nes);

            op.PushByte(OP.BIT_ABS);
            op.PushUShort(0x2002); //reset latch
            op.PushByte(OP.LDA_IMM);
            op.PushByte(0x20);
            op.PushByte(OP.STA_ABS);
            op.PushUShort(0x2005);
            op.PushByte(OP.LDA_IMM);
            op.PushByte(0x30);
            op.PushByte(OP.STA_ABS);
            op.PushUShort(0x2005);

            nes.Update(0);
            nes.Update(0);
            nes.Update(0);
            nes.Update(0);
            nes.Update(0);

            Assert.Equal(ExpectedScroll, nes.PPU.Registers.Scroll);
        }

        [Fact]
        public void ShouldSetUseLatch()
        {
            const ushort ExpectedScroll = 0x3000;
            var nes = new NES();
            nes.SetRom(new TestROM());

            var op = new OpWriter(nes);

            op.PushByte(OP.BIT_ABS);
            op.PushUShort(0x2002); //reset latch
            op.PushByte(OP.LDA_IMM);
            op.PushByte(0x20);
            op.PushByte(OP.STA_ABS);
            op.PushUShort(0x2005);
            op.PushByte(OP.BIT_ABS);
            op.PushUShort(0x2002); //reset latch
            op.PushByte(OP.LDA_IMM);
            op.PushByte(0x30);
            op.PushByte(OP.STA_ABS);
            op.PushUShort(0x2005);

            nes.Update(0);
            nes.Update(0);
            nes.Update(0);
            nes.Update(0);
            nes.Update(0);
            nes.Update(0);

            Assert.Equal(ExpectedScroll, nes.PPU.Registers.Scroll);
        }
    }
}
