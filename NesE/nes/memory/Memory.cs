using System.Collections.Generic;

namespace NesE.nes.memory
{
    public class Memory : IMemory
    {
        private int _bankAddresShift;
        private Dictionary<int, IMemory> _addressSpaces;

        public Memory(int bankAddresShift)
        {
            _bankAddresShift = bankAddresShift;
            _addressSpaces = new Dictionary<int, IMemory>();
        }

        public void AddAddressSpace(ushort selectorBits, ushort mask, byte[] memory)
        {
            AddAddressSpace(selectorBits, new AddressSpace(mask, memory));
        }

        public void AddAddressSpace(ushort selectorBits, IMemory memory)
        {
            if (_addressSpaces.ContainsKey(selectorBits))
            {
                _addressSpaces[selectorBits] = memory;
            }
            else
            {
                _addressSpaces.Add(selectorBits, memory);
            }
        }

        public void Set(int index, byte value)
        {
            var key = index >> _bankAddresShift;
            _addressSpaces[key].Set(index, value);
        }

        public byte Get(int index)
        {
            var key = index >> _bankAddresShift;
            return _addressSpaces[key].Get(index);
        }

        public byte this[int index] {
            get {
                int key = index >> _bankAddresShift;
                return _addressSpaces[key].Get(index);
            }
            set {
                int key = index >> _bankAddresShift;
                _addressSpaces[key].Set(index, value);
            }
        }
    }
}
