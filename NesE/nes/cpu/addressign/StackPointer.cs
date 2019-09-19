namespace NesE.nes.cpu.addressign
{
    public class StackPointer : BaseAddressAccessor
    {
        public StackPointer(CPU cpu) : base(cpu)
        {
        }

        public override byte GetValue()
        {
            return CPU.S;
        }

        public override void Reset()
        {
        }

        public override void SetValue(byte value)
        {
            CPU.S = value;
        }
    }
}
