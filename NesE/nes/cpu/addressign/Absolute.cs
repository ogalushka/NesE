namespace NesE.nes.cpu.addressign
{
    public class Absolute : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            var lower = cpu.ReadNext();
            var higher = cpu.ReadNext();
            var address = (higher << 8) | lower;
            return cpu.Ram[address];
        }
    }
}
