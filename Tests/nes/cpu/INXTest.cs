using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class INXTest : BaseCPUTest
    {
        public INXTest() : base()
        {
            CPU.Ram[0] = OP.INX_IMP;
        }

        [Fact]
        public void ShouldIncrement()
        {
            const byte Expected = 4;
            CPU.X = Expected - 1;

            CPU.Step();

            Assert.Equal(Expected, CPU.X);
        }

        [Fact]
        public void ShouldSetZero()
        {
            CPU.X = 0xFF;

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
            CPU.X = 0b0111_1111;

            CPU.Step();

            CPU.AssertFlagSet(PFlag.N);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            CPU.SetFlag(PFlag.N);
            CPU.X = 0xFF;

            CPU.Step();

            CPU.AssertFlagCleared(PFlag.N);
        }
    }
}
