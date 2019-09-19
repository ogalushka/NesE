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
            _cpu = new CPU(new TestRAM());
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
        public void ShouldSavePC()
        {
            _cpu.PC = 0x1234;
            _cpu.Ram[0x1234] = OP.BRK_IMP;

            var pointLower = 0x100 | (_cpu.S - 1);
            var valueLower = 0x35;
            var pointHigher = 0x100 | _cpu.S;
            var valueHigher = 0x12;

            _cpu.Step();

            Assert.Equal(valueLower, _cpu.Ram[pointLower]);
            Assert.Equal(valueHigher, _cpu.Ram[pointHigher]);
        }

        [Fact]
        public void ShouldSavePWithFlag()
        {
            _cpu.Ram[0] = OP.BRK_IMP;
            _cpu.SetFlag(PFlag.C);
            var P = (byte)(_cpu.P | PFlag.B | PFlag._);

            _cpu.Step();

            Assert.Equal(P, _cpu.PullFromStack());
        }
    }
}
