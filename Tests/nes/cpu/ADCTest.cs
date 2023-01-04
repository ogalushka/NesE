using NesE.nes;
using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class ADCTest : BaseCPUTest
    {
        public TestRAM _ram;

        public ADCTest()
        {
            _ram = CPU.RAM as TestRAM;
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
            setValue(ExpetedResult, CPU);

            CPU.Step();

            Assert.Equal(ExpetedResult, CPU.A);
        }

        [Fact]
        public void ShouldUseCarry()
        {
            const byte ExpectedResult = 0x01;
            CPU.P |= PFlag.C;

            CPU.Step();

            Assert.Equal(ExpectedResult, CPU.A);
        }

        [Fact]
        public void ShouldAddOverflow()
        {
            const byte ExpectedResult = 0x1E;

            const byte Initical = 0xFF;
            const byte ToAdd = 0x1F;

            CPU.A = Initical;
            _ram[1] = ToAdd;

            CPU.Step();

            Assert.Equal(ExpectedResult, CPU.A);
        }

        [Fact]
        public void CheckZeroFlag()
        {
            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.Z);
        }

        [Fact]
        public void CheckNonZeroFlag()
        {
            _ram[1] = 1;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.Z);
        }

        [Fact]
        public void CheckCarrySet()
        {
            _ram[1] = 1;
            CPU.A = 0xFF;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.C);
        }

        [Fact]
        public void CheckCarryUnSet()
        {
            _ram[1] = 1;
            CPU.A = 0xFE;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.C);
        }

        [Theory]
        [InlineData(0x7F, 0x01)]
        [InlineData(0x80, 0xFF)]
        public void CheckOverflowSet(byte b1, byte b2)
        {
            CPU.A = b1;
            _ram[1] = b2;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.V);
        }

        [Theory]
        [InlineData(0x01, 0x01)]
        [InlineData(0x01, 0xFF)]
        public void CheckOverflowUnSet(byte b1, byte b2)
        {
            CPU.A = b1;
            _ram[1] = b2;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.V);
        }

        [Fact]
        public void CheckNegativeSet()
        {
            _ram[1] = 0xFF;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }

        [Fact]
        public void CheckNegativeUnset()
        {
            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.N);
        }
    }
}
