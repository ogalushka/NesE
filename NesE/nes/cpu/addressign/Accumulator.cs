namespace NesE.nes.cpu.addressign
{
    public class Accumulator : BaseAddressAccessor
    {
        public Accumulator(CPU cpu) : base(cpu)
        {
        }

        public override byte GetValue()
        {
            return CPU.A;
        }

        public override void Reset()
        {
        }

        public override void SetValue(byte value)
        {
            CPU.A = value;
        }
    }
}
