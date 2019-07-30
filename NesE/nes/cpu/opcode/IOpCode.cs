using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public interface IOpCode
    {
        void Execute(CPU cpu, IAddressing addresing);
    }
}
