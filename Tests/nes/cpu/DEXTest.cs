using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class DEXTest : BaseCPUTest
    {
        public DEXTest() : base()
        {
            CPU.Ram[0] = OP.DEX_IMP;
        }

        [Fact]
        public void ShouldDecrement()
        {
            const byte Expected = 4;
            CPU.X = Expected + 1;

            CPU.Step();

            Assert.Equal(Expected, CPU.X);
        }

        [Fact]
        public void ShouldSetZero()
        {
            CPU.X = 1;

            CPU.Step();

            CPU.AssertFlagSet(PFlag.Z);
        }

        [Fact]
        public void ShouldClearZero()
        {
            CPU.SetFlag(PFlag.Z);
            CPU.X = 0;

            CPU.Step();

            CPU.AssertFlagCleared(PFlag.Z);
        }

        [Fact]
        public void ShouldSetNegative()
        {
            CPU.X = 0;

            CPU.Step();

            CPU.AssertFlagSet(PFlag.N);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            CPU.SetFlag(PFlag.N);
            CPU.X = 0b1000_0000;

            CPU.Step();

            CPU.AssertFlagCleared(PFlag.N);
        }
    }
}
