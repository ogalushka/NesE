using System;

namespace NesE.nes.rom
{
    public class ROM : IROM
    {
        const int HeaderBytes = 16;
        const int TrainerBytes = 512;
        public readonly int PrgRomBytes;
        public readonly int ChrRomBytes;
        public readonly int PrgRamBytes;

        public readonly RomMirroring Mirroring;
        public readonly bool PersistentMem;
        public readonly bool Trainer;
        public readonly bool IgnoreMirroring;
        public readonly bool VSUnisystem;
        public readonly bool PlayChoice;
        public readonly TVSystem TVSystem;
        private readonly int _mapper;

        public readonly byte[] _rawData;

        public int Mapper => _mapper;

        public ROM(byte[] data)
        {
            _rawData = data;
            PrgRomBytes = data[4] * 16384;
            ChrRomBytes = data[5] * 8192;

            var flags6 = data[6];
            if ((flags6 & 1) == 0)
            {
                Mirroring = RomMirroring.Horizontal;
            }
            else
            {
                Mirroring = RomMirroring.Vertical;
            }

            PersistentMem = (flags6 & 0b10) != 0;
            Trainer = (flags6 & 0b100) != 0;
            IgnoreMirroring = (flags6 & 0b1000) != 0;

            var flags7 = data[7];

            _mapper = (flags7 & 0xF0) | ((flags6 & 0xF0) >> 4);
            VSUnisystem = (flags7 & 1) != 0;
            PlayChoice = (flags7 & 0b10) != 0;

            PrgRamBytes = data[8] * 8192;

            if ((data[9] & 1) == 0)
            {
                TVSystem = TVSystem.NTSC;
            }
            else
            {
                TVSystem = TVSystem.PAL;
            }
        }

        public byte[] GetPrgRom()
        {
            var result = new byte[PrgRomBytes];
            var trainerBytes = Trainer ? TrainerBytes : 0;
            var startIndex = HeaderBytes + trainerBytes;
            Array.Copy(_rawData, startIndex, result, 0, PrgRomBytes);
            return result;
        }

        public byte[] GetChrRom()
        {
            var result = new byte[ChrRomBytes];
            var trainerBytes = Trainer ? TrainerBytes : 0;
            var startIndex = HeaderBytes + trainerBytes + PrgRomBytes;
            Array.Copy(_rawData, startIndex, result, 0, ChrRomBytes);
            return result;
        }

        public static bool IsValidROM(byte[] data)
        {
            return data[0] == 0x4E 
                && data[1] == 0x45 
                && data[2] == 0x53 
                && data[3] == 0x1A
                && data.Length >= 16;
        }
    }
}
