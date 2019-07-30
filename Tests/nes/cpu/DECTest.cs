using NesE.nes;
using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class DECTest
    {
        public readonly CPU _cpu;

        public DECTest()
        {
            _cpu = new CPU(new RAM());
        }

        private class DECTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.DEC_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Func<CPU, byte>)MemoryGetter.ZeroPage };
                yield return new object[] { OP.DEC_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX, (Func<CPU, byte>)MemoryGetter.ZeroPageX };
                yield return new object[] { OP.DEC_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Func<CPU, byte>)MemoryGetter.Absolute };
                yield return new object[] { OP.DEC_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX, (Func<CPU, byte>)MemoryGetter.AbsoluteX };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldDecrement(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            const byte Expected = 0x32;
            _cpu.Ram[0] = op;
            memorySetter(Expected + 1, _cpu);

            _cpu.Step();

            Assert.Equal(Expected, memoryGetter(_cpu));
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldSetZero(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.Ram[0] = op;
            memorySetter(1, _cpu);

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldClearZero(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.SetFlag(PFlag.Z);
            _cpu.Ram[0] = op;
            memorySetter(2, _cpu);

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldSetNegative(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.Ram[0] = op;
            memorySetter(0, _cpu);

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.N);
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldClearNegative(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            _cpu.SetFlag(PFlag.N);
            _cpu.Ram[0] = op;
            memorySetter(0b1000_0000, _cpu);

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.N);
        }
    }
}
