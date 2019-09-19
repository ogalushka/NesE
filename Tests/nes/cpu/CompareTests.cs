using NesE.nes;
using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class CompareTests
    {
        private readonly CPU _cpu;

        public CompareTests()
        {
            _cpu = new CPU(new TestRAM());
        }

        private class CompareTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.CMP_IMM, (Action<byte, CPU>)MemorySetter.Immediate, (Action<byte, CPU>)SetA};
                yield return new object[] { OP.CMP_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Action<byte, CPU>)SetA };
                yield return new object[] { OP.CMP_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX, (Action<byte, CPU>)SetA };
                yield return new object[] { OP.CMP_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Action<byte, CPU>)SetA };
                yield return new object[] { OP.CMP_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX, (Action<byte, CPU>)SetA };
                yield return new object[] { OP.CMP_ABY, (Action<byte, CPU>)MemorySetter.AbsoluteY, (Action<byte, CPU>)SetA };
                yield return new object[] { OP.CMP_IDX, (Action<byte, CPU>)MemorySetter.IndirectX, (Action<byte, CPU>)SetA };
                yield return new object[] { OP.CMP_IDY, (Action<byte, CPU>)MemorySetter.IndirectY, (Action<byte, CPU>)SetA };
                yield return new object[] { OP.CPX_IMM, (Action<byte, CPU>)MemorySetter.Immediate, (Action<byte, CPU>)SetX };
                yield return new object[] { OP.CPX_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Action<byte, CPU>)SetX };
                yield return new object[] { OP.CPX_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Action<byte, CPU>)SetX };
                yield return new object[] { OP.CPY_IMM, (Action<byte, CPU>)MemorySetter.Immediate, (Action<byte, CPU>)SetY };
                yield return new object[] { OP.CPY_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage, (Action<byte, CPU>)SetY };
                yield return new object[] { OP.CPY_ABS, (Action<byte, CPU>)MemorySetter.Absolute, (Action<byte, CPU>)SetY };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private void SetA(byte b, CPU cpu)
            {
                cpu.A = b;
            }

            private void SetX(byte b, CPU cpu)
            {
                cpu.X = b;
            }

            private void SetY(byte b, CPU cpu)
            {
                cpu.Y = b;
            }
        }

        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void ShouldSetZero(byte op, Action<byte, CPU> setMemoryValue, Action<byte, CPU> setRegisterValue)
        {
            _cpu.Ram[0] = op;
            const byte Value = 0x14;
            setMemoryValue(Value, _cpu);
            setRegisterValue(Value, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void ShouldClearZero(byte op, Action<byte, CPU> setMemoryValue, Action<byte, CPU> setRegisterValue)
        {
            _cpu.Ram[0] = op;
            const byte Value = 0x14;
            setMemoryValue(Value, _cpu);
            setRegisterValue(Value + 1, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.Z);
        }


        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void ShouldSetCarryIfEqual(byte op, Action<byte, CPU> setMemoryValue, Action<byte, CPU> setRegisterValue)
        {
            _cpu.Ram[0] = op;
            const byte Value = 0x14;
            setMemoryValue(Value, _cpu);
            setRegisterValue(Value, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.C);
        }

        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void ShouldSetCarryIfMore(byte op, Action<byte, CPU> setMemoryValue, Action<byte, CPU> setRegisterValue)
        {
            _cpu.Ram[0] = op;
            const byte Value = 0x14;
            setMemoryValue(Value, _cpu);
            setRegisterValue(Value + 1, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.C);
        }

        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void ShouldClearCarry(byte op, Action<byte, CPU> setMemoryValue, Action<byte, CPU> setRegisterValue)
        {
            _cpu.Ram[0] = op;
            const byte Value = 0x14;
            setMemoryValue(Value, _cpu);
            setRegisterValue(Value - 1, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.C);
        }

        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void ShouldSetNegative(byte op, Action<byte, CPU> setMemoryValue, Action<byte, CPU> setRegisterValue)
        {
            _cpu.Ram[0] = op;
            const byte Value = 0x14;
            setMemoryValue(Value, _cpu);
            setRegisterValue(Value - 1, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.N);
        }

        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void ShouldClearNegative(byte op, Action<byte, CPU> setMemoryValue, Action<byte, CPU> setRegisterValue)
        {
            _cpu.Ram[0] = op;
            const byte Value = 0x14;
            setMemoryValue(Value, _cpu);
            setRegisterValue(Value, _cpu);

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.N);
        }
    }
}
