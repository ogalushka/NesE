using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class FlagClear : Operation
    {
        private readonly PFlag _flag;

        public FlagClear(CPU cpu, PFlag flag) : base(cpu)
        {
            _flag = flag;
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            CPU.ClearFlag(_flag);
        }
    }
}
