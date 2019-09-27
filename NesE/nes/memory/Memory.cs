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

        public void AddAddressSpace(int selectorBits, ushort mask, byte[] memory)
        {
            if (_addressSpaces.ContainsKey(selectorBits))
            {
                _addressSpaces[selectorBits] = new AddressSpace(mask, memory);
            }
            else
            {
                _addressSpaces.Add(selectorBits, new AddressSpace(mask, memory));
            }
        }

        public void AddAddressSpace(int selectorBits, IMemory memory)
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
            _addressSpaces[(index >> _bankAddresShift)].Set(index, value);
        }

        public byte Get(int index)
        {
            return _addressSpaces[(index >> _bankAddresShift)].Get(index);
        }

        public byte this[int index] {
            get { return _addressSpaces[(index >> _bankAddresShift)].Get(index); }
            set { _addressSpaces[(index >> _bankAddresShift)].Set(index, value); }
        }
    }
}
