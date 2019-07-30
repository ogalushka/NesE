using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class BIT : IOpCode
    {
        public void Execute(CPU cpu, IAddressing addresing)
        {
            var value = addresing.GetValue(cpu);
            var result = (byte)(cpu.A & value);

            FlagChecker.SetNegative(cpu, value);
            FlagChecker.SetZero(cpu, result);
            if ((value & 0b0100_0000) != 0)
            {
                cpu.SetFlag(PFlag.V);
            }
            else
            {
                cpu.ClearFlag(PFlag.V);
            }
        }
    }
}
