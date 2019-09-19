namespace NesE.nes.cpu.addressign
{
    public class ZeroPageX : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            byte address = (byte)(cpu.ReadNext() + cpu.X);
            return address;
        }
    }
}
