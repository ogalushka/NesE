using NesE.nes.cpu.addressign;
using NesE.nes.cpu.opcode;

namespace NesE.nes.cpu
{
    public class Instruction
    {
        public readonly IOpCode Opcode;
        public readonly IAddressing Addressing;

        public Instruction(IOpCode opcode, IAddressing addressing)
        {
            Opcode = opcode;
            Addressing = addressing;
        }

        public void Execute(CPU cpu)
        {
            Opcode.Execute(cpu, Addressing);
        }
    }
}
