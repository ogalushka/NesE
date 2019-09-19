using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ADC : Operation
    {
        public ADC(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor addressing)
        {
            var value = addressing.GetValue();

            int carry = CPU.GetFlag(PFlag.C) ? 1 : 0;
            int result = value + CPU.A + carry;
            SetZero((byte) result);
            SetNegative((byte) result);
            SetOverflow(value, CPU.A, (byte)result);
            SetCarry(result);

            CPU.A = (byte)result;
        }
    }
}
