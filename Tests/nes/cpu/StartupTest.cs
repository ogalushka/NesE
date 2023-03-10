using NesE.nes;
using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class StartupTest
    {
        [Fact]
        public void ShouldSetPCToValueFromFFFCFFFD()
        {
            const ushort Expected = 0x1234;
            var mem = new TestRAM();
            mem[0xFFFC] = Expected & 0xFF;
            mem[0xFFFD] = Expected >> 8;

            var cpu = new CPU(mem, new Interupts());

            cpu.Reset();
            Assert.Equal(Expected, cpu.PC);
        }
    }
}
