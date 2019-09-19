using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public abstract class Operation
    {
        protected readonly CPU CPU;

        public Operation(CPU cpu)
        {
            CPU = cpu;
        }

        public abstract void Execute(BaseAddressAccessor accessor);

        protected void SetNegative(byte result)
        {
            if ((result & 0x80) != 0)
            {
                CPU.SetFlag(PFlag.N);
            }
            else
            {
                CPU.ClearFlag(PFlag.N);
            }
        }

        protected void SetZero(byte result)
        {
            if (result == 0)
            {
                CPU.SetFlag(PFlag.Z);
            }
            else
            {
                CPU.ClearFlag(PFlag.Z);
            }
        }

        protected void SetOverflow(byte value1, byte value2, byte result)
        {
            if (((value1 ^ result) & (value2 ^ result) & 0x80) != 0)
            {
                CPU.SetFlag(PFlag.V);
            }
            else
            {
                CPU.ClearFlag(PFlag.V);
            }
        }

        protected void SetCarry(int result)
        {
            if ((result & 0x100) != 0)
            {
                CPU.SetFlag(PFlag.C);
            }
            else
            {
                CPU.ClearFlag(PFlag.C);
            }
        }
    }
}
