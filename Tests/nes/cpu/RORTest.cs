using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class RORTest : BaseCPUTest
    {
        public RORTest()
        {
            CPU.RAM[0] = OP.ROR_ACC;
        }

        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.ROR_ACC, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.ROR_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Func<CPU, byte>)MemoryGetter.ZeroPage };
                yield return new object[] { OP.ROR_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX, (Func<CPU, byte>)MemoryGetter.ZeroPageX };
                yield return new object[] { OP.ROR_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Func<CPU, byte>)MemoryGetter.Absolute };
                yield return new object[] { OP.ROR_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX, (Func<CPU, byte>)MemoryGetter.AbsoluteX };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldShift(byte op, Action<byte, CPU> setValue, Func<CPU, byte> getValue)
        {
            CPU.RAM[0] = op;
            const byte ExpectedResult = 0b01000000;
            setValue(0b10000000, CPU);

            CPU.Step();

            Assert.Equal(ExpectedResult, getValue(CPU));
        }

        [Fact]
        public void ShouldUseCarry()
        {
            CPU.A = 0;
            CPU.SetFlag(PFlag.C);

            CPU.Step();

            Assert.Equal(0b10000000, CPU.A);
        }

        [Fact]
        public void ShouldSetZero()
        {
            CPU.A = 0b00000001;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.Z);
        }

        [Fact]
        public void ShouldClearZero()
        {
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
            CPU.SetFlag(PFlag.C);
            CPU.A = 0b00000000;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }

        [Fact]
        public void ShouldSetCarry()
        {
            CPU.A = 0b00000001;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.C);
        }

        [Fact]
        public void ShouldClearCarry()
        {
            CPU.A = 0b01000000;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.C);
        }
    }
}
