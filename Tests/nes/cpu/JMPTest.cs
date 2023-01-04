using NesE.nes.cpu;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class JMPTest : BaseCPUTest
    {
        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.JMP_ABS, (Action<ushort, CPU>)MemorySetter.ImmidiateTwoByte };
                yield return new object[] { OP.JMP_IND, (Action<ushort, CPU>)MemorySetter.Indirect };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void ShouldJump(byte op, Action<ushort, CPU> setter)
        {
            CPU.RAM[0] = op;
            ushort Expected = 0x1234;
            setter(Expected, CPU);

            CPU.Step();

            Assert.Equal(Expected, CPU.PC);
        }
    }
}
