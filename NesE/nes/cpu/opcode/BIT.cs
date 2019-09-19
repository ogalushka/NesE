using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class BIT : Operation
    {
        public BIT(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            var value = addresing.GetValue();
            var result = (byte)(CPU.A & value);

            SetNegative(value);
            SetZero(result);
            if ((value & 0b0100_0000) != 0)
            {
                CPU.SetFlag(PFlag.V);
            }
            else
            {
                CPU.ClearFlag(PFlag.V);
            }
        }
    }
}
