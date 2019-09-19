using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class PushRegister : Operation
    {
        public PushRegister(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            CPU.PutOnStack(accessor.GetValue());
        }
    }
}
