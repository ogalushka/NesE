namespace NesE.nes.cpu.addressign
{
    public class ZeroPageY : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            byte address = (byte)(cpu.ReadNext() + cpu.Y);
            return address;
        }
    }
}
