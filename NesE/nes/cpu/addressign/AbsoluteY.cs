namespace NesE.nes.cpu.addressign
{
    public class AbsoluteY : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            var lower = cpu.ReadNext();
            var higher = cpu.ReadNext();
            var address = (higher << 8) | lower;
            address += cpu.Y;
            return (ushort)address;
        }
    }
}
