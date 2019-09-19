namespace NesE.nes.cpu.addressign
{
    public class Status : BaseAddressAccessor
    {
        public Status(CPU cpu) : base(cpu)
        {
        }

        public override byte GetValue()
        {
            return (byte)(CPU.P | PFlag.B | PFlag._);
        }

        public override void Reset()
        {
        }

        public override void SetValue(byte value)
        {
            CPU.P = (PFlag)value;
            CPU.P |= PFlag._;
            CPU.P &= ~PFlag.B;
        }
    }
}
