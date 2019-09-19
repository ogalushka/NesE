using NesE.nes;
using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class ADCTest
    {
        public TestRAM _ram;
        public CPU _cpu;

        public ADCTest()
        {
            _ram = new TestRAM();
            _cpu = new CPU(_ram);
            _ram[0] = OP.ADC_IMM;
        }

        public class ADCTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.ADC_IMM, (Action<byte, CPU>)MemorySetter.Immediate };
                yield return new object[] { OP.ADC_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage };
                yield return new object[] { OP.ADC_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX };
                yield return new object[] { OP.ADC_ABS, (Action<byte, CPU>)MemorySetter.Absolute };
                yield return new object[] { OP.ADC_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX };
                yield return new object[] { OP.ADC_ABY, (Action<byte, CPU>)MemorySetter.AbsoluteY };
                yield return new object[] { OP.ADC_IDX, (Action<byte, CPU>)MemorySetter.IndirectX };
                yield return new object[] { OP.ADC_IDY, (Action<byte, CPU>)MemorySetter.IndirectY };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ADCTestData))]
        public void ShouldAdd(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;
            const byte ExpetedResult = 0x11;
            setValue(ExpetedResult, _cpu);

            _cpu.Step();

            Assert.Equal(ExpetedResult, _cpu.A);
        }

        [Fact]
        public void ShouldUseCarry()
        {
            const byte ExpectedResult = 0x01;
            _cpu.P |= PFlag.C;

            _cpu.Step();

            Assert.Equal(ExpectedResult, _cpu.A);
        }

        [Fact]
        public void ShouldAddOverflow()
        {
            const byte ExpectedResult = 0x1E;

            const byte Initical = 0xFF;
            const byte ToAdd = 0x1F;

            _cpu.A = Initical;
            _ram[1] = ToAdd;

            _cpu.Step();

            Assert.Equal(ExpectedResult, _cpu.A);
        }

        [Fact]
        public void CheckZeroFlag()
        {
            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.Z);
        }

        [Fact]
        public void CheckNonZeroFlag()
        {
            _ram[1] = 1;

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.Z);
        }

        [Fact]
        public void CheckCarrySet()
        {
            _ram[1] = 1;
            _cpu.A = 0xFF;

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.C);
        }

        [Fact]
        public void CheckCarryUnSet()
        {
            _ram[1] = 1;
            _cpu.A = 0xFE;

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.C);
        }

        [Theory]
        [InlineData(0x7F, 0x01)]
        [InlineData(0x80, 0xFF)]
        public void CheckOverflowSet(byte b1, byte b2)
        {
            _cpu.A = b1;
            _ram[1] = b2;

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.V);
        }

        [Theory]
        [InlineData(0x01, 0x01)]
        [InlineData(0x01, 0xFF)]
        public void CheckOverflowUnSet(byte b1, byte b2)
        {
            _cpu.A = b1;
            _ram[1] = b2;

            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.V);
        }

        [Fact]
        public void CheckNegativeSet()
        {
            _ram[1] = 0xFF;

            _cpu.Step();

            FlagAssert.AssertFlagSet(_cpu, PFlag.N);
        }

        [Fact]
        public void CheckNegativeUnset()
        {
            _cpu.Step();

            FlagAssert.AssertFlagCleared(_cpu, PFlag.N);
        }
    }
}
