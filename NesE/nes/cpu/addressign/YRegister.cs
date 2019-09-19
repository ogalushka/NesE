namespace NesE.nes.cpu.addressign
{
    public class YRegister : BaseAddressAccessor
    {
        public YRegister(CPU cpu) : base(cpu)
        {
        }

        public override byte GetValue()
        {
            return CPU.Y;
        }

        public override void Reset()
        {
        }

        public override void SetValue(byte value)
        {
            CPU.Y = value;
        }
    }
}
