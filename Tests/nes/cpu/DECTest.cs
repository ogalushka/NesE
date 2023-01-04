using NesE.nes;
using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class DECTest : BaseCPUTest
    {
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
            CPU.RAM[0] = op;
            memorySetter(Expected + 1, CPU);

            CPU.Step();

            Assert.Equal(Expected, memoryGetter(CPU));
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldSetZero(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            CPU.RAM[0] = op;
            memorySetter(1, CPU);

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldClearZero(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            CPU.SetFlag(PFlag.Z);
            CPU.RAM[0] = op;
            memorySetter(2, CPU);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldSetNegative(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            CPU.RAM[0] = op;
            memorySetter(0, CPU);

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }

        [Theory]
        [ClassData(typeof(DECTestData))]
        public void ShouldClearNegative(byte op, Action<byte, CPU> memorySetter, Func<CPU, byte> memoryGetter)
        {
            CPU.SetFlag(PFlag.N);
            CPU.RAM[0] = op;
            memorySetter(0b1000_0000, CPU);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.N);
        }
    }
}
