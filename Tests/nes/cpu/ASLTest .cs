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
            _cpu = new CPU(new RAM());
            _cpu.Ram[0] = OP.ASL_ACC;
        }

        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.ASL_ACC, (Action<byte, CPU>)MemorySetter.Accumulator };
                yield return new object[] { OP.ASL_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage };
                yield return new object[] { OP.ASL_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX };
                yield return new object[] { OP.ASL_ABS, (Action<byte, CPU>)MemorySetter.Absolute };
                yield return new object[] { OP.ASL_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldShift(byte op, Action<byte, CPU> setValue)
        {
            _cpu.Ram[0] = op;
            const byte ExpectedResult = 0b01000000;
            setValue(0b00100000, _cpu);

            _cpu.Step();

            Assert.Equal(ExpectedResult, _cpu.A);
        }

        [Fact]
        public void ShouldSetZero()
        {
            _cpu.A = 0b10000000;

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.Z);
        }

        [Fact]
        public void ShouldClearZero()
        {
            _cpu.A = 0b11000011;

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.Z);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            _cpu.A = 0b10000000;

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.N);
        }

        [Fact]
        public void ShouldSetNegative()
        {
            _cpu.A = 0b01000000;

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.N);
        }

        [Fact]
        public void ShouldSetCarry()
        {
            _cpu.A = 0b10000000;

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.C);
        }

        [Fact]
        public void ShouldClearCarry()
        {
            _cpu.A = 0b01000000;

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.C);
        }
    }
}
