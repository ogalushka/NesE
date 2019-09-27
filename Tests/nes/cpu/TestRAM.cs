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

        public byte Get(int index)
        {
            return _memory[index];
        }

        public void Set(int index, byte value)
        {
            _memory[index] = value;
        }
    }
}
