using NesE.nes.memory;

namespace NesE.nes
{
    public class TestRAM : IMemory
    {
        private byte[] _memory = new byte[0x10000];

        public byte this[int i]
        {
            get { return _memory[i]; }
            set { _memory[i] = value; }
        }
    }
}
