using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class EOR : Operation
    {
        public EOR(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            byte value = addresing.GetValue();
            CPU.A = (byte)(value ^ CPU.A);
            SetNegative(CPU.A);
            SetZero(CPU.A);
        }
    }
}
