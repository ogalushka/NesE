using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class BranchOnFlagClear : IOpCode
    {
        private readonly PFlag _flag;

        public BranchOnFlagClear(PFlag flag)
        {
            _flag = flag;
        }

        public void Execute(CPU cpu, IAddressing addresing)
        {
            var value = (sbyte)addresing.GetValue(cpu);
            if (!cpu.GetFlag(_flag))
            {
                cpu.PC = (ushort)(cpu.PC + value);
            }
        }
    }
}
