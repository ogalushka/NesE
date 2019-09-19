using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class JSR : Operation
    {
        private readonly Absolute _addressing;

        public JSR(CPU cpu) : base(cpu)
        {
            _addressing = new Absolute();
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            var newAddress = _addressing.GetAddress(CPU);
            var oldAddress = CPU.PC - 1;

            var oldAddressHigh = (byte)(oldAddress >> 8);
            var oldAddressLow = (byte)oldAddress;

            CPU.PutOnStack(oldAddressHigh);
            CPU.PutOnStack(oldAddressLow);

            CPU.PC = newAddress;
        }
    }
}
