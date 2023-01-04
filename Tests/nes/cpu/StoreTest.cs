using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class StoreTest : BaseCPUTest
    {
        public class TransferTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.STA_ZEP, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.ZeroPage };
                yield return new object[] { OP.STA_ZPX, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.ZeroPageX };
                yield return new object[] { OP.STA_ABS, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.Absolute };
                yield return new object[] { OP.STA_ABX, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.AbsoluteX };
                yield return new object[] { OP.STA_ABY, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.AbsoluteY };
                yield return new object[] { OP.STA_IDX, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.IndirectX };
                yield return new object[] { OP.STA_IDY, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.IndirectY };

                yield return new object[] { OP.STX_ZEP, (Action<byte, CPU>)MemorySetter.XRegister, (Func<CPU, byte>)MemoryGetter.ZeroPage };
                yield return new object[] { OP.STX_ZPY, (Action<byte, CPU>)MemorySetter.XRegister, (Func<CPU, byte>)MemoryGetter.ZeroPageY };
                yield return new object[] { OP.STX_ABS, (Action<byte, CPU>)MemorySetter.XRegister, (Func<CPU, byte>)MemoryGetter.Absolute };

                yield return new object[] { OP.STY_ZEP, (Action<byte, CPU>)MemorySetter.YRegister, (Func<CPU, byte>)MemoryGetter.ZeroPage };
                yield return new object[] { OP.STY_ZPX, (Action<byte, CPU>)MemorySetter.YRegister, (Func<CPU, byte>)MemoryGetter.ZeroPageX };
                yield return new object[] { OP.STY_ABS, (Action<byte, CPU>)MemorySetter.YRegister, (Func<CPU, byte>)MemoryGetter.Absolute };

                yield return new object[] { OP.TAX_IMP, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.X };
                yield return new object[] { OP.TAY_IMP, (Action<byte, CPU>)MemorySetter.Accumulator, (Func<CPU, byte>)MemoryGetter.Y };
                yield return new object[] { OP.TSX_IMP, (Action<byte, CPU>)MemorySetter.StackPointer, (Func<CPU, byte>)MemoryGetter.X };
                yield return new object[] { OP.TXA_IMP, (Action<byte, CPU>)MemorySetter.XRegister, (Func<CPU, byte>)MemoryGetter.Accumulator };
                yield return new object[] { OP.TXS_IMP, (Action<byte, CPU>)MemorySetter.XRegister, (Func<CPU, byte>)MemoryGetter.StackPointer };
                yield return new object[] { OP.TYA_IMP, (Action<byte, CPU>)MemorySetter.YRegister, (Func<CPU, byte>)MemoryGetter.Accumulator };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class FlagTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.TAX_IMP, (Action<byte, CPU>)MemorySetter.Accumulator };
                yield return new object[] { OP.TAY_IMP, (Action<byte, CPU>)MemorySetter.Accumulator };
                yield return new object[] { OP.TSX_IMP, (Action<byte, CPU>)MemorySetter.StackPointer };
                yield return new object[] { OP.TXA_IMP, (Action<byte, CPU>)MemorySetter.XRegister };
                yield return new object[] { OP.TYA_IMP, (Action<byte, CPU>)MemorySetter.YRegister };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TransferTestData))]
        public void ShouldTransferValue(byte op, Action<byte, CPU> setValue, Func<CPU, byte> getValue)
        {
            CPU.RAM[0] = op;
            const byte Expected = 0x76;
            setValue(Expected, CPU);

            CPU.Step();

            Assert.Equal(Expected, getValue(CPU));
        }

        [Theory]
        [ClassData(typeof(FlagTestData))]
        public void ShouldSetZero(byte op, Action<byte, CPU> setValue)
        {
            CPU.RAM[0] = op;
            CPU.ClearFlag(PFlag.Z);
            setValue(0, CPU);

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(FlagTestData))]
        public void ShouldClearZero(byte op, Action<byte, CPU> setValue)
        {
            CPU.RAM[0] = op;
            CPU.SetFlag(PFlag.Z);
            setValue(1, CPU);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(FlagTestData))]
        public void ShouldSetNegative(byte op, Action<byte, CPU> setValue)
        {
            CPU.RAM[0] = op;
            CPU.ClearFlag(PFlag.N);
            setValue(0b1000_0000, CPU);

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }

        [Theory]
        [ClassData(typeof(FlagTestData))]
        public void ShouldClearNegative(byte op, Action<byte, CPU> setValue)
        {
            CPU.RAM[0] = op;
            CPU.SetFlag(PFlag.N);
            setValue(0, CPU);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.N);
        }
    }
}
