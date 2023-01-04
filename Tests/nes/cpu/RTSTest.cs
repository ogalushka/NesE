using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class RTSTest : BaseCPUTest
    {
        [Fact]
        public void ShouldJump()
        {
            CPU.RAM[0] = OP.RTS_IMP;
            const ushort Expected = 0x1234;

            CPU.RAM[0x1FF] = 0x12;
            CPU.RAM[0x1FE] = 0x33;
            CPU.S = 0xFD;

            CPU.Step();

            Assert.Equal(Expected, CPU.PC);
        }

        [Fact]
        public void ShouldPushReturnOnStack()
        {
            CPU.RAM[0] = OP.RTS_IMP;
            CPU.S = 0xFD;

            CPU.Step();

            Assert.Equal(0xFF, CPU.S);
        }
    }
}
