using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu
{
    [Flags]
    public enum PFlag
    {
        N = 0b1000_0000, //negative
        V = 0b0100_0000, //overflow
        _ = 0b0010_0000,
        B = 0b0001_0000, //break
        D = 0b0000_1000, //decimal mode
        I = 0b0000_0100, //interupt disable
        Z = 0b0000_0010, //zero
        C = 0b0000_0001  //carry
    }
}
