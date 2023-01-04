using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class BRK : Operation
    {
        public BRK(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor addresing)
        {
            var lowerPC = CPU.RAM.Get(0xFFFE);
            var higherPC = CPU.RAM.Get(0xFFFF);

            ushort newPC = (ushort)((higherPC << 8) | lowerPC);

            CPU.PutOnStack((byte)(CPU.PC >> 8));
            CPU.PutOnStack((byte)CPU.PC);
            CPU.PutOnStack((byte)(CPU.P | PFlag.B | PFlag._));

            CPU.PC = newPC;
        }
    }
}
