using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu.opcode
{
    public static class FlagChecker
    {
        public static void SetNegative(CPU cpu, byte result)
        {
            if ((result & 0x80) != 0)
            {
                cpu.SetFlag(PFlag.N);
            }
            else
            {
                cpu.ClearFlag(PFlag.N);
            }
        }

        public static void SetZero(CPU cpu, byte result)
        {
            if (result == 0)
            {
                cpu.SetFlag(PFlag.Z);
            }
            else
            {
                cpu.ClearFlag(PFlag.Z);
            }
        }

        public static void SetOverflow(CPU cpu, byte value1, byte value2, byte result)
        {
            if (((value1 ^ result) & (value2 ^ result) & 0x80) != 0)
            {
                cpu.SetFlag(PFlag.V);
            }
            else
            {
                cpu.ClearFlag(PFlag.V);
            }
        }

        public static void SetCarry(CPU cpu, int result)
        {
            if ((result & 0x100) != 0)
            {
                cpu.SetFlag(PFlag.C);
            }
            else
            {
                cpu.ClearFlag(PFlag.C);
            }
        }
    }
}
