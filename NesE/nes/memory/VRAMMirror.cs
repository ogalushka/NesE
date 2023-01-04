using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.memory
{
    public class VRAMMirror : IMemory
    {
        public byte this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public byte Get(int index)
        {
            return 0;
        }

        public void Set(int index, byte value)
        {
        }
    }
}
