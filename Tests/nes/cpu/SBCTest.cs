using NesE.nes;
using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class SBCTest : BaseCPUTest
    {
        public TestRAM _ram;

        public SBCTest()
        {
            _ram = CPU.RAM as TestRAM;
            _ram[0] = OP.SBC_IMM;
        }

        public class ADCTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.SBC_IMM, (Action<byte, CPU>)MemorySetter.Immediate };
                yield return new object[] { OP.SBC_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage };
                yield return new object[] { OP.SBC_ZPX, (Action<byte, CPU>)MemorySetter.ZeroPageX };
                yield return new object[] { OP.SBC_ABS, (Action<byte, CPU>)MemorySetter.Absolute };
                yield return new object[] { OP.SBC_ABX, (Action<byte, CPU>)MemorySetter.AbsoluteX };
                yield return new object[] { OP.SBC_ABY, (Action<byte, CPU>)MemorySetter.AbsoluteY };
                yield return new object[] { OP.SBC_IDX, (Action<byte, CPU>)MemorySetter.IndirectX };
                yield return new object[] { OP.SBC_IDY, (Action<byte, CPU>)MemorySetter.IndirectY };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ADCTestData))]
        public void ShouldAdd(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;

            const byte ExpectedResult = 0x0F;
            setValue(0x10, CPU);
            CPU.A = 0x20;

            CPU.Step();

            Assert.Equal(ExpectedResult, CPU.A);
        }

        [Fact]
        public void ShouldSubImmediate()
        {
            const byte ExpectedResult = 0x0F;
            CPU.A = 0x20;
            _ram[1] = 0x10;

            CPU.Step();

            Assert.Equal(ExpectedResult, CPU.A);
        }

        [Fact]
        public void ShouldUseCarry()
        {
            const byte ExpectedResult = 0x10;
            CPU.SetFlag(PFlag.C);
            CPU.A = 0x20;
            _ram[1] = 0x10;

            CPU.Step();

            Assert.Equal(ExpectedResult, CPU.A);
        }

        [Fact]
        public void ShouldSubOverflow()
        {
            const byte ExpectedResult = 0xFF;
            CPU.SetFlag(PFlag.C);
            _ram[1] = 0x01;

            CPU.Step();

            Assert.Equal(ExpectedResult, CPU.A);
        }

        [Fact]
        public void CheckZeroFlag()
        {
            CPU.SetFlag(PFlag.C);
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
            CPU.SetFlag(PFlag.C);

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.C);
        }

        [Fact]
        public void CheckCarryUnSet()
        {
            _ram[1] = 1;
            CPU.A = 0;
            CPU.SetFlag(PFlag.C);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.C);
        }

        [Theory]
        [InlineData(0x80, 0x01)]
        public void CheckOverflowSet(byte b1, byte b2)
        {
            CPU.A = b1;
            _ram[1] = b2;
            CPU.SetFlag(PFlag.C);

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.V);
        }

        [Theory]
        [InlineData(0x02, 0x01)]
        public void CheckOverflowUnSet(byte b1, byte b2)
        {
            CPU.A = b1;
            _ram[1] = b2;
            CPU.SetFlag(PFlag.C);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.V);
        }

        [Fact]
        public void CheckNegativeSet()
        {
            _ram[1] = 0x0F;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, PFlag.N);
        }

        [Fact]
        public void CheckNegativeUnset()
        {
            _ram[1] = 0x00;
            CPU.A = 0x7F;

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, PFlag.N);
        }
    }
}
