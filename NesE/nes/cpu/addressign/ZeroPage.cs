using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu.addressign
{
    public class ZeroPage : IAddressing
    {
        public byte GetValue(CPU cpu)
        {
            return cpu.Ram[cpu.ReadNext()];
        }
    }
}
