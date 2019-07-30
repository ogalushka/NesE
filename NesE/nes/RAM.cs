using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes
{
    public class RAM
    {
        private byte[] _memory = new byte[0x10000];

        public byte this[int i]
        {
            get { return _memory[i]; }
            set { _memory[i] = value; }
        }
    }
}
