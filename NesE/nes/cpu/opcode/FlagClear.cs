using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class FlagClear : IOpCode
    {
        private readonly PFlag _flag;

        public FlagClear(PFlag flag)
        {
            _flag = flag;
        }

        public void Execute(CPU cpu, IAddressing addresing)
        {
            cpu.ClearFlag(_flag);
        }
    }
}
