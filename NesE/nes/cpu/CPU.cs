using NesE.nes.memory;
using System;
using System.Diagnostics;

namespace NesE.nes.cpu
{
    public class CPU
    {
        public bool IRQ = false;
        public bool NMI = false;

        public byte A;
        public byte X;
        public byte Y;

        public byte S;
        public PFlag P;

        public ushort PC;

        public readonly IMemory Ram;
        private readonly Instructions _instructions;

        public CPU(IMemory ram)
        {
            Ram = ram;
            _instructions = new Instructions(this);
            Reset();
        }

        public void Step()
        {
            if (IRQ && !GetFlag(PFlag.I))
            {
                _instructions.IRQ.Execute();
            }
            else if (NMI)
            {
                _instructions.UnmaskedIRQ.Execute();
            }
            else
            {
                _instructions.Get(ReadNext()).Execute();
            }
        }

        public void Reset()
        {
            S = 0xff;
            PC = (ushort)(Ram[0xFFFC] | Ram[0xFFFD] << 8);
            P |= PFlag.I | PFlag._;
        }

        public byte ReadNext()
        {
            return Ram[PC++];
        }

        public void PutOnStack(byte value)
        {
            Ram[0x100 | S--] = value;
        }

        public byte PullFromStack() 
        {
            return Ram[0x100 | ++S];
        }

        public void SetFlag(PFlag flag)
        {
            if (flag == PFlag.B || flag == PFlag._)
            {
                throw new Exception($"Can't set flag {flag}. Not a real flag");
            }
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
