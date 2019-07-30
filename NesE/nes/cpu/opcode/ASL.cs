using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ASL : IOpCode
    {
        private ROL rol = new ROL();

        public void Execute(CPU cpu, IAddressing addresing)
        {
            cpu.ClearFlag(PFlag.C);
            rol.Execute(cpu, addresing);
        }
    }
}
