using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu.addressign
{
    public class AbsoluteX : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            var lower = cpu.ReadNext();
            var higher = cpu.ReadNext();
            var address = (higher << 8) | lower;
            address += cpu.X;
            return cpu.Ram[address];
        }
    }
}
