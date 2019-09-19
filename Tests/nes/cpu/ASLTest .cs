using NesE.nes;
using NesE.nes.cpu;
using NesE.nes.cpu.opcode;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class ASLTest
    {
        private CPU _cpu;

        public ASLTest()
        {
            _cpu = new CPU(new TestRAM());
            _cpu.Ram[0] = OP.ASL_ACC;
        }

        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.ASL_ACC, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.ASL_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Func<CPU, byte>)MemoryGetter.ZeroPage };
                yield return new object[] { OP.ASL_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX, (Func<CPU, byte>)MemoryGetter.ZeroPageX };
                yield return new object[] { OP.ASL_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Func<CPU, byte>)MemoryGetter.Absolute };
                yield return new object[] { OP.ASL_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX, (Func<CPU, byte>)MemoryGetter.AbsoluteX };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldShift(byte op, Action<byte, CPU> setValue, Func<CPU, byte> getValue)
        {
            _cpu.Ram[0] = op;
            const byte ExpectedResult = 0b01000000;
            setValue(0b00100000, _cpu);

            _cpu.Step();

            Assert.Equal(ExpectedResult, getValue(_cpu));
        }

        [Fact]
        public void ShouldSetZero()
        {
            _cpu.A = 0b10000000;

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.Z);
        }

        [Fact]
        public void ShouldClearZero()
        {
            _cpu.A = 0b11000011;

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.Z);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            _cpu.A = 0b10000000;

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.N);
        }

        [Fact]
        public void ShouldSetNegative()
        {
            _cpu.A = 0b01000000;

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.N);
        }

        [Fact]
        public void ShouldSetCarry()
        {
            _cpu.A = 0b10000000;

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.C);
        }

        [Fact]
        public void ShouldClearCarry()
        {
            _cpu.A = 0b01000000;

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.C);
        }
    }
}
