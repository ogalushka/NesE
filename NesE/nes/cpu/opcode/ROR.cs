using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ROR : IOpCode
    {
        public void Execute(CPU cpu, IAddressing addresing)
        {
            var value = addresing.GetValue(cpu);

            var result = value >> 1;
            result |= (int)(cpu.P & PFlag.C) << 7;
            cpu.A = (byte)result;
            if ((value & 1) == 1)
            {
                cpu.SetFlag(PFlag.C);
            }
            else
            {
                cpu.ClearFlag(PFlag.C);
            }
            FlagChecker.SetNegative(cpu, cpu.A);
            FlagChecker.SetZero(cpu, cpu.A);
        }
    }
}
