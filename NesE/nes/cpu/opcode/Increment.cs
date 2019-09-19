using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class Increment : Operation
    {
        public Increment(CPU cpu) : base(cpu)
        {
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            var value = accessor.GetValue();
            value++;
            accessor.SetValue(value);
            SetZero(value);
            SetNegative(value);
        }
    }
}
