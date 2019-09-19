using NesE.nes;
using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class INCTest
    {
        public readonly CPU _cpu;

        public INCTest()
        {
            _cpu = new CPU(new TestRAM());
        }

        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.INC_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Func<CPU, byte>)MemoryGetter.ZeroPage };
                yield return new object[] { OP.INC_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX, (Func<CPU, byte>)MemoryGetter.ZeroPageX };
                yield return new object[] { OP.INC_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Func<CPU, byte>)MemoryGetter.Absolute };
                yield return new object[] { OP.INC_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX, (Func<CPU, byte>)MemoryGetter.AbsoluteX };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldIncrement(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            const byte Expected = 0x32;
            _cpu.Ram[0] = op;
            memorySetter(Expected - 1, _cpu);

            _cpu.Step();

            Assert.Equal(Expected, memoryGetter(_cpu));
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldSetZero(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.Ram[0] = op;
            memorySetter(0xFF, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldClearZero(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.SetFlag(PFlag.Z);
            _cpu.Ram[0] = op;
            memorySetter(0, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldSetNegative(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.Ram[0] = op;
            memorySetter(0b0111_1111, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.N);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldClearNegative(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.SetFlag(PFlag.N);
            _cpu.Ram[0] = op;
            memorySetter(0xFF, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.N);
        }
    }
}
