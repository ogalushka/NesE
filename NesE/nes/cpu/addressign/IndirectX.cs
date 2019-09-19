namespace NesE.nes.cpu.addressign
{
    public class IndirectX : IAddressResolver
    {
        public ushort GetAddress(CPU cpu)
        {
            byte lowByte = cpu.ReadNext();
            lowByte = (byte)(lowByte + cpu.X);
            byte highByte = (byte)(lowByte + 1);

            var low = cpu.Ram[lowByte];
            var high = cpu.Ram[highByte];
            var target = (high << 8) | low;
            return (ushort)target;
        }
    }
}
