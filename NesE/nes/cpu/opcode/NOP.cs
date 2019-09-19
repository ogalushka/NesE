using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class NOP : Operation
    {
        public NOP(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
        }
    }
}
