using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class Transfer : Operation
    {
        private readonly BaseAddressAccessor _transferFrom;
        private readonly bool _checkFlags;

        public Transfer(CPU cpu, BaseAddressAccessor transferFrom, bool checkFlags) : base(cpu)
        {
            _transferFrom = transferFrom;
            _checkFlags = checkFlags;
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            var value = _transferFrom.GetValue();
            accessor.SetValue(value);

            if (_checkFlags)
            {
                SetZero(value);
                SetNegative(value);
            }
        }
    }
}
