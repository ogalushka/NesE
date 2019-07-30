using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu.addressign
{
    public abstract class BaseAddressing : IAddressing
    {
        protected ushort Address = 0;
        protected bool isAddressRetrived = false;

        public void Reset()
        {
            Address = 0;
            isAddressRetrived = false;
        }
        public abstract byte GetValue(CPU cpu);

        protected ushort GetAddress(CPU cpu)
        {
            if (!isAddressRetrived)
            {
                Address = RetriveAddress(cpu);
                isAddressRetrived = true;
            }
            return Address;
        }

        protected abstract ushort RetriveAddress(CPU cpu);
    }
}
