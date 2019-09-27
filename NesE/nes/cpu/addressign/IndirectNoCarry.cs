namespace NesE.nes.cpu.addressign
{
    public class IndirectNoCarry : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            byte lowByte = cpu.ReadNext();
            byte highByte = cpu.ReadNext();
            var target = (highByte << 8) | lowByte;

            var low = cpu.Ram.Get((highByte << 8) | lowByte);
            lowByte += 1;
            var high = cpu.Ram.Get((highByte << 8) | lowByte);
            return (ushort)((high << 8) | low);
        }
    }
}
