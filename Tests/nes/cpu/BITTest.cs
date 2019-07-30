using NesE.nes;
using NesE.nes.cpu;
using NesE.nes.cpu.opcode;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests.nes.cpu
{
    public class BITTest
    {
        public RAM _ram;
        public CPU _cpu;

        public BITTest()
        {
            _ram = new RAM();
            _cpu = new CPU(_ram);
        }

        public class BITTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { OP.BIT_ZEP, (Action<byte, CPU>)MemorySetter.ZeroPage };
                yield return new object[] { OP.BIT_ABS, (Action<byte, CPU>)MemorySetter.Absolute };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(BITTestData))]
        public void CheckZeroFlagSet(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;
            setValue(0, _cpu);

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(BITTestData))]
        public void CheckZeroFlagCleared(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;
            _cpu.A = 1;
            setValue(1, _cpu);

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.Z);
        }

        [Theory]
        [ClassData(typeof(BITTestData))]
        public void CheckOverflowSet(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;
            setValue(0b01000000, _cpu);

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.V);
        }

        [Theory]
        [ClassData(typeof(BITTestData))]
        public void CheckOverflowClear(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;
            setValue(0b10111111, _cpu);

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.V);
        }

        [Theory]
        [ClassData(typeof(BITTestData))]
        public void CheckNegativeSet(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;
            setValue(0b10000000, _cpu);

            _cpu.Step();

            FlagAssert.Set(_cpu, PFlag.N);
        }

        [Theory]
        [ClassData(typeof(BITTestData))]
        public void CheckNegativeClear(byte op, Action<byte, CPU> setValue)
        {
            _ram[0] = op;
            setValue(0b01111111, _cpu);

            _cpu.Step();

            FlagAssert.Cleared(_cpu, PFlag.N);
        }
    }
}
