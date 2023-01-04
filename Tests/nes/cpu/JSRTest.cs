using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class JSRTest : BaseCPUTest
    {
        [Fact]
        public void ShouldJump()
        {
            CPU.RAM[0] = OP.JSR_ABS;
            const ushort Expected = 0x1234;
            CPU.RAM[1] = 0x34;
            CPU.RAM[2] = 0x12;

            CPU.Step();

            Assert.Equal(Expected, CPU.PC);
        }

        [Fact]
        public void ShouldPushReturnOnStack()
        {
            CPU.RAM[0] = OP.JSR_ABS;
            CPU.RAM[1] = 0x34;
            CPU.RAM[2] = 0x12;

            CPU.Step();

            Assert.Equal(2, CPU.RAM[0x100 | (CPU.S + 1)]);
            Assert.Equal(0, CPU.RAM[0x100 | (CPU.S + 2)]);
        }
    }
}
