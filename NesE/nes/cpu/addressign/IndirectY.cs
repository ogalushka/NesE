namespace NesE.nes.cpu.addressign
{
    public class IndirectY : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            var lowerByte = cpu.ReadNext();
            byte higherByte = (byte)(lowerByte + 1);
            var lower = cpu.RAM.Get(lowerByte);
            var higher = cpu.RAM.Get(higherByte);
            var address = (higher << 8) | lower;
            address += cpu.Y;
            return (ushort)address;
        }
    }
}
