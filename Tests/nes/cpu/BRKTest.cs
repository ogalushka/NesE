using NesE.nes;
using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class BRKTest
    {
        private readonly CPU _cpu;

        public BRKTest()
        {
            _cpu = new CPU(new RAM());
        }

        [Fact]
        public void ShouldSetPC()
        {
            _cpu.Ram[0] = OP.BRK_IMP;
            const ushort ExpectedPC = 0x1234;
            ushort expectedSP = (ushort)(_cpu.S - 3);
            _cpu.Ram[0xFFFE] = 0x34;
            _cpu.Ram[0xFFFF] = 0x12;

            _cpu.Step();

            Assert.Equal(ExpectedPC, _cpu.PC);
            Assert.Equal(expectedSP, _cpu.S);
        }

        [Fact]
        public void ShouldSetFlag()
        {
            _cpu.Ram[0] = OP.BRK_IMP;

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.B);
        }

        [Fact]
        public void ShouldSavePC()
        {
            _cpu.PC = 0x1234;
            _cpu.Ram[0x1234] = OP.BRK_IMP;

            var pointLower = _cpu.S - 1;
            var valueLower = 0x35;
            var pointHigher = _cpu.S;
            var valueHigher = 0x12;

            _cpu.Step();

            Assert.Equal(valueLower, _cpu.Ram[pointLower]);
            Assert.Equal(valueHigher, _cpu.Ram[pointHigher]);
        }

        [Fact]
        public void ShouldSaveP()
        {
            _cpu.Ram[0] = OP.BRK_IMP;
            _cpu.SetFlag(PFlag.C);
            var P = (byte)(_cpu.P | PFlag.B);

            _cpu.Step();

            Assert.Equal(P, _cpu.PullFromStack());
        }
    }
}
