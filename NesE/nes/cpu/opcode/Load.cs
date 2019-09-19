using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class Load : Operation
    {
        private readonly BaseAddressAccessor _targetRegsier;

        public Load(CPU cpu, BaseAddressAccessor targetRegister) : base(cpu)
        {
            _targetRegsier = targetRegister;
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            var value = accessor.GetValue();
            SetNegative(value);
            SetZero(value);
            _targetRegsier.SetValue(value);
        }
    }
}
