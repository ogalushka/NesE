namespace NesE.nes.cpu.addressign
{
    public interface IAddressResolver
    {
        ushort GetAddress(CPU cpu);
    }
}
