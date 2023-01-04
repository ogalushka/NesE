using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.ppu
{
    public struct RenderUnit
    {
        public int X;
        public int Y;
        public byte Attribute;
        public byte LowPattern;
        public byte HighPattern;
    }
}
