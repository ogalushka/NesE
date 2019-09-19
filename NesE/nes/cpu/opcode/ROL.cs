using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ROL : Operation
    {
        public ROL(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor addressAccessor)
        {
            var value = addressAccessor.GetValue();

            var result = value << 1;
            result |= (int)(CPU.P & PFlag.C);
            addressAccessor.SetValue((byte)result);
            SetCarry(result);
            SetNegative((byte)result);
            SetZero((byte)result);
        }
    }
}
