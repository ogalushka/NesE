using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class PullRegister : Operation
    {
        private readonly bool _setFlags;

        public PullRegister(CPU cpu, bool setFlags) : base(cpu)
        {
            _setFlags = setFlags;
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            var value = CPU.PullFromStack();
            if (_setFlags)
            {
                SetZero(value);
                SetNegative(value);
            }
            accessor.SetValue(value);
        }
    }
}
