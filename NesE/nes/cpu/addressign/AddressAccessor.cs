namespace NesE.nes.cpu.addressign
{
    public class AddressAccessor : BaseAddressAccessor
    {
        private readonly IAddressResolver _addressResolver;

        public AddressAccessor(CPU cpu, IAddressResolver addressResolver) : base(cpu)
        {
            _addressResolver = addressResolver;
        }

        protected ushort Address = 0;
        protected bool isAddressRetrived = false;

        public override void Reset()
        {
            isAddressRetrived = false;
        }

        public override byte GetValue()
        {
            return CPU.Ram.Get(GetAddress());
        }

        public override void SetValue(byte value)
        {
            CPU.Ram.Set(GetAddress(), value);
        }

        private ushort GetAddress()
        {
            if (!isAddressRetrived)
            {
                Address = _addressResolver.GetAddress(CPU);
                isAddressRetrived = true;
            }
            return Address;
        }
    }
}
