namespace NesE.nes.cpu.addressign
{
    public class Absolute : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            var lower = cpu.ReadNext();
            var higher = cpu.ReadNext();
            var address = (higher << 8) | lower;
            return (ushort)address;
        }
    }
}
