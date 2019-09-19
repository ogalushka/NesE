using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class Compare : Operation
    {
        private readonly BaseAddressAccessor _registerValue;

        public Compare(CPU cpu, BaseAddressAccessor getter) : base(cpu)
        {
            _registerValue = getter;
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            var register = 0b1_0000_0000 | _registerValue.GetValue();
            var memory = addresing.GetValue();

            var result = register - memory;
            SetZero((byte)result);
            SetNegative((byte)result);
            SetCarry(result);
        }
    }
}
