using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ROR : Operation
    {
        public ROR(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor addressAccessor)
        {
            var value = addressAccessor.GetValue();

            var result = value >> 1;
            result |= (int)(CPU.P & PFlag.C) << 7;
            addressAccessor.SetValue((byte)result);
            if ((value & 1) == 1)
            {
                CPU.SetFlag(PFlag.C);
            }
            else
            {
                CPU.ClearFlag(PFlag.C);
            }
            SetNegative((byte)result);
            SetZero((byte)result);
        }
    }
}
