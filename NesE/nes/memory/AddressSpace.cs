namespace NesE.nes.memory
{
    public class AddressSpace : IMemory
    {
        private readonly ushort _mask;
        private readonly byte[] _memory;

        public AddressSpace(ushort mask, byte[] memory)
        {
            _mask = mask;
            _memory = memory;
        }

        public byte this[int i]
        {
            get { return _memory[i & _mask]; }
            set { _memory[i & _mask] = value; }
        }
    }
}
