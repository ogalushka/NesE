using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class SBC : Operation
    {
        private readonly ADC _addWithCarry;
        private readonly InvertedValueAddressing _invertedValueAddressing;

        public SBC(CPU cpu) : base(cpu)
        {
            _addWithCarry = new ADC(cpu);
            _invertedValueAddressing = new InvertedValueAddressing(cpu);
        }

        public override void Execute(BaseAddressAccessor addressing)
        {
            _invertedValueAddressing.SetAddressing(addressing);
            _addWithCarry.Execute(_invertedValueAddressing);
        }

        private class InvertedValueAddressing : BaseAddressAccessor
        {
            private BaseAddressAccessor _addressing;

            public InvertedValueAddressing(CPU cpu) : base(cpu)
            {
            }

            public void SetAddressing(BaseAddressAccessor addressing)
            {
                _addressing = addressing;
            }

            public override byte GetValue()
            {
                return (byte)~_addressing.GetValue();
            }

            public override void Reset()
            {
                _addressing.Reset();
            }

            public override void SetValue(byte value)
            {
                _addressing.SetValue(value);
            }
        }
    }
}
