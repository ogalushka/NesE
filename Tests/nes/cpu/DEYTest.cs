using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class DEYTest : BaseCPUTest
    {
        public DEYTest() : base()
        {
            CPU.RAM[0] = OP.DEY_IMP;
        }

        [Fact]
        public void ShouldDecrement()
        {
            const byte Expected = 4;
            CPU.Y = Expected + 1;

            CPU.Step();

            Assert.Equal(Expected, CPU.Y);
        }

        [Fact]
        public void ShouldSetZero()
        {
            CPU.Y = 1;

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
            CPU.Y = 0;

            CPU.Step();

            CPU.AssertFlagSet(PFlag.N);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            CPU.SetFlag(PFlag.N);
            CPU.Y = 0b1000_0000;

            CPU.Step();

            CPU.AssertFlagCleared(PFlag.N);
        }
    }
}
