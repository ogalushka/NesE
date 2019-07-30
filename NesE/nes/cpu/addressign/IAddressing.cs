namespace NesE.nes.cpu.addressign
{
    public interface IAddressing
    {
        void Reset();
        byte GetValue(CPU cpu);
    }
}
