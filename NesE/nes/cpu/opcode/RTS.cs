using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class RTS : Operation
    {
        public RTS(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            var addressLow = CPU.PullFromStack();
            var addressHigh = CPU.PullFromStack();

            var targetAddress = (addressHigh << 8) | addressLow;

            targetAddress++;
            CPU.PC = (ushort)targetAddress;
        }
    }
}
