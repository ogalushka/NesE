using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class BRK : IOpCode
    {
        public void Execute(CPU cpu, IAddressing addresing)
        {
            cpu.SetFlag(PFlag.B);
            var lowerPC = cpu.Ram[0xFFFE];
            var higherPC = cpu.Ram[0xFFFF];

            ushort newPC = (ushort)((higherPC << 8) | lowerPC);

            cpu.PutOnStack((byte)(cpu.PC >> 8));
            cpu.PutOnStack((byte)cpu.PC);
            cpu.PutOnStack((byte)cpu.P);

            cpu.PC = newPC;
            
        }
    }
}
