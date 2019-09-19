namespace NesE.nes.cpu.addressign
{
    public class XRegister : BaseAddressAccessor
    {
        public XRegister(CPU cpu) : base(cpu)
        {
        }

        public override byte GetValue()
        {
            return CPU.X;
        }

        public override void Reset()
        {
        }

        public override void SetValue(byte value)
        {
            CPU.X = value;
        }
    }
}
