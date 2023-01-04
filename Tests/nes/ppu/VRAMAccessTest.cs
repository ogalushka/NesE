using NesE.nes;
using NesE.nes.cpu;
using Tests.nes.teststubs;
using Xunit;

namespace Tests.nes.ppu
{
    public class VRAMAccessTest
    {
        private NES _nes;
        private OpWriter _opWriter;

        public VRAMAccessTest()
        {
            _nes = new NES();
            _nes.SetRom(new TestROM());
            _opWriter = new OpWriter(_nes);
        }

        [Fact]
        public void ShouldSetFirstAddressByte()
        {
            const ushort ExpectedAddress = 0x2000;

            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002); //reset latch
            SetValue(0x2006, 0x20);

            UpdateUntilPC(_opWriter.Counter);

            Assert.Equal(ExpectedAddress, _nes.PPU.Registers.Address);
        }

        [Fact]
        public void ShouldSetSecondAddressByte()
        {
            const ushort ExpectedAddress = 0x2030;

            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002); //reset latch
            SetValue(0x2006, 0x20);
            SetValue(0x2006, 0x30);

            UpdateUntilPC(_opWriter.Counter);

            Assert.Equal(ExpectedAddress, _nes.PPU.Registers.Address);
        }

        [Fact]
        public void ShouldSetUseLatch()
        {
            const ushort ExpectedAddress = 0x3000;

            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002); //reset latch

            SetValue(0x2006, 0x20);

            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002); //reset latch

            SetValue(0x2006, 0x30);

            UpdateUntilPC(_opWriter.Counter);

            Assert.Equal(ExpectedAddress, _nes.PPU.Registers.Address);
        }

        [Fact]
        public void WriteToVRAM()
        {
            const byte Expected = 0x12;
            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002);
            SetValue(0x2006, 0x20);
            SetValue(0x2006, 0x10);
            SetValue(0x2007, Expected);

            UpdateUntilPC(_opWriter.Counter);

            Assert.Equal(Expected, _nes.PPU.Memory.Get(0x2010));
        }

        [Fact]
        public void ShouldReadVRAM()
        {
            const byte Expected = 0x12;
            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002);
            SetValue(0x2006, 0x20);
            SetValue(0x2006, 0x10);
            SetValue(0x2007, Expected);

            SetValue(0x2006, 0x20);
            SetValue(0x2006, 0x10);
            _opWriter.PushByte(OP.LDA_ABS);
            _opWriter.PushUShort(0x2007);

            UpdateUntilPC(_opWriter.Counter);

            Assert.Equal(Expected, _nes.CPU.A);
        }

        [Fact]
        public void ShouldIncrementAddressByOneOnWrite()
        {
            const byte Expected = 0x12;
            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002);
            SetValue(0x2000, 0);
            SetValue(0x2006, 0x20);
            SetValue(0x2006, 0x00);
            SetValue(0x2007, 0);
            SetValue(0x2007, Expected);

            UpdateUntilPC(_opWriter.Counter);

            Assert.Equal(Expected, _nes.PPU.Memory.Get(0x2001));
        }

        [Fact]
        public void ShouldIncrementAddressBy32OnWrite()
        {
            const byte Expected = 0x12;
            _opWriter.PushByte(OP.BIT_ABS);
            _opWriter.PushUShort(0x2002);
            SetValue(0x2000, 0b100); // set increment
            SetValue(0x2006, 0x20);
            SetValue(0x2006, 0x00);
            SetValue(0x2007, 0);
            SetValue(0x2007, Expected);

            UpdateUntilPC(_opWriter.Counter);

            Assert.Equal(Expected, _nes.PPU.Memory.Get(0x2020));
        }

        private void SetValue(ushort Address, byte value)
        {
            _opWriter.PushByte(OP.LDA_IMM);
            _opWriter.PushByte(value);
            _opWriter.PushByte(OP.STA_ABS);
            _opWriter.PushUShort(Address);
        }

        private void UpdateUntilPC(ushort PC)
        {
            while (_nes.CPU.PC != PC)
            {
                _nes.Update(0);
            }
        }
    }
}
