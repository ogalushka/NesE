using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class NOPTest : BaseCPUTest
    {
        [Fact]
        public void ShouldExecute()
        {
            CPU.Ram[0] = OP.NOP_IMP;

            CPU.Step();

            Assert.Equal(1, CPU.PC);
        }
    }
}
