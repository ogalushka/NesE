using NesE.nes.cpu.addressign;
using NesE.nes.cpu.opcode;
using System.Collections.Generic;

namespace NesE.nes.cpu
{
    public class CPU
    {
        public readonly RAM Ram;

        public byte A;
        public byte X;
        public byte Y;

        public ushort S;
        public PFlag P;

        public ushort PC;

        public CPU(RAM ram)
        {
            Ram = ram;
            Reset();
        }

        public void Step()
        {
            Instructions.Get(ReadNext()).Execute(this);
        }

        public void Reset()
        {
            S = 0x01ff;
            PC = (ushort)(Ram[0xFFFC] | Ram[0xFFFD] << 8);
            P |= PFlag.I | PFlag._;
        }

        public byte ReadNext()
        {
            return Ram[PC++];
        }

        public void PutOnStack(byte value)
        {
            Ram[S] = value;
            S--;
        }

        public byte PullFromStack()
        {
            S++;
            return Ram[S];
        }

        public void SetFlag(PFlag flag)
        {
            P |= flag;
        }

        public void ClearFlag(PFlag flag)
        {
            P &= ~flag;
        }

        public bool GetFlag(PFlag flag)
        {
            return (P & flag) != 0;
        }
    }
}
