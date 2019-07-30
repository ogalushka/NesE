using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ADC : IOpCode
    {
        public void Execute(CPU cpu, IAddressing addressing)
        {
            var value = addressing.GetValue(cpu);

            int carry = cpu.GetFlag(PFlag.C) ? 1 : 0;
            int result = value + cpu.A + carry;
            FlagChecker.SetZero(cpu, (byte) result);
            FlagChecker.SetNegative(cpu, (byte) result);
            FlagChecker.SetOverflow(cpu, value, cpu.A, (byte)result);
            FlagChecker.SetCarry(cpu, result);

            cpu.A = (byte)result;
        }
    }
}
