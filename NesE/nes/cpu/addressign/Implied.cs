namespace NesE.nes.cpu.addressign
{
    public class Implied : BaseAddressAccessor
    {
        public Implied(CPU cpu) : base(cpu)
        {
        }

        public override byte GetValue()
        {
            return 0;
        }

        public override void Reset()
        {
        }

        public override void SetValue(byte value)
        {
        }
    }
}
