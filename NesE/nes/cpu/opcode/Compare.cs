using System;
using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class Compare : IOpCode
    {
        private readonly Func<CPU, byte> _registerValue;

        public Compare(Func<CPU, byte> getter)
        {
            _registerValue = getter;
        }

        public void Execute(CPU cpu, IAddressing addresing)
        {
            var register = 0b1_0000_0000 | _registerValue(cpu);
            var memory = addresing.GetValue(cpu);

            var result = register - memory;
            FlagChecker.SetZero(cpu, (byte)result);
            FlagChecker.SetNegative(cpu, (byte)result);
            FlagChecker.SetCarry(cpu, result);
        }
    }
}
