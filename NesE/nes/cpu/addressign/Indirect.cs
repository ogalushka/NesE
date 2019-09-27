namespace NesE.nes.cpu.addressign
{
    public class Indirect : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            byte lowByte = cpu.ReadNext();
            byte highByte = cpu.ReadNext();
            var target = (highByte << 8) | lowByte;

            var low = cpu.Ram.Get(target);
            var high = cpu.Ram.Get(target + 1);
            return (ushort)((high << 8) | low);
        }
    }
}
