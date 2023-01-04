using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class INYTest : BaseCPUTest
    {
        public INYTest() : base()
        {
            CPU.RAM[0] = OP.INY_IMP;
        }

        [Fact]
        public void ShouldIncrement()
        {
            const byte Expected = 4;
            CPU.Y = Expected - 1;

            CPU.Step();

            Assert.Equal(Expected, CPU.Y);
        }

        [Fact]
        public void ShouldSetZero()
        {
            CPU.Y = 0xFF;

            CPU.Step();

            CPU.AssertFlagSet(PFlag.Z);
        }

        [Fact]
        public void ShouldClearZero()
        {
            CPU.SetFlag(PFlag.Z);
            CPU.Y = 0;

            CPU.Step();

            CPU.AssertFlagCleared(PFlag.Z);
        }

        [Fact]
        public void ShouldSetNegative()
        {
            CPU.Y = 0b0111_1111;

            CPU.Step();

            CPU.AssertFlagSet(PFlag.N);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            CPU.SetFlag(PFlag.N);
            CPU.Y = 0xFF;

            CPU.Step();

            CPU.AssertFlagCleared(PFlag.N);
        }
    }
}
