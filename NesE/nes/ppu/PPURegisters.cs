using NesE.nes.cpu;
using NesE.nes.memory;
using System;
using System.Collections.Generic;

namespace NesE.nes.ppu
{
    public class PPURegisters : IMemory
    {
        public const ushort AddressMask = 0b0000_0000_0000_0111;
        public const int CTRL = 0;
        public const int MASK = 1;
        public const int STATUS = 2;
        public const int OAMADDR = 3;
        public const int OAMDATA = 4;
        public const int SCROLL = 5;
        public const int ADDR = 6;
        public const int DATA = 7;

        private readonly Func<byte>[] GetMethods = new Func<byte>[8];
        private readonly Action<byte>[] SetMethods = new Action<byte>[8];

        private readonly byte[] _oam;
        private readonly IMemory _vram;
        public readonly Interupts _interupts;

        private byte _status;
        public byte Controll;
        public byte Mask;
        public byte OAMAddress;
        public ushort Scroll;
        public ushort Address;

        public byte Status { get => _status;
            set {
                _status = value;
                CheckNMI();
            }
        }

        public PPURegisters(byte[] oam, IMemory vram, Interupts interupts)
        {
            _oam = oam;
            _vram = vram;
            _interupts = interupts;
            GetMethods[STATUS] = ReadStatus;
            GetMethods[OAMDATA] = ReadOamData;
            GetMethods[DATA] = ReadVRAM;

            SetMethods[CTRL] = WriteControll;
            SetMethods[MASK] = WriteMask;
            SetMethods[OAMADDR] = WriteOAMAddress;
            SetMethods[OAMDATA] = WriteOamData;
            SetMethods[SCROLL] = WriteScroll;
            SetMethods[ADDR] = WriteAddress;
            SetMethods[DATA] = WriteVRAM;
        }

        public byte Get(int index)
        {
            var registerIndex = index & AddressMask;
            var getMethod = GetMethods[registerIndex];
            return getMethod != null ? getMethod() : (byte)0;
        }

        public void Set(int index, byte value)
        {
            var registerIndex = index & AddressMask;
            var setMethod = SetMethods[registerIndex];
            setMethod?.Invoke(value);
        }

        private byte ReadStatus()
        {
            var result = Status;
            Status &= 0x7F;
            return result;
        }

        private byte ReadOamData()
        {
            return _oam[OAMAddress];
        }

        private byte ReadVRAM()
        {
            var address = Address & 0x3FFF;
            return _vram[address];
        }

        private void WriteControll(byte value)
        {
            Controll = value;
            CheckNMI();
        }

        private void WriteMask(byte value)
        {
            Mask = value;
        }

        private void WriteOAMAddress(byte value)
        {
            OAMAddress = value;
        }

        private void WriteOamData(byte value)
        {
            _oam[OAMAddress++] = value;
        }

        private void WriteScroll(byte value)
        {
            var latch = (Status & 0x80) != 0;
            if (latch)
            {
                Scroll = (ushort)((Scroll & 0xFF00) | (value));
            }
            else
            {
                Scroll = (ushort)((Scroll & 0x00FF) | (value << 8));
            }
            Status ^= 0x80;
            return;
        }

        private void WriteAddress(byte value)
        {
            var latch = (Status & 0x80) != 0;
            if (latch)
            {
                Address = (ushort)((Address & 0xFF00) | (value));
            }
            else
            {
                Address = (ushort)((Address & 0x00FF) | (value << 8));
            }
            Status ^= 0x80;
            return;
        }

        private void WriteVRAM(byte value)
        {
            var address = Address & 0x3FFF;
            _vram.Set(address, value);
            if ((Controll & 0b100) == 0)
            {
                Address++;
            }
            else
            {
                Address += 32;
            }
        }

        private void CheckNMI()
        {
            var nmiEnabled = (Controll & 0x80) != 0;
            var nmiOccured = (Status & 0x80) != 0;
            /*if (nmiEnabled && nmiOccured)
            {
                _interupts.NMI = true;
            }*/
            _interupts.NMI = nmiEnabled && nmiOccured;
        }

        public byte this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
