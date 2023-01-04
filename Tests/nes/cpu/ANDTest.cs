using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class ANDTest : BaseCPUTest
    {
        public ANDTest()
        {
            CPU.RAM[0] = OP.AND_IMM;
        }

        private class ANDTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.AND_IMM, (Action<byte, CPU>)MemorySetter.Immediate };
                yield return new object[] { OP.AND_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage };
                yield return new object[] { OP.AND_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX };
                yield return new object[] { OP.AND_ABS, (Action<byte, CPU>)MemorySetter.Absolute };
                yield return new object[] { OP.AND_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX };
                yield return new object[] { OP.AND_ABY, (Action<byte, CPU>)MemorySetter.AbsoluteY };
                yield return new object[] { OP.AND_IDX, (Action<byte, CPU>)MemorySetter.IndirectX };
                yield return new object[] { OP.AND_IDY, (Action<byte, CPU>)MemorySetter.IndirectY };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ANDTestData))]
        public void ShouldAnd(byte op, Action<byte, CPU> setValue)
        {
            CPU.RAM[0] = op;
            const byte ExpectedResult = 0b10000010;
            setValue(0b10101010, CPU);
            CPU.A = 0b11000011;

            CPU.Step();

            Assert.Equal(ExpectedResult, CPU.A);
        }

        [Fact]
        public void ShouldSetZero()
        {
            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.Z);
        }

        [Fact]
        public void ShouldClearZero()
        {
            CPU.RAM[1] = 0b10101010;
            CPU.A = 0b11000011;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.Z);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.N);
        }

        [Fact]
        public void ShouldSetNegative()
        {
            CPU.RAM[1] = 0b10000000;
            CPU.A = 0b10000000;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }
    }
}
