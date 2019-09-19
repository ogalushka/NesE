using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class ASL : Operation
    {
        private readonly ROL rol;

        public ASL(CPU cpu) : base(cpu)
        {
            rol = new ROL(cpu);
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            CPU.ClearFlag(PFlag.C);
            rol.Execute(addresing);
        }
    }
}
