namespace NesE.nes.cpu.addressign
{
    public abstract class BaseAddressAccessor
    {
        protected readonly CPU CPU;

        public BaseAddressAccessor(CPU cpu)
        {
            CPU = cpu;
        }

        public abstract void Reset();
        public abstract byte GetValue();
        public abstract void SetValue(byte value);
    }
}
