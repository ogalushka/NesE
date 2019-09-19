using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class FlagSet : Operation
    {
        private PFlag _flag;

        public FlagSet(CPU cpu, PFlag flag) : base(cpu)
        {
            _flag = flag;
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            CPU.SetFlag(_flag);
        }
    }
}
