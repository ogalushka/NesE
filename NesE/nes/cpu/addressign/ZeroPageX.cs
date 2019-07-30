namespace NesE.nes.cpu.addressign
{
    public class ZeroPageX : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            byte address = (byte)(cpu.ReadNext() + cpu.X);
            return cpu.Ram[address];
        }
    }
}
