using NesE.nes;
using NesE.nes.cpu;
using NesE.nes.cpu.opcode;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class EORTest
    {
        private CPU _cpu;
        public EORTest()
        {
            _cpu = new CPU(new TestRAM());
            _cpu.Ram[0] = OP.EOR_IMM;
        }

        private class ANDTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.EOR_IMM, (Action<byte, CPU>)MemorySetter.Immediate };
                yield return new object[] { OP.EOR_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage };
                yield return new object[] { OP.EOR_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX };
                yield return new object[] { OP.EOR_ABS, (Action<byte, CPU>)MemorySetter.Absolute };
                yield return new object[] { OP.EOR_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX };
                yield return new object[] { OP.EOR_ABY, (Action<byte, CPU>)MemorySetter.AbsoluteY };
                yield return new object[] { OP.EOR_IDX, (Action<byte, CPU>)MemorySetter.IndirectX };
                yield return new object[] { OP.EOR_IDY, (Action<byte, CPU>)MemorySetter.IndirectY };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ANDTestData))]
        public void ShouldAnd(byte op, Action<byte, CPU> setValue)
        {
            _cpu.Ram[0] = op;
            const byte ExpectedResult = 0b01101001;
            setValue(0b10101010, _cpu);
            _cpu.A = 0b11000011;

            _cpu.Step();

            Assert.Equal(ExpectedResult, _cpu.A);
        }

        [Fact]
        public void ShouldSetZero()
        {
            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.Z);
        }

        [Fact]
        public void ShouldClearZero()
        {
            _cpu.Ram[1] = 0b10101010;
            _cpu.A = 0b11000011;

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.Z);
        }

        [Fact]
        public void ShouldClearNegative()
        {
            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.N);
        }

        [Fact]
        public void ShouldSetNegative()
        {
            _cpu.A = 0b10000000;

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.N);
        }
    }
}
