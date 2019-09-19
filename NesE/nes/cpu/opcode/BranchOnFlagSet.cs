using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class BranchOnFlagSet : Operation
    {
        private readonly PFlag _flag;

        public BranchOnFlagSet(CPU cpu, PFlag flag) : base(cpu)
        {
            _flag = flag;
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            var value = (sbyte)addresing.GetValue();
            if (CPU.GetFlag(_flag))
            {
                CPU.PC = (ushort)(CPU.PC + value);
            }
        }
    }
}
