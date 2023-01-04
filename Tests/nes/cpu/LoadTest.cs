using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class LoadTest : BaseCPUTest
    {
        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.LDA_IMM, (Action<byte, CPU>)MemorySetter.Immediate, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.LDA_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.LDA_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.LDA_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.LDA_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.LDA_ABY, (Action<byte, CPU>)MemorySetter.AbsoluteY, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.LDX_IMM, (Action<byte, CPU>)MemorySetter.Immediate, (Func<CPU, byte>)MemoryGetter.X };
                yield return new object[] { OP.LDX_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Func<CPU, byte>)MemoryGetter.X };
                yield return new object[] { OP.LDX_ZPY, (Action<byte, CPU>)MemorySetter.ZeroPageY, (Func<CPU, byte>)MemoryGetter.X };
                yield return new object[] { OP.LDX_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Func<CPU, byte>)MemoryGetter.X };
                yield return new object[] { OP.LDX_ABY, (Action<byte, CPU>)MemorySetter.AbsoluteY, (Func<CPU, byte>)MemoryGetter.X };
                yield return new object[] { OP.LDY_IMM, (Action<byte, CPU>)MemorySetter.Immediate, (Func<CPU, byte>)MemoryGetter.Y };
                yield return new object[] { OP.LDY_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Func<CPU, byte>)MemoryGetter.Y };
                yield return new object[] { OP.LDY_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX, (Func<CPU, byte>)MemoryGetter.Y };
                yield return new object[] { OP.LDY_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Func<CPU, byte>)MemoryGetter.Y };
                yield return new object[] { OP.LDY_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX, (Func<CPU, byte>)MemoryGetter.Y };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldLoadDataToRegister(byte op, Action<byte, CPU> setMemory, Func<CPU, byte> getRegister)
        {
            const byte Expected = 0x44;
            CPU.RAM[0] = op;
            setMemory(Expected, CPU);

            CPU.Step();

            Assert.Equal(Expected, getRegister(CPU));
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldSetZeroFlag(byte op, Action<byte, CPU> setMemory, Func<CPU, byte> getRegister)
        {
            CPU.ClearFlag(PFlag.Z);
            CPU.RAM[0] = op;
            setMemory(0, CPU);

            CPU.Step();
            
            FlagAssert.AssertFlagSet(CPU, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldClearZeroFlag(byte op, Action<byte, CPU> setMemory, Func<CPU, byte> getRegister)
        {
            CPU.SetFlag(PFlag.Z);
            CPU.RAM[0] = op;
            setMemory(1, CPU);

            CPU.Step();
            
            FlagAssert.AssertFlagCleared(CPU, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldSetNegativeFlag(byte op, Action<byte, CPU> setMemory, Func<CPU, byte> getRegister)
        {
            CPU.ClearFlag(PFlag.N);
            CPU.RAM[0] = op;
            setMemory(0b1000_0000, CPU);

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldClearNegativeFlag(byte op, Action<byte, CPU> setMemory, Func<CPU, byte> getRegister)
        {
            CPU.SetFlag(PFlag.N);
            CPU.RAM[0] = op;
            setMemory(1, CPU);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.N);
        }
    }
}
