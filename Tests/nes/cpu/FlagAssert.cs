using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public static class FlagAssert
    {
        public static void AssertFlagSet(this CPU cpu, PFlag f)
        {
            Assert.True((cpu.P & f) != 0);
        }

        public static void AssertFlagCleared(this CPU cpu, PFlag f)
        {
            Assert.True((cpu.P & f) == 0);
        }
    }
}
