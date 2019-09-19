using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class RTITest : BaseCPUTest
    {
        [Fact]
        public void ShouldReturnFromSubroutine()
        {
            const PFlag ExpectedP = PFlag.C | PFlag.Z | PFlag.N | PFlag._;
            const ushort ExpectedPC = 0x1234;
            CPU.Ram[0] = OP.RTI_IMP;

            CPU.Ram[0x100 | CPU.S--] = 0x12;
            CPU.Ram[0x100 | CPU.S--] = 0x34;
            CPU.Ram[0x100 | CPU.S--] = (byte)(ExpectedP | PFlag.B | PFlag._);

            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
            Assert.Equal(ExpectedP, CPU.P);
        }
    }
}
