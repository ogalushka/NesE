namespace NesE.nes.cpu.addressign
{
    public class Immediate : BaseAddressAccessor
    {
        public Immediate(CPU cpu) : base(cpu)
        {
        }

        public override byte GetValue()
        {
            return CPU.ReadNext();
        }

        public override void Reset()
        {
        }

        public override void SetValue(byte value)
        {
            throw new WriteToReadOnlyAccessorException();
        }
    }
}
