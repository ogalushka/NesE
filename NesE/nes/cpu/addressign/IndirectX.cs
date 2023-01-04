namespace NesE.nes.cpu.addressign
{
    public class IndirectX : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            byte lowByte = cpu.ReadNext();
            lowByte = (byte)(lowByte + cpu.X);
            byte highByte = (byte)(lowByte + 1);

            var low = cpu.RAM.Get(lowByte);
            var high = cpu.RAM.Get(highByte);
            var target = (high << 8) | low;
            return (ushort)target;
        }
    }
}
