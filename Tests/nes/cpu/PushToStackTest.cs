using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class PushToStackTest : BaseCPUTest
    {
        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.PHA_IMP, (Action<byte, CPU>)MemorySetter.Accumulator };
                yield return new object[] { OP.PHP_IMP, (Action<byte, CPU>)MemorySetter.StatusReg };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void ShouldPushAccumulatorToStack()
        {
            CPU.RAM[0] = OP.PHA_IMP;
            const byte Expected = 0x32;
            CPU.A = Expected;

            CPU.Step();

            Assert.Equal(Expected, CPU.RAM[0x100 | (CPU.S + 1)]);
        }

        [Fact]
        public void ShouldPushStatusToStackSettingBits()
        {
            CPU.RAM[0] = OP.PHP_IMP;
            const byte BitsToSet = 0b0011_0000;
            const byte StatusRegister = 0b0100_1111;
            CPU.P = (PFlag)StatusRegister;

            CPU.Step();

            Assert.Equal(StatusRegister | BitsToSet, CPU.RAM[0x100 | (CPU.S + 1)]);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldChangeStack(byte op, Action<byte, CPU> registerSetter)
        {
            CPU.RAM[0] = op;
            var Expected = CPU.S - 1;
            registerSetter(0x32, CPU);

            CPU.Step();

            Assert.Equal(Expected, CPU.S);
        }
    }
}
