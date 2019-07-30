using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class LSR : IOpCode
    {
        private ROR rol = new ROR();

        public void Execute(CPU cpu, IAddressing addresing)
        {
            cpu.ClearFlag(PFlag.C);
            rol.Execute(cpu, addresing);
        }
    }
}
