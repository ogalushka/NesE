namespace NesE.nes.cpu.addressign
{
    public class IndirectNoCarry : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            byte lowByte = cpu.ReadNext();
            byte highByte = cpu.ReadNext();
            var target = (highByte << 8) | lowByte;

            var low = cpu.Ram[(highByte << 8) | lowByte];
            lowByte += 1;
            var high = cpu.Ram[(highByte << 8) | lowByte];
            return (ushort)((high << 8) | low);
        }
    }
}
