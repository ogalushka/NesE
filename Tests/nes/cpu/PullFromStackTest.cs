using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class PullFromStackTest : BaseCPUTest
    {
        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.PLA_IMP, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.PLP_IMP, (Func<CPU, byte>)MemoryGetter.Status };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void ShouldPullAccumulator()
        {
            CPU.Ram[0] = OP.PLA_IMP;
            byte Expected = 0x44;
            CPU.Ram[0x100 | CPU.S] = Expected;
            CPU.S--;

            CPU.Step();

            Assert.Equal(Expected, CPU.A);
        }

        [Fact]
        public void ShouldPullStatusIgnoringBytes()
        {
            CPU.Ram[0] = OP.PLP_IMP;
            byte Expected = 0b1110_0011;
            byte memValue = (byte)((Expected & 0b1101_1111) | 0b0001_0000);
            CPU.Ram[0x100 | CPU.S] = memValue;
            CPU.S--;

            CPU.Step();

            Assert.Equal(Expected, (byte)CPU.P);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldChangeStackPointer(byte op, Func<CPU, byte> registerGetter)
        {
            CPU.Ram[0] = op;
            var Expected = CPU.S;
            CPU.S--;

            CPU.Step();

            Assert.Equal(Expected, CPU.S);
        }

        [Fact]
        public void ShouldSetN()
        {
            CPU.ClearFlag(PFlag.N);
            CPU.Ram[0] = OP.PLA_IMP;
            CPU.Ram[0x100 | CPU.S] = 0b1000_0000;
            CPU.S--;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }

        [Fact]
        public void ShouldClearN()
        {
            CPU.SetFlag(PFlag.N);
            CPU.Ram[0] = OP.PLA_IMP;
            CPU.Ram[0x100 | CPU.S] = 0;
            CPU.S--;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.N);
        }

        [Fact]
        public void ShouldSetZ()
        {
            CPU.ClearFlag(PFlag.Z);
            CPU.Ram[0] = OP.PLA_IMP;
            CPU.Ram[0x100 | CPU.S] = 0;
            CPU.S--;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.Z);
        }

        [Fact]
        public void ShouldClearZ()
        {
            CPU.SetFlag(PFlag.Z);
            CPU.Ram[0] = OP.PLA_IMP;
            CPU.Ram[0x100 | CPU.S] = 1;
            CPU.S--;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.Z);
        }
    }
}
