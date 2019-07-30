using NesE.nes;
using NesE.nes.cpu;
using NesE.nes.cpu.addressign;
using Xunit;

namespace Tests.nes.addressing
{
    public class AddressingModes
    {
        private readonly CPU _cpu;

        public AddressingModes()
        {
            _cpu = new CPU(new RAM());
        }

        [Fact]
        public void Accumulator()
        {
            const byte Expected = 0x33;
            _cpu.A = Expected;

            Assert.Equal(Expected, new Accumulator().GetValue(_cpu));
            Assert.Equal(0, _cpu.PC);
        }

        [Fact]
        public void Immidiate()
        {
            const byte Expected = 0xF3;
            _cpu.Ram[0] = Expected;

            Assert.Equal(Expected, new Immediate().GetValue(_cpu));
            Assert.Equal(1, _cpu.PC);
        }

        [Fact]
        public void ZeroPage()
        {
            const byte Expected = 0x33;
            const byte Address = 0x10;
            _cpu.Ram[Address] = Expected;
            _cpu.Ram[0] = Address;

            Assert.Equal(Expected, new ZeroPage().GetValue(_cpu));
            Assert.Equal(1, _cpu.PC);
        }

        [Fact]
        public void ZeroPageX()
        {
            const byte Expected = 0x34;
            _cpu.X = 0x09;
            _cpu.Ram[0] = 0x01;
            _cpu.Ram[0xA] = Expected;

            Assert.Equal(Expected, new ZeroPageX().GetValue(_cpu));
            Assert.Equal(1, _cpu.PC);
        }

        [Fact]
        public void ZeroPageXOverflow()
        {
            const byte Expected = 0x35;
            _cpu.Ram[0] = 0xFF;
            _cpu.X = 0x04;
            _cpu.Ram[0x03] = Expected;

            Assert.Equal(Expected, new ZeroPageX().GetValue(_cpu));
            Assert.Equal(1, _cpu.PC);
        }

        [Fact]
        public void Absolute()
        {
            const byte Expected = 0x36;
            _cpu.Ram[0] = 0x34;
            _cpu.Ram[1] = 0x12;
            _cpu.Ram[0x1234] = Expected;

            Assert.Equal(Expected, new Absolute().GetValue(_cpu));
            Assert.Equal(2, _cpu.PC);
        }

        [Fact]
        public void AbsoluteX()
        {
            //TODO check overflows
            const byte Expected = 0x36;
            _cpu.Ram[0] = 0x33;
            _cpu.Ram[1] = 0x12;
            _cpu.Ram[0x1234] = Expected;
            _cpu.X = 1;

            Assert.Equal(Expected, new AbsoluteX().GetValue(_cpu));
            Assert.Equal(2, _cpu.PC);
        }

        [Fact]
        public void AbsoluteY()
        {
            //TODO check overflows
            const byte Expected = 0x37;
            _cpu.Ram[0] = 0x33;
            _cpu.Ram[1] = 0x12;
            _cpu.Ram[0x1234] = Expected;
            _cpu.Y = 1;

            Assert.Equal(Expected, new AbsoluteY().GetValue(_cpu));
            Assert.Equal(2, _cpu.PC);
        }

        [Fact]
        public void IndexedIndirectX()
        {
            const byte Expected = 0x39;

            _cpu.Ram[0] = 0x10;
            _cpu.X = 1;
            _cpu.Ram[0x11] = 0x64;
            _cpu.Ram[0x12] = 0x32;
            _cpu.Ram[0x3264] = Expected;

            Assert.Equal(Expected, new IndirectX().GetValue(_cpu));
            Assert.Equal(1, _cpu.PC);
        }

        [Fact]
        public void IndexedIndirectXWrap()
        {
            const byte Expected = 0x40;

            _cpu.Ram[0] = 0x10;
            _cpu.X = 2;
            _cpu.Ram[0x12] = 0x64;
            _cpu.Ram[0x13] = 0x32;
            _cpu.Ram[0x3264] = Expected;

            Assert.Equal(Expected, new IndirectX().GetValue(_cpu));
            Assert.Equal(1, _cpu.PC);
        }

        [Fact]
        public void IndexedIndirectY()
        {
            const byte Expected = 0x39;

            _cpu.Ram[0] = 0x15;
            _cpu.Ram[0x15] = 0x30;
            _cpu.Ram[0x16] = 0x12;
            _cpu.Y = 4;
            _cpu.Ram[0x1234] = Expected;

            Assert.Equal(Expected, new IndirectY().GetValue(_cpu));
            Assert.Equal(1, _cpu.PC);
        }
    }
}
