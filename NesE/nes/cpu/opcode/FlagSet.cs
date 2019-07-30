using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class FlagSet : IOpCode
    {
        private PFlag _flag;

        public FlagSet(PFlag flag)
        {
            _flag = flag;
        }

        public void Execute(CPU cpu, IAddressing addresing)
        {
            cpu.SetFlag(_flag);
        }
    }
}
