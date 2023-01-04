using NesE.nes.memory;
using System;

namespace NesE.nes.cpu
{
    public class CPU
    {
        public readonly Interupts Interupts;

        public byte A;
        public byte X;
        public byte Y;

        public byte S;
        public PFlag P;

        public ushort PC;

        public readonly IMemory RAM;
        private readonly Instructions _instructions;

        public CPU(IMemory ram, Interupts interupts)
        {
            RAM = ram;
            Interupts = interupts;
            _instructions = new Instructions(this);
            Reset();
        }

        public void Step()
        {
            if (Interupts.IRQ && !GetFlag(PFlag.I))
            {
                _instructions.IRQ.Execute();
            }
            else if (Interupts.NMI)
            {
                // TODO break infinite cycle reset NMI STATUS https://wiki.nesdev.com/w/index.php/NMI
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
            PC = (ushort)(RAM.Get(0xFFFC) | RAM.Get(0xFFFD) << 8);
            P |= PFlag.I | PFlag._;
        }

        public byte ReadNext()
        {
            return RAM.Get(PC++);
        }

        public void PutOnStack(byte value)
        {
            RAM.Set(0x100 | S--, value);
        }

        public byte PullFromStack() 
        {
            return RAM.Get(0x100 | ++S);
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
