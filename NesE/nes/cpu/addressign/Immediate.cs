namespace NesE.nes.cpu.addressign
{
    public class Immediate : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            return cpu.ReadNext();
        }
    }
}
