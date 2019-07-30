using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ROL : IOpCode
    {
        public void Execute(CPU cpu, IAddressing addresing)
        {
            var value = addresing.GetValue(cpu);

            var result = value << 1;
            result |= (int)(cpu.P & PFlag.C);
            cpu.A = (byte)result;
            FlagChecker.SetCarry(cpu, result);
            FlagChecker.SetNegative(cpu, cpu.A);
            FlagChecker.SetZero(cpu, cpu.A);
        }
    }
}
