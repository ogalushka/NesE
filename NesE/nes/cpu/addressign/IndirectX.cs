using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu.addressign
{
    public class IndirectX : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            byte lowByte = cpu.ReadNext();
            lowByte = (byte)(lowByte + cpu.X);
            byte highByte = (byte)(lowByte + 1);

            var low = cpu.Ram[lowByte];
            var high = cpu.Ram[highByte];
            var target = (high << 8) | low;
            return cpu.Ram[target];
        }
    }
}
