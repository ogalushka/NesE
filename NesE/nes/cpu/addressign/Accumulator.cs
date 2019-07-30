namespace NesE.nes.cpu.addressign
{
    public class Accumulator : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            return cpu.A;
        }
    }
}
