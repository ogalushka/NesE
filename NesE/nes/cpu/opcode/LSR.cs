using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class LSR : Operation
    {
        private ROR rol;

        public LSR(CPU cpu) : base(cpu)
        {
            rol = new ROR(cpu);
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            CPU.ClearFlag(PFlag.C);
            rol.Execute(addresing);
        }
    }
}
