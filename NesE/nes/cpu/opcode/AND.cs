using System;
using System.Collections.Generic;
using System.Text;
using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class AND : IOpCode
    {
        public void Execute(CPU cpu, IAddressing addresing)
        {
            byte value = addresing.GetValue(cpu);
            cpu.A = (byte)(value & cpu.A);
            FlagChecker.SetNegative(cpu, cpu.A);
            FlagChecker.SetZero(cpu, cpu.A);
        }
    }
}
