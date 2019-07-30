using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ORA : IOpCode
    {
        public void Execute(CPU cpu, IAddressing addresing)
        {
            byte value = addresing.GetValue(cpu);
            cpu.A = (byte)(value | cpu.A);
            FlagChecker.SetNegative(cpu, cpu.A);
            FlagChecker.SetZero(cpu, cpu.A);
        }
    }
}
