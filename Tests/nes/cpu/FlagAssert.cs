using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public static class FlagAssert
    {
        public static void Set(CPU cpu, PFlag f)
        {
            Assert.True((cpu.P & f) != 0);
        }

        public static void Cleared(CPU cpu, PFlag f)
        {
            Assert.True((cpu.P & f) == 0);
        }
    }
}
