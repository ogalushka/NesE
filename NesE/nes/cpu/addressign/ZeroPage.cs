namespace NesE.nes.cpu.addressign
{
    public class ZeroPage : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            return cpu.ReadNext();
        }
    }
}
