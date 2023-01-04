using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class BRKTest : BaseCPUTest
    {
        [Fact]
        public void ShouldSetPC()
        {
            CPU.RAM[0] = OP.BRK_IMP;
            const ushort ExpectedPC = 0x1234;
            ushort expectedSP = (ushort)(CPU.S - 3);
            CPU.RAM[0xFFFE] = 0x34;
            CPU.RAM[0xFFFF] = 0x12;

            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
            Assert.Equal(expectedSP, CPU.S);
        }

        [Fact]
        public void ShouldSavePC()
        {
            CPU.PC = 0x1234;
            CPU.RAM[0x1234] = OP.BRK_IMP;

            var pointLower = 0x100 | (CPU.S - 1);
            var valueLower = 0x35;
            var pointHigher = 0x100 | CPU.S;
            var valueHigher = 0x12;

            CPU.Step();

            Assert.Equal(valueLower, CPU.RAM[pointLower]);
            Assert.Equal(valueHigher, CPU.RAM[pointHigher]);
        }

        [Fact]
        public void ShouldSavePWithFlag()
        {
            CPU.RAM[0] = OP.BRK_IMP;
            CPU.SetFlag(PFlag.C);
            var P = (byte)(CPU.P | PFlag.B | PFlag._);

            CPU.Step();

            Assert.Equal(P, CPU.PullFromStack());
        }
    }
}
