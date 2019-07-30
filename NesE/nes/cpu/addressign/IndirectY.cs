namespace NesE.nes.cpu.addressign
{
    public class IndirectY : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            var lowerByte = cpu.ReadNext();
            byte higherByte = (byte)(lowerByte + 1);
            var lower = cpu.Ram[lowerByte];
            var higher = cpu.Ram[higherByte];
            var address = (higher << 8) | lower;
            address += cpu.Y;
            return cpu.Ram[address];
        }
    }
}
