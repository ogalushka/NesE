using System;
using System.Diagnostics;
using NesE.nes.cpu;
using NesE.nes.memory;
using NesE.nes.ppu;
using NesE.nes.rom;

namespace NesE.nes
{
    public class NES
    {
        private const float MasterClock = 21477272f;

        private const float CPUFrequency = MasterClock / 12f;
        private const float CPUCycleTime = 1f / CPUFrequency;

        private const float PPUFrequency = MasterClock / 4f;
        private const float PPUCycleTime = 1f / PPUFrequency;

        //private const float

        private float cpuTimeBehind = 0;
        private float ppuTimeBehind = 0;

        public CPU CPU;
        public PPU PPU;

        public NES()
        {
        }

        public void Update(float dt)
        {
            cpuTimeBehind += dt;

            while (cpuTimeBehind > CPUCycleTime)
            {
                cpuTimeBehind -= CPUCycleTime;
                CPU.Step();

                PPU.Step();
                PPU.Step();
                PPU.Step();
            }
        }

        public void SetRom(IROM rom)
        {
            var cpuMem = new Memory(13);
            var ppuMem = new Memory(12);
            var ppuMirrorMem = new Memory(11);

            var interupts = new Interupts();
            var inernalRam = new byte[0x800];
            var dummySpace = new byte[0x2000];
            var ppuOAM = new byte[0x100];
            var VRAM = new byte[0x1000];
            var ppuRegisters = new PPURegisters(ppuOAM, ppuMem, interupts);
            var ioRegisters = new IORegisters(ppuOAM, cpuMem);
            var paletteRAM = new byte[0x20];

            cpuMem.AddAddressSpace(0, 0b0000_0111_1111_1111, inernalRam);
            cpuMem.AddAddressSpace(1, ppuRegisters);
            cpuMem.AddAddressSpace(2, ioRegisters);
            cpuMem.AddAddressSpace(3, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(4, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(5, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(6, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(7, 0x1FFF, dummySpace);

            ppuMem.AddAddressSpace(2, 0xFFF, VRAM);
            ppuMem.AddAddressSpace(3, ppuMirrorMem);
            ppuMirrorMem.AddAddressSpace(0b0011_0, 0xEFF, VRAM);
            ppuMirrorMem.AddAddressSpace(0b0011_1, 0x1F, paletteRAM);

            Mappers.AddRomMem(rom, cpuMem, ppuMem);

            CPU = new CPU(cpuMem, interupts);
            PPU = new PPU(ppuMem, ppuRegisters, ppuOAM);
        }
    }
}
