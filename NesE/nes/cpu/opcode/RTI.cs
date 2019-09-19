using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class RTI : Operation
    {
        private readonly PullRegister _pullStatus;
        private readonly Status _registerAccessor;

        public RTI(CPU cpu) : base(cpu)
        {
            _pullStatus = new PullRegister(CPU, false);
            _registerAccessor = new Status(CPU);
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            _pullStatus.Execute(_registerAccessor);

            var addressLow = CPU.PullFromStack();
            var addressHigh = CPU.PullFromStack();

            var targetAddress = (addressHigh << 8) | addressLow;
            
            CPU.PC = (ushort)targetAddress;
        }
    }
}
