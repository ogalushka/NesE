using NesE.nes.cpu.addressign;
using NesE.nes.cpu.opcode;

namespace NesE.nes.cpu
{
    public class Instruction
    {
        private readonly Operation _opcode;
        private readonly BaseAddressAccessor _addressing;

        public Instruction(Operation opcode, BaseAddressAccessor addressing)
        {
            _opcode = opcode;
            _addressing = addressing;
        }

        public void Execute()
        {
            _addressing.Reset();
            _opcode.Execute(_addressing);
        }
    }
}
