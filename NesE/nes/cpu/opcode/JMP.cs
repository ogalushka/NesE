using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class JMP : Operation
    {
        private IAddressResolver _addressResolver;

        public JMP(CPU cpu, IAddressResolver addressResolver) : base(cpu)
        {
            _addressResolver = addressResolver;
        }

        public override void Execute(BaseAddressAccessor addressing)
        {
            CPU.PC = _addressResolver.GetAddress(CPU);
        }
    }
}
