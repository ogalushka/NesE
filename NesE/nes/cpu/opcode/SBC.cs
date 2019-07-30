using NesE.nes.cpu.addressign;
using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu.opcode
{
    public class SBC : IOpCode
    {
        private ADC _addWithCarry = new ADC();
        private InvertedValueAddressing _invertedValueAddressing = new InvertedValueAddressing();

        public void Execute(CPU cpu, IAddressing addressing)
        {
            _invertedValueAddressing.SetAddresing(addressing);
            _addWithCarry.Execute(cpu, _invertedValueAddressing);
        }

        private class InvertedValueAddressing : IAddressing
        {
            private IAddressing _addressing;

            public void SetAddresing(IAddressing addressing)
            {
                _addressing = addressing;
            }

            public byte GetValue(CPU cpu)
            {
                return (byte)~_addressing.GetValue(cpu);
            }
        }
    }
}
