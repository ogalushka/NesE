using NesE.nes.memory;
using System;
using System.Collections.Generic;

namespace NesE.nes.ppu
{
    public class PPU
    {
        const int ScanLines = 262;
        const int CyclePerLine = 341;
        const ushort NametableAddr = 0x2000;
        const ushort AttributeTableOffset = 0x3C0;
        const int TilesPerLine = 32;

        public readonly Queue<RenderUnit> RenderQueue;
        public IMemory Memory;
        public PPURegisters Registers;
        public byte[] OAM;
        public byte[] SecondaryOAM;
        
        private int cycle;
        private int line;
        private bool oddFrame;

        public PPU(Memory ppuMem, PPURegisters registers, byte[] oam)
        {
            RenderQueue = new Queue<RenderUnit>();
            Memory = ppuMem;
            Registers = registers;
            OAM = oam;
            SecondaryOAM = new byte[0xFF];
            line = 261;
        }

        public void Step()
        {
            if (line == 261)
            {
                if (cycle == 1)
                {
                    ClearVBlank();
                }
                IncrementCycleCounter();
                return;
            }

            if (line >= 240)
            {
                var setVBlankCycle = line == 241 && cycle == 1;
                if (setVBlankCycle)
                {
                    SetVBlank();
                }
                IncrementCycleCounter();
                return;
            }

            var visibleLine = 0 < cycle && cycle < 257;
            if (visibleLine && cycle % 8 == 1)
            {
                var tileX = cycle / 8;
                var tileY = line / 8;
                var tileLine = line % 8;

                var nameTableTileOffset = tileX + (tileY * TilesPerLine);

                // TODO support multiple nametables & scrolling
                var nameTableByte = Memory.Get(NametableAddr + nameTableTileOffset);
                
                // TODO add PatternTable index
                const int PatternTableIndex = 1;
                var patternTableAddress = nameTableByte << 4;
                patternTableAddress = patternTableAddress | tileLine;
                patternTableAddress = patternTableAddress | PatternTableIndex << 12;
                var patternTableLowerAddress = patternTableAddress;
                var patternTableUpperAddress = patternTableAddress | 0b1000;

                var patternTableLow = Memory.Get(patternTableLowerAddress);
                var patternTableHigh = Memory.Get(patternTableUpperAddress);

                // TODO support multiple nametables & scrolling
                var attributeTableTileOffset = (tileX / 4) + ((tileY / 4) * (TilesPerLine / 4));
                var attributeTableAddress = NametableAddr + AttributeTableOffset + attributeTableTileOffset;
                var attributeTableByte = Memory.Get(attributeTableAddress);

                RenderQueue.Enqueue(new RenderUnit {
                    X = tileX * 8,
                    Y = line,
                    Attribute = attributeTableByte,
                    LowPattern = patternTableLow,
                    HighPattern = patternTableHigh
                });
            }
            IncrementCycleCounter();
        }

        private void IncrementCycleCounter()
        {
            cycle++;
            if (cycle >= 341)
            {
                cycle = 0;
                line++;
            }

            if (line >= 262)
            {
                line = 0;
                oddFrame = !oddFrame;
            }

            var renderingDisabled = (Registers.Mask & 0x18) == 0;
            if (line == 0 && cycle == 0 && (oddFrame || renderingDisabled))
            {
                cycle++;
            }
        }

        private void SetVBlank()
        {
            Registers.Status = (byte)(Registers.Status | 0x80);
        }

        private void ClearVBlank()
        {
            Registers.Status = (byte)(Registers.Status & 0x7F);
        }
    }
}
